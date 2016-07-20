using GenericBackend.Core;

namespace GenericBackend.DataModels.GoodNightMedical
{
    public class MachineOrder : MongoEntityBase
    {
        public RentOption RentOption { get; set; }
        public Machine Machine { get; set; }
        public DoctorPrescription Prescription { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ContactMethod PreferredContract { get; set; }
        public string Comments { get; set; }

    }
}