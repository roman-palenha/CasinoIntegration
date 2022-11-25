using CasinoIntegration.BusinessLayer.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.Entities;
using CasinoIntegration.DataAccessLayer.Repositories.Interfaces;

namespace CasinoIntegration.BusinessLayer.Services
{
    public class MachineService : IMachineService
    {
        private const int MaxSpinValue = 9;
        private readonly IMachineRepository _machineRepository;
        
        public MachineService(
          IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
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

        public async Task ChangeSlotsSize(string id, int newSize)
        {
            if (newSize < 0)
            {
                throw new ArgumentOutOfRangeException("Slot size must be equal or greater than zero");
            }

            var machine = await GetById(id);

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

        private int[] ReturnSlotsArray(Machine machine)
        {
            Random randNum = new Random();

            int[] slotsArray = Enumerable
                .Repeat(0, machine.SlotsSize)
                .Select(i => randNum.Next(0, MaxSpinValue))
                .ToArray();

            return slotsArray;
        }
    }
}
