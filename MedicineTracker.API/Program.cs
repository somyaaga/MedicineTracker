using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using MedicineTracker.API.Interface;
using MedicineTracker.API.Services;
using Microsoft.EntityFrameworkCore;
using MedicineTracker.API.Data;

var builder = WebApplication.CreateBuilder(args);

var keyVaultName = builder.Configuration["KeyVaultName"];
if (!string.IsNullOrEmpty(keyVaultName))
{
    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
}

// Add services to the container.
builder.Services.AddScoped<IMedicine, MedicineServices>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddSingleton<ILogger,logg>();


builder.Services.AddDbContext<MedDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

//keyvault
//builder.Services.AddDbContext<MedDBContext>(options =>
//    options.UseSqlServer(builder.Configuration["DefaultConnection"]));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC",
        policy =>
        {
            policy.WithOrigins("https://localhost:7238") // your MVC port
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS
app.UseCors("AllowMVC");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
