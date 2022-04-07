using iOCO.Core.Model;
using iOCO.Core.Robot;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace iOCO.Infrastructure.Implementation.Robot
{
    public class RobotService : IRobotService
    {
        private readonly IConfiguration iConfig;

        public RobotService(IConfiguration iConfig)
        {
            this.iConfig = iConfig;
        }
        public async Task<IEnumerable<RobotDetails>> GetRobots()
        {
            using (var httpClient = new HttpClient())
            {
                var robotData = await httpClient.GetStringAsync(iConfig.GetValue<string>("RobotCPUSys:Url"));
                var robotInfoList = JsonConvert.DeserializeObject<IList<RobotDetails>>(robotData);
                return robotInfoList;
            }
        }
    }
}
