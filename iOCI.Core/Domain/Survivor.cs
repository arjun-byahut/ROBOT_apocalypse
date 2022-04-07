using System.Collections.Generic;

namespace iOCO.Core.Domain
{
    public class Survivor : DomainBase
    {
        public Survivor()
        {
            Inventories = new List<Inventory>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsInfected { get; set; }
        public virtual IEnumerable<Inventory> Inventories { get; set; }
    }
}
