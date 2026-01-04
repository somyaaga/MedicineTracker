using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicineTracker.API.Interface;
using Asp.Versioning;
using MedicineTracker.API.Models;
using System.Diagnostics.Eventing.Reader;
using Azure.Messaging;

namespace MedicineTracker.API.Controllers.v1 //version folder
{
    //[Route("api/[controller]")]
    [Route("api/v{version:ApiVersion}/[controller]")] // Versioned route
    [Route("api/Medicine")] // Default route
    [ApiVersion("1.0")] // Specify the API version
    [ApiController]
    public class MedicineController : ControllerBase
    {
        public readonly IMedicine _medicineService;
       
        public MedicineController(IMedicine medservice)
        {
            _medicineService = medservice;
        }

        [HttpGet]
        public ActionResult FetchAllMedicines()
        {
            
            var medicines = _medicineService.GetAllMedicines();
            if (!medicines.Any() || medicines==null)
                return NotFound("No Medicines yet. Add some!");
            else
            return Ok( new
            {
                Message = "Medicines v1 retrieved successfully.",
                Medicines = medicines
            }
               );
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
