using Asp.Versioning;
using AutoMapper;
using DriverInfo.API.Entities;
using DriverInfo.API.Models;
using DriverInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Principal;

namespace DriverInfo.API.Controllers
{
    //[Authorize(Policy ="OnlyDogPeople")]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{version:apiVersion}/drivers/{driverId}/[controller]")]
    public class WinsController : ControllerBase
    {
        private readonly ILogger<WinsController> _logger;
        private readonly IMailService _mailService;
        private readonly IDriverInfoRepository _driverInfoRepository;
        private readonly IMapper _mapper;

        public WinsController(ILogger<WinsController> logger, IMailService mailService, IDriverInfoRepository driverInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get the wins of a driver
        /// </summary>
        /// <param name="driverId">The id of the driver whose wins to get</param>
        /// <returns>The list of the driver's wins</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WinDto>>> GetWins(int driverId)
        {
            
            if (!await _driverInfoRepository.DriverExistsAsync(driverId))
            {
                _logger.LogInformation($"Driver with id {driverId} wasn't found.");

                return NotFound();
            }

            var winsForDriver = await _driverInfoRepository
                .GetWinsForDriverAsync(driverId);

            return Ok(_mapper.Map<IEnumerable<WinDto>>(winsForDriver));
        }

        /// <summary>
        /// Get a win for a driver
        /// </summary>
        /// <param name="driverId">The id of the driver</param>
        /// <param name="winId">The id of the win to get</param>
        /// <returns>A single win</returns>
        [HttpGet("{winId}", Name = "GetWin")]
        public async Task<ActionResult<WinDto>> GetWin(int driverId, int winId)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(driverId))
            {
                _logger.LogInformation($"Driver with id {driverId} wasn't found.");

                return NotFound();
            }

            var win = await _driverInfoRepository.GetWinForDriverAsync(driverId, winId);

            if (win == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WinDto>(win));
        }

        /// <summary>
        /// Adding a win to a driver
        /// </summary>
        /// <param name="driverId">The driver's id to add the win to</param>
        /// <param name="win">The win to add</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<WinDto>> CreateWin(int driverId, [FromBody] WinForCreationDto win)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(driverId))
            {
                _logger.LogInformation($"Driver with id {driverId} wasn't found.");

                return NotFound();
            }

            var finalWin = _mapper.Map<Entities.Win>(win);

            await _driverInfoRepository.AddWinForDriverAsync(driverId, finalWin);
            await _driverInfoRepository.SaveChangesAsync();

            var createdWinToReturn = _mapper.Map<Models.WinDto>(finalWin);

            return CreatedAtRoute("GetWin",
                new
                {
                    driverId =driverId,
                    winId = createdWinToReturn.Id
                },
                createdWinToReturn);
        }

        [HttpPut("{winid}")]
        public async Task<ActionResult> UpdateWin(int driverId, int winId, WinForUpdateDto win)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(driverId))
            {
                _logger.LogInformation($"Driver with id {driverId} wasn't found.");

                return NotFound();
            }

            var winEntity = await _driverInfoRepository.GetWinForDriverAsync(driverId, winId);

            if (winEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(win, winEntity);

            await _driverInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{winid}")]
        public async Task<ActionResult> PartiallyUpdateWin(int driverId, int winId, JsonPatchDocument<WinForUpdateDto> patchDocument)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(driverId))
            {
                _logger.LogInformation($"Driver with id {driverId} wasn't found.");

                return NotFound();
            }

            var winEntity = await _driverInfoRepository.GetWinForDriverAsync(driverId, winId);

            if (winEntity == null)
            {
                return NotFound();
            }

            var winToPatch = _mapper.Map<WinForUpdateDto>(winEntity);

            patchDocument.ApplyTo(winToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(winToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(winToPatch, winEntity);

            await _driverInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{winId}")]
        public async Task<ActionResult> DeleteWin(int driverId, int winId)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(driverId))
            {
                _logger.LogInformation($"Driver with id {driverId} wasn't found.");

                return NotFound();
            }

            var winEntity = await _driverInfoRepository.GetWinForDriverAsync(driverId, winId);

            if (winEntity == null)
            {
                return NotFound();
            }

            _driverInfoRepository.DeleteWin(winEntity);
            await _driverInfoRepository.SaveChangesAsync();

            _mailService.Send("Win deleted.", $"Win {winEntity.Name} with id {winEntity.Id} was deleted.");

            return NoContent();
        }
    }
}
