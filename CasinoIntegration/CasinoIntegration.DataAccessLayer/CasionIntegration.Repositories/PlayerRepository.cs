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

        public async Task CreateAsync(Player player)
        {
            await _playersCollection.InsertOneAsync(player);
        }

        public async Task DeleteAsync(Player player)
        {
            await _playersCollection.DeleteOneAsync(x => x.UserName.ToLower().Equals(player.UserName.ToLower()));
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _playersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Player> GetByNameAsync(string username)
        {
            var foundUsers = await _playersCollection.FindAsync(x => x.UserName.Equals(username));
            var result = await foundUsers.FirstOrDefaultAsync();

            return result;
        }

        public async Task UpdateAsync(Player player)
        {
            await _playersCollection.ReplaceOneAsync(x => x.UserName.Equals(player.UserName), player);
        }
    }
}
