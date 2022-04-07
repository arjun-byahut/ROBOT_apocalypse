using iOCO.Core.Domain;
using iOCO.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iOCO.Core.Surviour
{
    public interface ISurvivorService
    {
        public Task<bool> AddSurvivor(Survivor survivor);
        public Task<bool> UpdateSurvivorLocation(LocationDetails locationDetails);
        public Task<IEnumerable<Survivor>> GetSurviours(bool isInfected);
        public Task<decimal> GetSurvioursPercentage(bool isInfected);
        public Task<string> ReportContaminatedSurvivor(long survivorID, long reporterSurvivorId);
    }
}
