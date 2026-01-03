using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineTracker.API.Interface;
using MedicineTracker.API.Models;
using System.Diagnostics.Eventing.Reader;

namespace MedicineTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        public readonly IMedicine _medicineService;
       
        public MedicineController(IMedicine medservice)
        {
            _medicineService = medservice;
        }

        [HttpGet]
        public IActionResult FetchAllMedicines()
        {
            var medicines = _medicineService.GetAllMedicines();
            if (!medicines.Any() || medicines==null)
                return NotFound("No Medicines yet. Add some!");
            else
            return Ok(medicines);
        }
         
        [HttpGet("Search")]
        public IActionResult SearchMedicine(string? searchTerm)
        {
            var medicines = _medicineService.GetAllMedicines();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                medicines = medicines.Where(m => m.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

                if(!medicines.Any())
                    return NotFound("No medicines found matching the search criteria.");
                else
                    return Ok(medicines);
            }
            else
            {
                return BadRequest("Search term cannot be empty.");
            }
        }

        [HttpPost]
        public IActionResult AddMeds(Medicine meds)
        {
            _medicineService.AddMedicine(meds);

            if(meds == null)
                return BadRequest("Invalid medicine data.");
            else
                return Created("","Medicine added successfully.");
        }
    }
}
