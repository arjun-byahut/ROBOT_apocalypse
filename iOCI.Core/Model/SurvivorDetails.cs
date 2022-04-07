using System.Collections.Generic;

namespace iOCO.Core.Model
{
    public class SurvivorDetails
    {
        public SurvivorDetails()
        {
            Inventories = new List<InventoryDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public IEnumerable<InventoryDetails> Inventories { get; set; }
    }
}
