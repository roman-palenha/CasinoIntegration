using CasinoIntegration.BusinessLayer.DTO.Request;
using CasinoIntegration.BusinessLayer.DTO.Response;
using CasinoIntegration.DataAccessLayer.Entities;

namespace CasinoIntegration.BusinessLayer.Services.Interfaces
{
    public interface IPlayerService
    {
        /// <summary>
        /// Method to get player by name
        /// </summary>
        /// <param name="username">Player`s username</param>
        /// <returns>Found player</returns>
        Task<Player> GetByNameAsync(string username);

        /// <summary>
        /// Method to get all players
        /// </summary>
        /// <returns>List of players</returns>
        Task<IEnumerable<Player>> GetAllAsync();

        /// <summary>
        /// Method to create player
        /// </summary>
        /// <param name="playerDto">Player`s dto</param>
        /// <returns>Created player</returns>
        /// <exception cref="ArgumentException">Exception if player is not valid</exception>
        Task<Player> CreateAsync(PlayerDTO playerDto);

        /// <summary>
        /// Method to update player`s balance
        /// </summary>
        /// <param name="username">Needed player`s username</param>
        /// <param name="balance">New balance of player</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Exception if params are not valid</exception>
        Task UpdateBalanceAsync(string username, PlayerBalanceDTO playerBalance);


        /// <summary>
        /// Method to delete a player
        /// </summary>
        /// <param name="player">Player to delete</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Exception if there is not a such player</exception>
        Task DeleteAsync(Player player);


        /// <summary>
        /// Method to make a bet
        /// </summary>
        /// <param name="username">Player`s username</param>
        /// <param name="bet">Player`s bet</param>
        /// <returns>Balance after bet</returns>
        /// <exception cref="InvalidDataException">Exception if user is not found</exception>
        /// <exception cref="InvalidOperationException">Exception if balance is negative</exception>
        Task<double> Bet(string username, double bet);

        /// <summary>
        /// Confirming result of player`s bet
        /// </summary>
        /// <param name="resultArray">Our resut array of machine</param>
        /// <param name="afterBetBalance">Balance after bet</param>
        /// <param name="win">Player`s win</param>
        /// <param name="username">Username of player</param>
        /// <returns>Spin result of player</returns>
        Task<SpinResult> ConfirmResultBet(int[] resultArray, double balanceWithBet, double win, string username);
    }
}
