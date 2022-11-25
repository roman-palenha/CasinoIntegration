using CasinoIntegration.DataAccessLayer.DatabaseSettings.Interfaces;

namespace CasinoIntegration.DataAccessLayer.DatabaseSettings
{
    public class CasinoIntegrationDatabaseSettings : ICasinoIntegrationDatabaseSettings
    {
        public string PlayersCollectionName { get; set; }
        public string MachinesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}
