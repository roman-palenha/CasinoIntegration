namespace CasinoIntegration.DataAccessLayer.DatabaseSettings.Interfaces
{
    public interface ICasinoIntegrationDatabaseSettings
    {
        public string PlayersCollectionName { get; set; }
        public string MachinesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
