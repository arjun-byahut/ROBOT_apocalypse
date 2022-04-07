namespace iOCO.Core.Domain
{
    public class ContaminatedSurvivor : DomainBase
    {
        public int Id { get; set; }
        public long SurvivorId { get; set; }
        public long ReporterSurvivorId { get; set; }
    }
}
