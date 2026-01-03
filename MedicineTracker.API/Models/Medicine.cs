using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicineTracker.API.Models;

public partial class Medicine
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Notes { get; set; }

    public DateOnly? Expiry { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public string? Brand { get; set; }
}
