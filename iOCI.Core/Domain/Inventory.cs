namespace iOCO.Core.Domain
{
    public class Inventory : DomainBase
    {
        public int Id { get; set; }
        public string InventoryName { get; set; }
        public int NoOfDays { get; set; }
        public long SurvivorId { get; set; }
        public virtual Survivor Survivor { get; set; }
    }
}
