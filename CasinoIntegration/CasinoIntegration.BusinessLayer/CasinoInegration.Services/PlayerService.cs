﻿using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
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

        public PlayerService(
           IPlayerRepository playersRepository)
        {
            _playersRepository = playersRepository;
        }

        public async Task<SpinResult> ConfirmResultBet(int[] resultArray, double beforeBetBalance, double win, string username)
        {
            var afterWinBalance = beforeBetBalance + win;
            await UpdateBalanceAsync(username, afterWinBalance);

            return new SpinResult { Slots = resultArray, Balance = afterWinBalance, Win = win };
        }

        public async Task<double> Bet(string username, double bets)
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

        public async Task CreateAsync(Player player)
        {
            if (await GetByNameAsync(player.UserName) != null)
                throw new ArgumentException("There is a user with same username");

            if (player.Balance < 0)
                throw new ArgumentException("Cannot be a user with negative balance");

            await _playersRepository.CreateAsync(player);
        }

        public async Task DeleteAsync(Player player)
        {
            if (GetByNameAsync(player.UserName) == null)
                throw new ArgumentException("There is not a user with same username");

            await _playersRepository.DeleteAsync(player);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            var result = await _playersRepository.GetAllAsync();

            return result;
        }

        public async Task<Player?> GetByNameAsync(string username)
        { 
            var result = await _playersRepository.GetByNameAsync(username);

            return result;
        }

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
