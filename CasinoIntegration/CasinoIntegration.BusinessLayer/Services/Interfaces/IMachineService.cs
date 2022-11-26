using CasinoIntegration.BusinessLayer.DTO.Models;
using CasinoIntegration.BusinessLayer.DTO.Request;
using CasinoIntegration.DataAccessLayer.Entities;

namespace CasinoIntegration.BusinessLayer.Services.Interfaces
{
    public interface IMachineService
    {
        /// <summary>
        /// Method for changing machine SlotSize parameter
        /// </summary>
        /// <param name="id">MachineId</param>
        /// <param name="machineSlotSize">MachineSlotSize</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Task ChangeSlotsSize(string id, MachineSlotSizeDTO machineSlotSize);

        /// <summary>
        /// A method for getting machine by id
        /// </summary>
        /// <param name="id">MachineId</param>
        /// <returns>Machine</returns>
        Task<Machine> GetById(string id);

        /// <summary>
        /// Method for creating a machine 
        /// </summary>
        /// <param name="machineDto">Machine</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task Create(MachineDTO machineDto);

        /// <summary>
        /// Method for getting all machines
        /// </summary>
        /// <returns>Enumerabe of machines</returns>
        Task<IEnumerable<Machine>> GetAllAsync();

        /// <summary>
        /// A method for taking bet of sum double bet, on machine with id = machineId
        /// </summary>
        /// <param name="machineId">MachineId</param>
        /// <param name="bet">Bet</param>
        /// <returns>BetResult</returns>
        Task<BetResult> TakeBet(string machineId, double bet);
    }
}
