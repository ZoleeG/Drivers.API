using DriverInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverInfo.API.Controllers
{
    [Route("api/drivers/{driverId}/[controller]")]
    [ApiController]
    public class WinsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<WinDto>> GetWins(int driverId)
        {
            var driver = DriversDataStore.Current.Drivers.FirstOrDefault(
                d => d.Id == driverId);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver.Wins);
        }

        [HttpGet("{winId}")]
        public ActionResult<WinDto> GetWin(int driverId, int winId)
        {
            var driver = DriversDataStore.Current.Drivers.FirstOrDefault(
                d => d.Id == driverId);

            if (driver == null)
            {
                return NotFound();
            }

            var win = driver.Wins.FirstOrDefault(
                w => w.Id == winId);

            if (win == null)
            {
                return NotFound();
            }

            return Ok(win);
        }
    }
}
