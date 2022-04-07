using iOCO.Core.Model;
using iOCO.Core.Robot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IOCI_Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly IRobotService _robotService;

        public RobotController(ILogger<RobotController> logger, IRobotService robotService)
        {
            _robotService = robotService;
        }

        [Route("GetAllRobots")]
        [HttpGet]
        public async Task<IEnumerable<RobotDetails>> GetAllRobots()
        {
            try
            {
                return await _robotService.GetRobots();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
