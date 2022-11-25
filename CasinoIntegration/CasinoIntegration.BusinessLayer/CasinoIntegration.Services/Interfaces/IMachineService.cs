using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces
{
    public interface IMachineService
    {
        /// <summary>
        /// Method for changing machine SlotSize parameter
        /// </summary>
        /// <param name="id">the id of machine to be changed</param>
        /// <param name="newSize">the new size of machine</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Task ChangeMachineSlotsSize(string id, int newSize);

        /// <summary>
        ///Method for getting the machines size of slots array, creates an array of size SlotsSize, and fills it waith random integers (0-9) for each slot
        /// </summary>
        /// <param name="machine">A machine object from which to get the SlotsSize parameter</param>
        /// <returns>an array filled with random integers</returns>
        int[] ReturnSlotsArray(Machine machine);

        /// <summary>
        /// A method for getting machine by id
        /// </summary>
        /// <param name="id">the id of machine</param>
        /// <returns>Machine with provided id</returns>
        Task<Machine> GetById(string id);

        /// <summary>
        /// Method for creating a machine 
        /// </summary>
        /// <param name="machine">An object of type machine</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task Create(Machine machine);

        /// <summary>
        /// Method for getting all machines
        /// </summary>
        /// <returns>Enumerabe of all machines in db</returns>
        Task<IEnumerable<Machine>> GetAllAsync();

        /// <summary>
        /// A method for taking bet of sum double bet, on machine with id = machineId
        /// </summary>
        /// <param name="machineId">machine id on which to place the bet</param>
        /// <param name="bet">the sum of bet to be placed on machine</param>
        /// <returns>Tuple with the resulting array after spin of machine, and users win</returns>
        Task<(int[],double)> TakeBet(string machineId, double bet);
    }
}
