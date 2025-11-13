using System.ComponentModel.DataAnnotations;

namespace MedicineTracker.MVC.Models
{
    public class Medicine
    {
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            public string Notes { get; set; }
            [Required]
            public DateTime Expiry { get; set; }
            [Required]
            public int Quantity { get; set; }
            [Required]
            public float Price { get; set; }
            [Required]
            public string Brand { get; set; }        
    }
}
