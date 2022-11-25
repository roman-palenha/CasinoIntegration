using AutoMapper;
using CasinoIntegration.BusinessLayer.CasinoIntegration.DTO.Request;
using CasinoIntegration.BusinessLayer.CasinoIntegration.DTO.Response;
using CasinoIntegration.BusinessLayer.CasinoIntegration.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Repositories.Interfaces;

namespace CasinoIntegration.BusinessLayer.CasinoIntegration.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playersRepository;
        private readonly IMapper _mapper;

        public PlayerService(
           IPlayerRepository playersRepository, IMapper mapper)
        {
            _playersRepository = playersRepository ?? throw new ArgumentNullException(nameof(playersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SpinResult> ConfirmResultBet(int[] resultArray, double balanceWithBet, double win, string username)
        {
            var afterWinBalance = balanceWithBet + win;
            await UpdateBalanceAsync(username, afterWinBalance);

            return new SpinResult { Slots = resultArray, Balance = afterWinBalance, Win = win };
        }

        public async Task<double> Bet(string username, double bet)
        {
            var user = await _playersRepository.GetByNameAsync(username);
            if (user == null)
            {
                throw new InvalidDataException($"There are no users with the username: {username}");
            }

            var newBalance = user.Balance - bet;
            if (newBalance < 0)
            {
                throw new InvalidOperationException("User cannot have a negative balance");
            }

            return newBalance;
        }

        public async Task<Player> CreateAsync(PlayerDTO playerDto)
        {
            if (await GetByNameAsync(playerDto.UserName) != null)
                throw new ArgumentException($"There are no users with the username: {playerDto.UserName}");

            if (playerDto.Balance < 0)
                throw new ArgumentException("User cannot have a negative balance");

            var player = _mapper.Map<Player>(playerDto);
            await _playersRepository.CreateAsync(player);
            return player;
        }

        public async Task DeleteAsync(Player player)
        {
            if (GetByNameAsync(player.UserName) == null)
                throw new ArgumentException($"There are no users with the username: {player.UserName}");

            await _playersRepository.DeleteAsync(player);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            var result = await _playersRepository.GetAllAsync();

            return result;
        }

        public async Task<Player> GetByNameAsync(string username)
        { 
            var result = await _playersRepository.GetByNameAsync(username);

            return result;
        }

        public async Task UpdateBalanceAsync(string username, double balance)
        {
            var player = await GetByNameAsync(username);
            if (player == null)
                throw new ArgumentException($"There are no users with the username: {username}");

            if (balance < 0)
                throw new ArgumentException("User cannot have a negative balance");

            player.Balance = balance;

            await _playersRepository.UpdateAsync(player);
        }
    }
}
