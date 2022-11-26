using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CasinoIntegration.DataAccessLayer.Entities
{
    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int SlotSize { get; set; }
    }
}
