using DriverInfo.API.Models;
using DriverInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DriverInfo.API.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverInfoRepository _driverInfoRepository;

        public DriversController(IDriverInfoRepository driverInfoRepository)
        {
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverWithoutWinsDto>>> GetDrivers()
        {
            var driverEntities = await _driverInfoRepository.GetDriversAsync();
            var results = new List<DriverWithoutWinsDto>();
            foreach (var driverEntity in driverEntities)
            {
                results.Add(new DriverWithoutWinsDto
                {
                    Id = driverEntity.Id,
                    Description = driverEntity.Description,
                    Name = driverEntity.Name,
                }
                );
            }

            return Ok( results );
        }

        [HttpGet("{id}")]
        public ActionResult<DriverDto> GetDriver(int id)
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
