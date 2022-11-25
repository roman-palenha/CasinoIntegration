using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        /// <summary>
        /// Get player from db by name
        /// </summary>
        /// <param name="username">Player`s username</param>
        /// <returns>Found player</returns>
        Task<Player> GetByNameAsync(string username);

        /// <summary>
        /// Get all players from db
        /// </summary>
        /// <returns>List of players</returns>
        Task<IEnumerable<Player>> GetAllAsync();

        /// <summary>
        /// Create player in db
        /// </summary>
        /// <param name="player">Player to create</param>
        /// <returns></returns>
        Task CreateAsync(Player player);


        /// <summary>
        /// Update player in db
        /// </summary>
        /// <param name="player">Player to update</param>
        /// <returns></returns>
        Task UpdateAsync(Player player);

        /// <summary>
        /// Delete player in db
        /// </summary>
        /// <param name="player">Player to delete</param>
        /// <returns></returns>
        Task DeleteAsync(Player player);
    }
}
