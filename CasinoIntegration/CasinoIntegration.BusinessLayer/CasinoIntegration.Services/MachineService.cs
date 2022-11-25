using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces;

namespace CasinoIntegration.BusinessLayer.CasinoInegration.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(
          IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        /// <summary>
        /// A method for taking bet of sum double bet, on machine with id = machineId
        /// </summary>
        /// <param name="machineId">machine id on which to place the bet</param>
        /// <param name="bet">the sum of bet to be placed on machine</param>
        /// <returns>Tuple with the resulting array after spin of machine, and users win</returns>
        public async Task<(int[], double)> TakeBet(string machineId, double bet)
        {
            var machine = await _machineRepository.GetById(machineId);

            var resultArray = ReturnSlotsArray(machine);

            var firstNumFromArray = resultArray[0];

            var win = resultArray
                .TakeWhile(x => x == firstNumFromArray)
                .Sum(x => x)
                * bet;

            return (resultArray,win);
        }

        /// <summary>
        ///Method for getting the machines size of slots array, creates an array of size SlotsSize, and fills it waith random integers (0-9) for each slot
        /// </summary>
        /// <param name="machine">A machine object from which to get the SlotsSize parameter</param>
        /// <returns>an array filled with random integers</returns>
        public int[] ReturnSlotsArray(Machine machine)
        {
            Random randNum = new Random();

            int[] slotsArray = Enumerable
                .Repeat(0, machine.SlotsSize)
                .Select(i => randNum.Next(0, 9))
                .ToArray();

            return slotsArray;
        }

        /// <summary>
        /// Method for changing machine SlotSize parameter
        /// </summary>
        /// <param name="id">the id of machine to be changed</param>
        /// <param name="newSize">the new size of machine</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task ChangeMachineSlotsSize(string id, int newSize)
        {
            var machine = await GetById(id);

            if (newSize < 0)
            {
                throw new ArgumentOutOfRangeException("Size cant be < 0");
            }

            machine.SlotsSize = newSize;

            await _machineRepository.Update(machine);
        }

        /// <summary>
        /// A method for getting machine by id
        /// </summary>
        /// <param name="id">the id of machine</param>
        /// <returns>Machine with provided id</returns>
        public async Task<Machine> GetById(string id)
        {
            var result = await _machineRepository.GetById(id);

            return result;
        }

        /// <summary>
        /// Method for getting all machines
        /// </summary>
        /// <returns>Enumerabe of all machines in db</returns>
        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            var result = await _machineRepository.GetAllAsync();

            return result;
        }

        /// <summary>
        /// Method for creating a machine 
        /// </summary>
        /// <param name="machine">An object of type machine</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Create(Machine machine)
        {
            if (machine == null || machine.SlotsSize < 0)
                throw new ArgumentException("Wrong machine data");

            await _machineRepository.Create(machine);
        }
    }
}
