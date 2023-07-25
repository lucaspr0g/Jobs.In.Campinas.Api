﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastucture.Repository.Collections
{
    public sealed class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }
    }
}