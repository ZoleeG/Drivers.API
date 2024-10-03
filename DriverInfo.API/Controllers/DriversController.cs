using DriverInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DriverInfo.API.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<DriversDto>> GetDrivers()
        {
            return Ok(DriversDataStore.Current.Drivers);
        }

        [HttpGet("{id}")]
        public ActionResult<DriversDto> GetDriver(int id)
        {
            var driverToReturn = DriversDataStore.Current.Drivers.FirstOrDefault(x => x.Id == id);

            if (driverToReturn == null)
            {
                return NotFound();
            }

            return Ok(driverToReturn);
        }
    }
}
