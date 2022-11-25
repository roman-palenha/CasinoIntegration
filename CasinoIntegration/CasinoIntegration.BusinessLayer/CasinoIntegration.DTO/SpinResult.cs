using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.BusinessLayer.CasinoIntegrationDTO
{
    public class SpinResult
    {
        public int[] Slots { get; set; }
        public double Balance { get; set; }
        public double Win { get; set; }
    }
}
