using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.BusinessLayer.DTO.Request
{
    public class BetDTO
    {
        public string MachineId { get; set; }
        public double Bet { get; set; }
    }
}
