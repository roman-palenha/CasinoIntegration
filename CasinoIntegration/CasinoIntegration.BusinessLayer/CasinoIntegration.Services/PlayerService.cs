using AutoMapper;
using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
using CasinoIntegration.BusinessLayer.CasinoIntegrationDTO;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces;

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

        public async Task<SpinResult> ConfirmResultBet(int[] resultArray, double afterBetBalance, double win, string username)
        {
            var afterWinBalance = afterBetBalance + win;
            await UpdateBalanceAsync(username, afterWinBalance);

            return new SpinResult { Slots = resultArray, Balance = afterWinBalance, Win = win };
        }

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

        public async Task<Player> GetByNameAsync(string username)
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
