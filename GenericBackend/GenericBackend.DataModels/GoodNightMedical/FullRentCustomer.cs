using GenericBackend.Core;

namespace GenericBackend.DataModels.GoodNightMedical
{
    public class FullRentCustomer : MongoEntityBase, IMongoEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ContactMethod ContactMethod { get; set; }
        public DoctorPrescription DoctorPrescription { get; set; }
        public string Comments { get; set; }
        public string MachineId { get; set; }
        public bool New { get; set; } = true;
    }
}
