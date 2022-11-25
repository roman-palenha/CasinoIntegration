using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings
{
    public class SlotMachineDatabaseSettings : ISlotMachineDatabaseSettings
    {
        public string PlayersCollectionName { get; set; }
        public string MachinesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    
}
