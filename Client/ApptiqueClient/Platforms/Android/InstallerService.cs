using Android.Content;
using Android.OS;
using Android.Net;
using ApptiqueClient.Models;
using System.Net;
using Android.Provider;

using Permission= Android.Content.PM.Permission;
using FileProvider = AndroidX.Core.Content.FileProvider;
using Uri = Android.Net.Uri;
using Environment = Android.OS.Environment;
using Android;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace ApptiqueClient.Platforms.Android
{
    public class InstallerService : IInstallerService
    {

        public async Task<bool> InstallAndDownloadPackage(RevisionModel revision, string packagename)
        {
            try
            {
                string apk = Path.Combine(Path.Combine(FileSystem.Current.AppDataDirectory, "installation.apk"));
                revision.APKPath = "http://ro-srv-it-train:9049/"+ revision.APKPath;
            var currentActivitiy = Platform.CurrentActivity;
            string downloadPath = $"{revision.APKPath}";
                var b = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        
            
        
                WebClient client = new WebClient();
            //delete file in temp directory
            System.IO.File.Delete(apk);
             
      
            await Task.Run(() => client.DownloadFile(downloadPath, apk));
            //if (false && Build.VERSION.SdkInt >= BuildVersionCodes.R && !Platform.CurrentActivity.PackageManager.CanRequestPackageInstalls())
            //{
            //    // Ask the user to grant the app the REQUEST_INSTALL_PACKAGES permission
            //    Intent intent = new Intent(Settings.ActionManageAllFilesAccessPermission);
            //    intent.SetData(Uri.Parse($"package:{packagename}"));
            //    currentActivitiy.StartActivity(intent);
            //    return false;
            //}
              
                Intent installIntent = new Intent(Intent.ActionView);
            installIntent.SetDataAndType(Uri.Parse($"file://{apk}"), "application/vnd.android.package-archive");
            installIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

            if ( Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                // On Android 7.0 and later, we need to explicitly grant read permission to the APK file
                var apkUri = FileProvider.GetUriForFile(currentActivitiy.ApplicationContext, $"com.companyname.apptiqueclient.fileprovider", new Java.IO.File(apk));
                installIntent.AddFlags(ActivityFlags.GrantReadUriPermission);
                installIntent.SetDataAndType(apkUri, "application/vnd.android.package-archive");
            }
            currentActivitiy.StartActivity(installIntent);

            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }
}
