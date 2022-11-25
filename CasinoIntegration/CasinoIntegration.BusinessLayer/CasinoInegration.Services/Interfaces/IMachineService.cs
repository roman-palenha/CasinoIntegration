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
        Task changeMachineSlotsSize(string id, int newSize);
        int[] ReturnSlotsArray(Machine game);
        Task<Machine> GetById(string id);
        Task Create(Machine game);
        Task<IEnumerable<Machine>> GetAllAsync();
    }
}
