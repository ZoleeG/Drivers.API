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
            return new JsonResult(
                new List<object>
                {
                    new{ id = 1, Name = "Max Verstappen"},
                    new{ id = 2, Name = "Lewis Hamilton"}
                });
        }
    }
}
