using CasinoIntegration.BusinessLayer.CasinoIntegrationDTO;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<Player> GetByNameAsync(string username);
        Task<IEnumerable<Player>> GetAllAsync();
        Task CreateAsync(Player player);
        Task UpdateBalanceAsync(string username, double balance);
        Task DeleteAsync(Player player);
        Task<double> Bet(string username, double bet);
        Task<SpinResult> ConfirmResultBet(int[] resultArray, double balanceWithBet, double win, string username);
    }
}
