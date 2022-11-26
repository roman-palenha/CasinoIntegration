using AutoMapper;
using CasinoIntegration.BusinessLayer.DTO.Models;
using CasinoIntegration.BusinessLayer.DTO.Request;
using CasinoIntegration.BusinessLayer.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.Entities;
using CasinoIntegration.DataAccessLayer.Repositories.Interfaces;

namespace CasinoIntegration.BusinessLayer.Services
{
    public class MachineService : IMachineService
    {
        private const int MaxSpinValue = 9;
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;
        public MachineService(
          IMachineRepository machineRepository, IMapper mapper)
        {
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BetResult> TakeBet(string machineId, double bet)
        {
            var machine = await _machineRepository.GetById(machineId);
            if (machine == null)
                throw new InvalidDataException($"There are no machine with the id: {machineId}");

            var resultArray = ReturnSlotsArray(machine);
            if (resultArray.Length <= 0)
                throw new InvalidDataException("Slot array can`t have a size equal or lower than 0");

            var firstNumFromArray = resultArray[0];

            var win = resultArray
                .TakeWhile(x => x == firstNumFromArray)
                .Sum(x => x)    
                * bet;

            return new BetResult { ResultArray = resultArray, Win = win};
        }

        public async Task ChangeSlotsSize(string id, MachineSlotSizeDTO machineSlotSize)
        {
            if (machineSlotSize == null)
                throw new ArgumentNullException(nameof(machineSlotSize));

            var slotSize = machineSlotSize.SlotSize;
            if (slotSize < 0)
                throw new ArgumentOutOfRangeException("Slot size must be equal or greater than zero");

            var machine = await GetById(id);
            if(machine == null)
                throw new InvalidDataException($"There are no machine with the id: {id}");

            machine.SlotSize = slotSize;

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

        public async Task Create(MachineDTO machineDto)
        {
            if (machineDto == null || machineDto.SlotSize < 0 || string.IsNullOrWhiteSpace(machineDto.Name))
                throw new ArgumentException("Wrong machine data");

            var machine = _mapper.Map<Machine>(machineDto);
            await _machineRepository.Create(machine);
        }

        private int[] ReturnSlotsArray(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));
            
            var randNum = new Random();

            var slotsArray = Enumerable
                .Repeat(0, machine.SlotSize)
                .Select(i => randNum.Next(0, MaxSpinValue))
                .ToArray();

            return slotsArray;
        }
    }
}
