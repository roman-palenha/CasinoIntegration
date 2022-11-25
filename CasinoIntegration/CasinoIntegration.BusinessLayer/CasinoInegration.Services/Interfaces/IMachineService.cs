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
        Task ChangeMachineSlotsSize(string id, int newSize);
        int[] ReturnSlotsArray(Machine machine);
        Task<Machine> GetById(string id);
        Task Create(Machine machine);
        Task<IEnumerable<Machine>> GetAllAsync();
        Task<(int[],double)> TakeBet(string machineId, double bet);
    }
}
