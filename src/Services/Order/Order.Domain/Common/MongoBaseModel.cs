using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Order.Domain.Common
{
    public class MongoBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
