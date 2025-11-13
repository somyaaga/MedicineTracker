using MedicineTracker.API.Interface;
using MedicineTracker.API.Models;
using System.Text.Json;

namespace MedicineTracker.API.Services
{
    public class MedicineServices : IMedicine
    {
        List<Medicine> medlist = new List<Medicine>();
        private readonly string filePath = "Data/medicines.json";

        public MedicineServices()
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            if (!File.Exists(filePath)) File.WriteAllText(filePath, "[]");
        }
        public List<Medicine> GetAllMedicines()
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Medicine>>(json) ?? medlist;  
        }

        public void AddMedicine(Medicine meds)
        {
            var medicines = GetAllMedicines();
            meds.Id = medicines.Count > 0 ? medicines.Max(m => m.Id) + 1 : 1;
            medicines.Add(meds);
            File.WriteAllText(filePath, JsonSerializer.Serialize(medicines, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
