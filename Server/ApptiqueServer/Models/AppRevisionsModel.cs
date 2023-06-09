﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApptiqueServer.Models
{
    public class AppRevisionsModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public int AppVersion { get; set; }
        public string ApkPath { get; set; }
        public string AppVersionName { get; set; }
        public string ChangeSet { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
