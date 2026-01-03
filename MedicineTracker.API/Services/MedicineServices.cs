using MedicineTracker.API.Interface;
using MedicineTracker.API.Models;
using System.Text.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MedicineTracker.API.Data;

namespace MedicineTracker.API.Services
{
    public class MedicineServices : IMedicine
    {
        
        private readonly MedDBContext _meddbconetxt;
        public MedicineServices(MedDBContext dBContext)
        {
            _meddbconetxt = dBContext;
        }

        public List<Medicine> GetAllMedicines()
        {
             var medlist = _meddbconetxt.Medicines.AsNoTracking().ToList();
            return medlist;

        }

        public void AddMedicine(Medicine meds)
        {      
            var medicines = GetAllMedicines();
            meds.Id = medicines.Count > 0 ? medicines.Max(m => m.Id) + 1 : 1;
            medicines.Add(meds);
            
            //File.WriteAllText(filePath, JsonSerializer.Serialize(medicines, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
