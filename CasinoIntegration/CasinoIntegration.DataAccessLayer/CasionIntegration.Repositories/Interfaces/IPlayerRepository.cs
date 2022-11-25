using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.DataAccessLayer.CasionIntegration.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> GetByNameAsync(string username);
        Task<IEnumerable<Player>> GetAllAsync();
        Task CreateAsync(Player player);
        Task UpdateAsync(Player player);
        Task DeleteAsync(Player player);
    }
}
