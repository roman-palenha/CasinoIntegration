using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities
{
    public class Player
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
    }
}
