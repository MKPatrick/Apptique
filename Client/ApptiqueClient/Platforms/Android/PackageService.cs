using Android.Content.PM;
using ApptiqueClient.Models;

namespace ApptiqueClient.Platforms.Android
{
    internal class PackageService : IPackageService
    {

        public List<PackageModel> GetAllPackages()
        {
            List<PackageModel> packageModels = new List<PackageModel>();
            PackageManager packageManager = Platform.CurrentActivity.PackageManager;
            var packages = packageManager.GetInstalledPackages(PackageInfoFlags.MatchUninstalledPackages);

            foreach (PackageInfo package in packages)
            {
                PackageModel packageM = new();

                packageM.PackageName = package.PackageName;
                packageM.VersionCode = package.VersionCode;
                packageModels.Add(packageM);
            }
            return packageModels;
        }
    }
}
