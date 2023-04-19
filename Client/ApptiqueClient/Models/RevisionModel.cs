using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApptiqueClient.Models
{
    public class RevisionModel
    {
        public string ID { get; set; }
        public int AppVersion { get; set; }
        public string APKPath { get; set; }
        public string AppVersionName { get; set; }
        public string ChangeSet { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
