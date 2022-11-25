﻿using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
using CasinoIntegration.BusinessLayer.CasinoIntegrationDTO;
using Microsoft.AspNetCore.Mvc;

namespace CasinoIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMachineService _machineService;

        public PlayerController(IPlayerService playerService, IMachineService machineService)
        {
            _playerService = playerService;
            _machineService = machineService;
        }   

        /// <summary>
        /// Action for getting all players
        /// </summary>
        /// <returns>List of players</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _playerService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Action for registering new players
        /// </summary>
        /// <param name="registerPlayer">New player to register</param>
        /// <returns>Registered player</returns>
        [HttpPost]
        public async Task<IActionResult> Register(PlayerDTO registerPlayer)
        {
            var player = await _playerService.CreateAsync(registerPlayer);
            return Ok(player);
        }

        /// <summary>
        /// Action for updating player`s balance
        /// </summary>
        /// <param name="username">Username of player</param>
        /// <param name="balance">New balance of player</param>
        /// <returns>Ok</returns>
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateBalance(string username, [FromBody] double balance)
        {
            await _playerService.UpdateBalanceAsync(username, balance);
            return Ok();
        }

        /// <summary>
        /// Action for making bet
        /// </summary>
        /// <param name="username">Player with username is making bet</param>
        /// <param name="bet">Sum of bet</param>
        /// <param name="machineId">MachineId to process bet</param>
        /// <returns>Spin Result of bet</returns>
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
