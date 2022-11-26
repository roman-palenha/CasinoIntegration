using CasinoIntegration.DataAccessLayer.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.Entities;
using CasinoIntegration.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CasinoIntegration.DataAccessLayer.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly IMongoCollection<Machine> _machineCollection;

        public MachineRepository(
         IOptions<CasinoIntegrationDatabaseSettings> casinoIntegrationDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                casinoIntegrationDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                casinoIntegrationDatabaseSettings.Value.DatabaseName);

            _machineCollection = mongoDatabase.GetCollection<Machine>(
            casinoIntegrationDatabaseSettings.Value.MachinesCollectionName);
        }

        public async Task Create(Machine machine)
        {
            if(machine == null)
                throw new ArgumentNullException(nameof(machine));

            await _machineCollection.InsertOneAsync(machine);
        }

       
        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            var result = await _machineCollection.Find(_ => true).ToListAsync();

            return result;
        }

      
        public async Task<Machine> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id can`t be null or white space");

            var result = await _machineCollection
                    .Find(x => x.Id.Equals(id))
                    .FirstOrDefaultAsync();

            return result;
        }

       
        public async Task Update(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            await _machineCollection.ReplaceOneAsync(x => x.Id.Equals(machine.Id), machine);
        }
    }
}
