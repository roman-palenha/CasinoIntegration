using CasinoIntegration.DataAccessLayer.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.Entities;
using CasinoIntegration.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;

namespace CasinoIntegration.DataAccessLayer.Repositories
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
            if(player == null)
                throw new ArgumentNullException(nameof(player));

            await _playersCollection.InsertOneAsync(player);
        }

        public async Task DeleteAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            await _playersCollection.DeleteOneAsync(x => x.UserName.ToLower().Equals(player.UserName.ToLower()));
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _playersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Player> GetByNameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username can`t be null or white space");

            var foundUsers = await _playersCollection.FindAsync(x => x.UserName.Equals(username));
            var result = await foundUsers.FirstOrDefaultAsync();

            return result;
        }

        public async Task UpdateAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            await _playersCollection.ReplaceOneAsync(x => x.UserName.Equals(player.UserName), player);
        }
    }
}
