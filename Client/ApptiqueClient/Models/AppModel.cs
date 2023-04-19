using Android.Icu.Number;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApptiqueClient.Models
{
    public class AppModel
    {
        public string ID { get; set; }
        public string AppName { get; set; }
        public string PackageName { get; set; }
        public string AppIcon { get; set; }
        public string Description { get; set; }
        public List<RevisionModel> Revisions { get; set; }
        public int CurrentReleaseVersion { get; set; }
        public DateTime LastReleaseDate { get; set; }
    }
}
