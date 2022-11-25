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

        /// <summary>
        /// Method for inserting a machine object to db
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        public async Task Create(Machine machine)
        {
            await _machineCollection.InsertOneAsync(machine);
        }

        /// <summary>
        /// Method for getting all machines from db
        /// </summary>
        /// <returns>Enumerable of machines</returns>
        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            var result = await _machineCollection.Find(_ => true).ToListAsync();

            return result;
        }

        /// <summary>
        /// Method for getting machine by id
        /// </summary>
        /// <param name="id">Id of machine</param>
        /// <returns>Object of type machine</returns>
        public async Task<Machine> GetById(string id)
        {
            var result = await _machineCollection
                    .Find(x => x.Id.Equals(id))
                    .FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Method for updating a machine in database
        /// </summary>
        /// <param name="machine">Object of type machine to be updated</param>
        /// <returns></returns>
        public async Task Update(Machine machine)
        {
            await _machineCollection.ReplaceOneAsync(x => x.Id.Equals(machine.Id), machine);
        }
    }
}
