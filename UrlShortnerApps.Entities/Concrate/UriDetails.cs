using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UrlShortnerApps.Entities.Abstract;

namespace UrlShortnerApps.Entities.Concrate
{
    public class UriDetails : IEntities
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("originalurl")]
        public string originalurl { get; set; }

        [BsonElement("shortnerurl")]
        public string shortnerurl { get; set; }

        [BsonElement("createdate")]
        public DateTime createdate { get; set; }

        [BsonElement("ticketcount")]
        public int ticketcount { get; set; }


    }
}
