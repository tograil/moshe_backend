using GenericBackend.Core;

namespace GenericBackend.DataModels.GoodNightMedical
{
    public class RentOption : MongoEntityBase
    {
        public string Name { get; set; }
        public decimal PricePerMonth { get; set; }
    }
}