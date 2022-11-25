using AutoMapper;
using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
using CasinoIntegration.BusinessLayer.CasinoIntegrationDTO;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.BusinessLayer.CasinoInegration.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playersRepository;
        private readonly IMapper _mapper;

        public PlayerService(
           IPlayerRepository playersRepository, IMapper mapper)
        {
            _playersRepository = playersRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Confirming result of player`s bet
        /// </summary>
        /// <param name="resultArray">Our resut array of machine</param>
        /// <param name="afterBetBalance">Balance after bet</param>
        /// <param name="win">Player`s win</param>
        /// <param name="username">Username of player</param>
        /// <returns>Spin result of player</returns>
        public async Task<SpinResult> ConfirmResultBet(int[] resultArray, double afterBetBalance, double win, string username)
        {
            var afterWinBalance = afterBetBalance + win;
            await UpdateBalanceAsync(username, afterWinBalance);

            return new SpinResult { Slots = resultArray, Balance = afterWinBalance, Win = win };
        }

        /// <summary>
        /// Method to make a bet
        /// </summary>
        /// <param name="username">Player`s username</param>
        /// <param name="bet">Player`s bet</param>
        /// <returns>Balance after bet</returns>
        /// <exception cref="InvalidDataException">Exception if user is not found</exception>
        /// <exception cref="InvalidOperationException">Exception if balance is negative</exception>
        public async Task<double> Bet(string username, double bet)
        {
            var user = await _playersRepository.GetByNameAsync(username);
            if (user == null)
            {
                throw new InvalidDataException("User not found");
            }

            var newBalance = user.Balance - bet;
            if (newBalance < 0)
            {
                throw new InvalidOperationException("Negative balance");
            }

            return newBalance;
        }

        /// <summary>
        /// Method to create player
        /// </summary>
        /// <param name="playerDto">Player`s dto</param>
        /// <returns>Created player</returns>
        /// <exception cref="ArgumentException">Exception if player is not valid</exception>
        public async Task<Player> CreateAsync(PlayerDTO playerDto)
        {
            if (await GetByNameAsync(playerDto.UserName) != null)
                throw new ArgumentException("There is a user with same username");

            if (playerDto.Balance < 0)
                throw new ArgumentException("Cannot be a user with negative balance");

            var player = _mapper.Map<Player>(playerDto);
            await _playersRepository.CreateAsync(player);
            return player;
        }

        /// <summary>
        /// Method to delete a player
        /// </summary>
        /// <param name="player">Player to delete</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Exception if there is not a such player</exception>
        public async Task DeleteAsync(Player player)
        {
            if (GetByNameAsync(player.UserName) == null)
                throw new ArgumentException("There is not a user with same username");

            await _playersRepository.DeleteAsync(player);
        }

        /// <summary>
        /// Method to get all players
        /// </summary>
        /// <returns>List of players</returns>
        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            var result = await _playersRepository.GetAllAsync();

            return result;
        }

        /// <summary>
        /// Method to get player by name
        /// </summary>
        /// <param name="username">Player`s username</param>
        /// <returns>Found player</returns>
        public async Task<Player> GetByNameAsync(string username)
        { 
            var result = await _playersRepository.GetByNameAsync(username);

            return result;
        }

        /// <summary>
        /// Method to update player`s balance
        /// </summary>
        /// <param name="username">Needed player`s username</param>
        /// <param name="balance">New balance of player</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Exception if params are not valid</exception>
        public async Task UpdateBalanceAsync(string username, double balance)
        {
            var player = await GetByNameAsync(username);
            if (player == null)
                throw new ArgumentException("There is not a user with same username");

            if (balance < 0)
                throw new ArgumentException("Negative balance");

            player.Balance = balance;

            await _playersRepository.UpdateAsync(player);
        }
    }
}
