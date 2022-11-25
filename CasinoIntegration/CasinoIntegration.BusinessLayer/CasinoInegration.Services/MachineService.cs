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

        public int[] ReturnSlotsArray(Machine machine)
        {
            Random randNum = new Random();

            int[] slotsArray = Enumerable
                .Repeat(0, machine.SlotsSize)
                .Select(i => randNum.Next(0, 9))
                .ToArray();

            return slotsArray;
        }

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

        public async Task<Machine> GetById(string id)
        {
            var result = await _machineRepository.GetById(id);

            return result;
        }

        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            var result = await _machineRepository.GetAllAsync();

            return result;
        }

        public async Task Create(Machine machine)
        {
            if (machine == null || machine.SlotsSize < 0)
                throw new ArgumentException("Wrong machine data");

            await _machineRepository.Create(machine);
        }
    }
}
