using iOCO.Core.Domain;
using iOCO.Core.Model;
using iOCO.Core.Surviour;
using iOCO.Infrastructure.EntityFrameworkCore.DBContext;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iOCO.Infrastructure.Implementation.Surviour
{
    public class SurvivorService : BaseEFService, ISurvivorService
    {
        public async Task<bool> AddSurvivor(Survivor survivor)
        {
            if (survivor == null)
            {
                return false;
            }

            survivor.CreatedBy = 1;
            survivor.CreatedDate = DateTime.Now;
            survivor.Inventories.ToList().ForEach(x => { x.CreatedBy = 1; x.CreatedDate = DateTime.Now; });
            var survivorRepo = Repository<Survivor>();
            survivorRepo.Add(survivor);

            try
            {
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }

        public async Task<IEnumerable<Survivor>> GetSurviours(bool isInfected)
        {
            var survivorRepo = Repository<Survivor>();
            try
            {
                var allSurvior = survivorRepo.ListAll();
                var survivors = await allSurvior.Include(x => x.Inventories).Where(x => x.IsInfected == isInfected).ToListAsync();
                return survivors;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<decimal> GetSurvioursPercentage(bool isInfected)
        {
            var survivorRepo = Repository<Survivor>();
            try
            {
                var allSurvior = await survivorRepo.ListAll().ToListAsync();
                if (allSurvior == null)
                    return 0;

                var survivors = allSurvior.Where(x => x.IsInfected == isInfected).ToList();
                if (survivors == null)
                    return 0;

                decimal suvivorsCount = survivors.Count;
                decimal allSuvivorsCount = allSurvior.Count;
                var percent = (suvivorsCount / allSuvivorsCount) * 100M;
                return percent;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ReportContaminatedSurvivor(long survivorId, long reporterSurvivorId)
        {
            var survivorRepo = Repository<Survivor>();
            try
            {
                var allSurvior = survivorRepo.ListAll();
                var survivor = await allSurvior.SingleOrDefaultAsync(x => x.Id == survivorId);
                if (survivor == null)
                    return "No contaminated survivor.";

                if (survivor.IsInfected)
                    return "Survivor is infacted.";

                var reporterSurvivor = await allSurvior.SingleOrDefaultAsync(x => x.Id == reporterSurvivorId);
                if (reporterSurvivor == null)
                    return "No survivor exist to check for contamination.";

                if (reporterSurvivor.IsInfected)
                    return "Survivor is already infected and can not report other survior's status.";

                var survivorContaminationRepo = Repository<ContaminatedSurvivor>();
                var survivorContaminations = survivorContaminationRepo.ListAll();
                var survivorContaminationObj = await survivorContaminations.FirstOrDefaultAsync(x => x.ReporterSurvivorId == reporterSurvivorId && x.SurvivorId == survivorId);
                if (survivorContaminationObj != null)
                    return "Survivor is already  contaminated by reporter.";

                int contaminationCount = survivorContaminations.Where(x => x.SurvivorId == survivorId).Count();
                if (contaminationCount == 2)
                {
                    //Mark survivor as infacted
                    survivor.IsInfected = true;
                    survivorRepo.Update(survivor);
                }

                ContaminatedSurvivor objSurvivorContamination = new();
                objSurvivorContamination.ReporterSurvivorId = reporterSurvivorId;
                objSurvivorContamination.SurvivorId = survivorId;
                survivorContaminationRepo.Add(objSurvivorContamination);
                await SaveChangesAsync();
                return "Survivor contaminated.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateSurvivorLocation(LocationDetails survivorLocation)
        {
            if (survivorLocation != null)
            {
                var survivorRepo = Repository<Survivor>();
                try
                {
                    var survivor = await survivorRepo.GetByIdAsync(survivorLocation.SurvivorId);
                    if (survivor == null)
                        return false;

                    survivor.Latitude = survivorLocation.Latitude;
                    survivor.Longitude = survivorLocation.Longitude;
                    survivor.UpdateDate = DateTime.Now;
                    survivor.UpdatedBy = 1;
                    survivorRepo.Update(survivor);

                    await SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

            return false;
        }
    }
}
