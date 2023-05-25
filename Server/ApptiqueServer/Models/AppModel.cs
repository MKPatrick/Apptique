using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApptiqueServer.Models
{
    public class AppModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        [Required]
        public string AppName { get; set; }
        public string PackageName { get; set; }
        public string AppIcon { get; set; }
        public string Description { get; set; }
        public List<AppRevisionsModel> Revisions { get; set; } = new List<AppRevisionsModel>();

        public int CurrentReleaseVersion
        {
            get
            {
                return Revisions.Count > 0 ? Revisions.OrderBy(x => x.AppVersion).Last().AppVersion : 0;
            }
        }
        public DateTime LastReleaseDate
        {
            get
            {
                return Revisions.Count > 0 ? Revisions.OrderBy(x => x.AppVersion).Last().ReleaseDate : DateTime.MinValue;
            }
        }

    }
}
