using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces
{
    public interface IMachineRepository
    {
        Task<Machine> GetById(string id);
        Task Create(Machine machine);
        Task Update(Machine machine);
        Task<IEnumerable<Machine>> GetAllAsync();
    }
}
