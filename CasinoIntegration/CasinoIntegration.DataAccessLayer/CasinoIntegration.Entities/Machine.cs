﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities
{
    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int SlotsSize { get; set; }
    }
}
