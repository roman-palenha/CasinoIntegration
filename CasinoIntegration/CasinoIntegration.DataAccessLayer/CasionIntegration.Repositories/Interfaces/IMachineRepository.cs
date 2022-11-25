using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;

namespace CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces
{
    public interface IMachineRepository
    {

        /// <summary>
        /// Method for getting machine by id
        /// </summary>
        /// <param name="id">Id of machine</param>
        /// <returns>Object of type machine</returns>
        Task<Machine> GetById(string id);

        /// <summary>
        /// Method for inserting a machine object to db
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        Task Create(Machine machine);

        /// <summary>
        /// Method for updating a machine in database
        /// </summary>
        /// <param name="machine">Object of type machine to be updated</param>
        /// <returns></returns>
        Task Update(Machine machine);

        /// <summary>
        /// Method for getting all machines from db
        /// </summary>
        /// <returns>Enumerable of machines</returns>
        Task<IEnumerable<Machine>> GetAllAsync();
    }
}
