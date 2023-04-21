using ApptiqueClient.Models;

namespace ApptiqueClient.ViewModel
{
    public class AppsOverviewViewModel
    {
        public AppModel App { get; set; }
        public int? InstalledVersion { get; set; }
        public bool IsUpdateAvailable
        {
            get
            {
                return !(InstalledVersion == null) && InstalledVersion < App.CurrentReleaseVersion;
            }

        }

        public bool CanBeInstalled
        {
            get
            {
                return InstalledVersion == null;
            }
        }
    }
}
