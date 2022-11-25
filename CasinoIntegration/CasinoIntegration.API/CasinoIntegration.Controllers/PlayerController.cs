using AutoMapper;
using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
using CasinoIntegration.BusinessLayer.CasinoIntegrationDTO;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CasinoIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMachineService _machineService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMachineService machineService, IMapper mapper)
        {
            _playerService = playerService;
            _machineService = machineService;
            _mapper = mapper;
        }   

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _playerService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(PlayerDTO registerPlayer)
        {
            try
            {
                var player = _mapper.Map<Player>(registerPlayer);
                await _playerService.CreateAsync(player);
                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateBalance(string username, [FromBody] double balance)
        {
            try
            {
                await _playerService.UpdateBalanceAsync(username, balance);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{username}/{machineId}/{bet}")]
        public async Task<IActionResult> Bet([FromRoute] string username, double bet, string machineId)
        {
            var user = await _playerService.GetByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var newBalance = user.Balance - bet;
            if (newBalance < 0)
            {
                return BadRequest("Negative balance");
            }

            var machine = await _machineService.GetById(machineId);

            var resultArray = _machineService.ReturnSlotsArray(machine);

            var firstNumFromArray = resultArray[0];

            var win = resultArray
                .TakeWhile(x => x == firstNumFromArray)
                .Sum(x => x)
                * bet;

            newBalance += win;

            await _playerService.UpdateBalanceAsync(username, newBalance);

            var res = new SpinResult { Slots = resultArray, Balance = newBalance, Win = win };

            return Ok(res);
        }
    }
}
