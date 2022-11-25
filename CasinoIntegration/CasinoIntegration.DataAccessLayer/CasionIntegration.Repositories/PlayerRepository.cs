using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMongoCollection<Player> _playersCollection;

        public PlayerRepository(
          IOptions<CasinoIntegrationDatabaseSettings> casinoIntegrationDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                casinoIntegrationDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                casinoIntegrationDatabaseSettings.Value.DatabaseName);

            _playersCollection = mongoDatabase.GetCollection<Player>(
            casinoIntegrationDatabaseSettings.Value.PlayersCollectionName);
        }

        /// <summary>
        /// Create player in db
        /// </summary>
        /// <param name="player">Player to create</param>
        /// <returns></returns>
        public async Task CreateAsync(Player player)
        {
            await _playersCollection.InsertOneAsync(player);
        }

        /// <summary>
        /// Delete player in db
        /// </summary>
        /// <param name="player">Player to delete</param>
        /// <returns></returns>
        public async Task DeleteAsync(Player player)
        {
            await _playersCollection.DeleteOneAsync(x => x.UserName.ToLower().Equals(player.UserName.ToLower()));
        }

        /// <summary>
        /// Get all players from db
        /// </summary>
        /// <returns>List of players</returns>
        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _playersCollection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Get player from db by name
        /// </summary>
        /// <param name="username">Player`s username</param>
        /// <returns>Found player</returns>
        public async Task<Player> GetByNameAsync(string username)
        {
            var foundUsers = await _playersCollection.FindAsync(x => x.UserName.Equals(username));
            var result = await foundUsers.FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Update player in db
        /// </summary>
        /// <param name="player">Player to update</param>
        /// <returns></returns>
        public async Task UpdateAsync(Player player)
        {
            await _playersCollection.ReplaceOneAsync(x => x.UserName.Equals(player.UserName), player);
        }
    }
}
