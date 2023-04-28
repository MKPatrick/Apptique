using ApptiqueClient.Models;

namespace ApptiqueClient.ViewModel;

public class AppsOverviewViewModel
{
    public AppModel App { get; set; }
    public int? InstalledVersion { get; set; }

    public bool IsUpdateAvailable => InstalledVersion != null && InstalledVersion < App.CurrentReleaseVersion;

    public bool CanBeInstalled => InstalledVersion == null;
}