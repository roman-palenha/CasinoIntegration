using CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings.Interfaces;

namespace CasinoIntegration.DataAccessLayer.CasinoIntegration.DatabaseSettings
{
    public class CasinoIntegrationDatabaseSettings : ICasinoIntegrationDatabaseSettings
    {
        public string PlayersCollectionName { get; set; }
        public string MachinesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}
