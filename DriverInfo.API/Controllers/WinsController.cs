using DriverInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace DriverInfo.API.Controllers
{
    [Route("api/drivers/{driverId}/[controller]")]
    [ApiController]
    public class WinsController : ControllerBase
    {
        private readonly ILogger<WinsController> _logger;

        public WinsController(ILogger<WinsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<WinDto>> GetWins(int driverId)
        {
            var driver = DriversDataStore.Current.Drivers.FirstOrDefault(
                d => d.Id == driverId);

            if (driver == null)
            {
                _logger.LogInformation($"Driver with id {driverId} wasn't found.");
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
            //-----This is already included by the [ApiController]----

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

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
                Year = win.Year,
            };

            driver.Wins.Add(finalWin);
            return CreatedAtRoute("GetWin", new
            {
                driverId = driverId,
                winId = finalWin.Id
            },
            finalWin);
        }

        [HttpPut("{winid}")]
        public ActionResult UpdateWin(int driverId, int winId, WinForUpdateDto win)
        {
            var driver = DriversDataStore.Current.Drivers.FirstOrDefault(d => d.Id == driverId);

            if (driver == null)
            {
                return NotFound();
            }

            var winFromStore = driver.Wins.FirstOrDefault(w => w.Id == winId);

            if (winFromStore == null)
            {
                return NotFound();
            }

            winFromStore.Name = win.Name;
            winFromStore.GridPosition = win.GridPosition;
            winFromStore.Year = win.Year;

            return NoContent();
        }

        [HttpPatch("{winid}")]
        public ActionResult PartiallyUpdateWin(int driverId, int winId, JsonPatchDocument<WinForUpdateDto> patchDocument)
        {
            var driver = DriversDataStore.Current.Drivers.FirstOrDefault(d => d.Id == driverId);

            if (driver == null)
            {
                return NotFound();
            }

            var winFromStore = driver.Wins.FirstOrDefault(w => w.Id == winId);

            if (winFromStore == null)
            {
                return NotFound();
            }

            var winToPatch = new WinForUpdateDto()
                {
                    Name= winFromStore.Name,
                    GridPosition= winFromStore.GridPosition,
                    Year= winFromStore.Year
                };

            patchDocument.ApplyTo(winToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(winToPatch))
            {
                return BadRequest(ModelState);
            }

            winFromStore.Name = winToPatch.Name;
            winFromStore.GridPosition = winToPatch.GridPosition;
            winFromStore.Year = winToPatch.Year;

            return NoContent();
        }

        [HttpDelete("{winid}")]
        public ActionResult DeleteWin(int driverId, int winId)
        {
            var driver = DriversDataStore.Current.Drivers.FirstOrDefault(d => d.Id == driverId);

            if (driver == null)
            {
                return NotFound();
            }

            var winFromStore = driver.Wins.FirstOrDefault(w => w.Id == winId);

            if (winFromStore == null)
            {
                return NotFound();
            }

            driver.Wins.Remove(winFromStore);

            return NoContent();
        }
    }
}
