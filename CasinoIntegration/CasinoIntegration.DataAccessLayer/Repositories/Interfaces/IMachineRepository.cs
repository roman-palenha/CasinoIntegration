using CasinoIntegration.DataAccessLayer.Entities;

namespace CasinoIntegration.DataAccessLayer.Repositories.Interfaces
{
    public interface IMachineRepository
    {

        /// <summary>
        /// Method for getting machine by id
        /// </summary>
        /// <param name="id">Machine Id</param>
        /// <returns>Machine</returns>
        Task<Machine> GetById(string id);

        /// <summary>
        /// Method for inserting a machine object to db
        /// </summary>
        /// <param name="machine">Machine to craete</param>
        /// <returns></returns>
        Task Create(Machine machine);

        /// <summary>
        /// Method for updating a machine in database
        /// </summary>
        /// <param name="machine">Machine to update</param>
        /// <returns></returns>
        Task Update(Machine machine);

        /// <summary>
        /// Method for getting all machines from db
        /// </summary>
        /// <returns>Enumerable of machines</returns>
        Task<IEnumerable<Machine>> GetAllAsync();
    }
}
