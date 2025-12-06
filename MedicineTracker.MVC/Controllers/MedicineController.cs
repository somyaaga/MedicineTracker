using MedicineTracker.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using MedicineTracker.MVC.Models;   
namespace MedicineTracker.MVC.Controllers
{
    public class MedicineController : Controller
    {
        private readonly MedAPIService _medApiService;

        public MedicineController(MedAPIService medapi)
        {
            _medApiService = medapi;
        }

        public async Task<IActionResult> Index()  //getting all data
        {
            var medicines = await _medApiService.GetAllMedicinesAsync();
            if (!medicines.Any() || medicines == null)
            {
                ViewBag.Message = "No medicines added yet.";
                return View(new List<Medicine>());
            }
            else
                return View(medicines);
        }

        public  async Task<IActionResult> Search(string searchTerm)  //searching data
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction("Index");
            }
            var medicines = await _medApiService.SearchMedicinesAsync(searchTerm);
            if (medicines == null || !medicines.Any())
            {
                ViewBag.Message = "No medicines found matching the search criteria.";
                return View("Index",new List<Medicine>());
            }
            return View("Index",medicines);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Medicine medicine)
        {
            if (!ModelState.IsValid)
                return View(medicine);

            await _medApiService.AddMedicineAsync(medicine);

            // Since AddMedicineAsync returns void, we cannot check for success.
            // If you want to handle errors, consider updating AddMedicineAsync to return a bool or throw exceptions.

            return RedirectToAction("Index");
        }
    }
}
