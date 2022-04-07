using iOCO.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iOCO.Core.Robot
{
    public interface IRobotService
    {
        Task<IEnumerable<RobotDetails>> GetRobots();
    }

    
}
