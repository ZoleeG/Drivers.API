using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DriverInfo.API.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        [HttpGet()]
        public JsonResult GetDrivers()
        {
            return new JsonResult(DriversDataStore.Current.Drivers);
        }

        [HttpGet("{id}")]
        public JsonResult GetDriver(int id)
        { 
            return new JsonResult(
                DriversDataStore.Current.Drivers.FirstOrDefault(x => x.Id == id));
        }
    }
}
