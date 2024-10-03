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

        [HttpGet("{winId}", Name = "GetWin")]
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

        [HttpPost]
        public ActionResult<WinDto> CreateWin(int driverId, [FromBody] WinForCreationDto win)
        {
            var driver = DriversDataStore.Current.Drivers.FirstOrDefault(d => d.Id == driverId);

            if (driver == null)
            {
                return NotFound();
            }

            var maxWinId = DriversDataStore.Current.Drivers.SelectMany(d => d.Wins).Max(w => w.Id);

            var finalWin = new WinDto()
            {
                Id = ++maxWinId,
                Name = win.Name,
                GridPosition = win.GridPosition,
                Year = win.Year
            };

            driver.Wins.Add(finalWin);
            return CreatedAtRoute("GetWin", new
            {
                driverId = driverId,
                winId = finalWin.Id
            },
            finalWin);
        }
    }
}
