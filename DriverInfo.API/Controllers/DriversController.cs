using DriverInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DriverInfo.API.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        private readonly DriversDataStore _driversDataStore;

        public DriversController(DriversDataStore driversDataStore)
        {
            _driversDataStore = driversDataStore ?? throw new ArgumentNullException(nameof(driversDataStore));
        }


        [HttpGet]
        public ActionResult<IEnumerable<DriversDto>> GetDrivers()
        {
            return Ok(_driversDataStore.Drivers);
        }

        [HttpGet("{id}")]
        public ActionResult<DriversDto> GetDriver(int id)
        {
            var driverToReturn = _driversDataStore.Drivers.FirstOrDefault(x => x.Id == id);

            if (driverToReturn == null)
            {
                return NotFound();
            }

            return Ok(driverToReturn);
        }
    }
}
