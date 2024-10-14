using Asp.Versioning;
using AutoMapper;
using DriverInfo.API.Models;
using DriverInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DriverInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/drivers")]
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

        /// <summary>
        /// Get a list of all drivers.
        /// </summary>
        /// <param name="name">The name of the driver to get</param>
        /// <param name="searchQuery">Searchterm</param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a driver by id
        /// </summary>
        /// <param name="driverId">The id of the driver to get</param>
        /// <param name="includeWins">Whether or not to include the wins of the driver</param>
        /// <returns>A driver with or without their wins</returns>
        ///<response code = "200">Returns the requested driver</response>
        [HttpGet("{driverId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DriverDto>> GetDriver(int driverId, bool includeWins = false)
        {
            var driver = await _driverInfoRepository.GetDriverAsync(driverId, includeWins);

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
