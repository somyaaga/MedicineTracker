using MedicineTracker.API.Models;

namespace MedicineTracker.API.Interface
{
    public interface IMedicine
    {
        List<Medicine> GetAllMedicines();
        void AddMedicine(Medicine med);
    }
}
