using AutoMapper;
using DriverInfo.API.Models;
using DriverInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DriverInfo.API.Controllers
{
    [ApiController]
    [Route("api/drivers")]
    public class DriversController : ControllerBase
    {
        const int maxDriversPageSize = 20;

        private readonly IDriverInfoRepository _driverInfoRepository;
        private readonly IMapper _mapper;

        public DriversController(IDriverInfoRepository driverInfoRepository, IMapper mapper)
        {
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverWithoutWinsDto>>> GetDrivers([FromQuery]string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize > maxDriversPageSize)
            {
                pageSize = maxDriversPageSize;
            }

            var (driverEntities, paginationMetadata) = await _driverInfoRepository.GetDriversAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            
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
