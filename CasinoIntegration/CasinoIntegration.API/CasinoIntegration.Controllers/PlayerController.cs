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
            var player = _mapper.Map<Player>(registerPlayer);
            await _playerService.CreateAsync(player);
            return Ok(player);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateBalance(string username, [FromBody] double balance)
        {
            await _playerService.UpdateBalanceAsync(username, balance);
            return Ok();
        }

        [HttpGet]
        [Route("{username}/{machineId}/{bet}")]
        public async Task<IActionResult> Bet([FromRoute] string username, double bet, string machineId)
        {
            var balanceWithBet = await _playerService.Bet(username, bet);
            var betResult = await _machineService.TakeBet(machineId, bet);
            var result = await _playerService.ConfirmResultBet(betResult.Item1, balanceWithBet, betResult.Item2, username);

            return Ok(result);
        }
    }
}
