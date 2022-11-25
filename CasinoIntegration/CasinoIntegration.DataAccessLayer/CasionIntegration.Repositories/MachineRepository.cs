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

namespace CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories
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
            await _machineCollection.InsertOneAsync(machine);
        }

       
        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            var result = await _machineCollection.Find(_ => true).ToListAsync();

            return result;
        }

      
        public async Task<Machine> GetById(string id)
        {
            var result = await _machineCollection
                    .Find(x => x.Id.Equals(id))
                    .FirstOrDefaultAsync();

            return result;
        }

       
        public async Task Update(Machine machine)
        {
            await _machineCollection.ReplaceOneAsync(x => x.Id.Equals(machine.Id), machine);
        }
    }
}
