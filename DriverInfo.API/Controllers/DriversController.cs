using AutoMapper;
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
        private readonly IMapper _mapper;

        public DriversController(IDriverInfoRepository driverInfoRepository, IMapper mapper)
        {
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverWithoutWinsDto>>> GetDrivers([FromQuery]string? name)
        {
            var driverEntities = await _driverInfoRepository.GetDriversAsync(name);
            
            return Ok(_mapper.Map<IEnumerable<DriverWithoutWinsDto>>(driverEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> GetDriver(int id, bool includeWins = false)
        {
            var driver = await _driverInfoRepository.GetDriverAsync(id, includeWins);

            if (driver == null)
            {
                return NotFound();
            }
            if (includeWins)
            {
                return Ok(_mapper.Map<DriverDto>(driver));
            }

            return Ok(_mapper.Map<DriverWithoutWinsDto>(driver));
        }
    }
}
