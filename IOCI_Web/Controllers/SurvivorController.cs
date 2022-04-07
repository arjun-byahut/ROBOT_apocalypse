using AutoMapper;
using iOCO.Core.Domain;
using iOCO.Core.Model;
using iOCO.Core.Surviour;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOCI_Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SurvivorController : ControllerBase
    {
        private readonly ISurvivorService _survivorService;
        private readonly IMapper _mapper;

        public SurvivorController(ISurvivorService survivorService, IMapper mapper)
        {
            _survivorService = survivorService;
            _mapper = mapper;
        }

        [Route("AddSurvivor")]
        [HttpPost]
        public async Task<bool> AddSurvivor(SurvivorDetails survivorInfo)
        {
            try
            {
                var survivor = _mapper.Map<Survivor>(survivorInfo);
                return await _survivorService.AddSurvivor(survivor);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("UpdateSurvivorLocation")]
        [HttpPatch]
        public async Task<bool> UpdateSurvivorLocation(LocationDetails locationDetails)
        {
            try
            {
                return await _survivorService.UpdateSurvivorLocation(locationDetails);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("GetInfactedSurviours")]
        [HttpGet]
        public async Task<IEnumerable<SurvivorDetails>> GetInfactedSurviours()
        {
            try
            {
                var survivors = await _survivorService.GetSurviours(true);
                return _mapper.Map<IList<SurvivorDetails>>(survivors);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("GetNonInfactedSurviours")]
        [HttpGet]
        public async Task<IEnumerable<SurvivorDetails>> GetNonInfactedSurviours()
        {
            try
            {
                var survivors = await _survivorService.GetSurviours(false);
                return _mapper.Map<IList<SurvivorDetails>>(survivors);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("GetNonInfactedSurviourPercentage")]
        [HttpGet]
        public async Task<decimal> GetNonInfactedSurviourPercentage()
        {
            try
            {
                return await _survivorService.GetSurvioursPercentage(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("GetInfactedSurviourPercentage")]
        [HttpGet]
        public async Task<decimal> GetInfactedSurviourPercentage()
        {
            try
            {
                return await _survivorService.GetSurvioursPercentage(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("ReportContaminatedSurvivor")]
        [HttpPost]
        public async Task<string> ReportContaminatedSurvivor(long survivorID, long reporterSurvivorId)
        {
            try
            {
                return await _survivorService.ReportContaminatedSurvivor(survivorID, reporterSurvivorId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
