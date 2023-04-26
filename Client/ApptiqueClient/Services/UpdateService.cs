using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using ApptiqueClient.Platforms.Android;
using ApptiqueClient.ViewModel;
using Timer = System.Timers.Timer;

namespace ApptiqueClient.Services
{
    public class UpdateService : Service, IServiceTest
    {
        private Timer updateTimer;
        List<AppsOverviewViewModel> apps = new();
        AppService appService = new(new HttpClient());
        PackageService packageService = new();


        public override void OnCreate()
        {
            base.OnCreate();

            updateTimer = new Timer(500);
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.Enabled = true;


        }

        public override IBinder OnBind(Intent intent)
        {
            //We dont use this
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            // Stop the timer when the service is destroyed
            updateTimer.Enabled = false;
            updateTimer.Elapsed -= UpdateTimer_Elapsed;
            updateTimer.Dispose();
            updateTimer = null;
        }

        private async void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Call the GetPackages method to check for updates
            await GetPackages();
        }

        private async Task GetPackages()
        {
            apps.Clear();

            var appsFromServer = await appService.GetAppInformationsFromServer();
            appsFromServer.ForEach(x => apps.Add(new AppsOverviewViewModel() { App = x }));
            var packages = packageService.GetAllPackages();

            foreach (var item in packages)
            {
                var appMatch = apps.FirstOrDefault(x => x.App.PackageName == item.PackageName);

                if (appMatch != null)
                {
                    appMatch.InstalledVersion = item.VersionCode;



                    if (appMatch.App.CurrentReleaseVersion > item.VersionCode)
                    {
                        var notificationManager = GetSystemService(NotificationService) as NotificationManager;
                        var notificationBuilder = new Notification.Builder(this)
                            .SetContentTitle("Update Available")
                            .SetContentText($"An update for {appMatch.App.AppName} is available")
                            .SetSmallIcon(Resource.Drawable.ic_m3_chip_check)
                            .SetAutoCancel(true);

                        notificationManager.Notify(0, notificationBuilder.Build());

                    }

                }
            }


        }

        [return: GeneratedEnum]//we catch the actions intents to know the state of the foreground service
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.Action == "START_SERVICE")
            {
                RegisterNotification();//Proceed to notify
            }
            else if (intent.Action == "STOP_SERVICE")
            {
                StopForeground(true);//Stop the service
                StopSelfResult(startId);
            }

            return StartCommandResult.NotSticky;
        }

        //Start and Stop Intents, set the actions for the MainActivity to get the state of the foreground service
        //Setting one action to start and one action to stop the foreground service
        public void Start()
        {
            Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(UpdateService));
            startService.SetAction("START_SERVICE");
            MainActivity.ActivityCurrent.StartService(startService);
        }

        public void Stop()
        {
            Intent stopIntent = new Intent(MainActivity.ActivityCurrent, this.Class);
            stopIntent.SetAction("STOP_SERVICE");
            MainActivity.ActivityCurrent.StartService(stopIntent);
        }

        private async Task RegisterNotification()
        {

            NotificationChannel channel = new NotificationChannel("ServiceChannel", "ServiceDemo", NotificationImportance.Max);
            NotificationManager manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);

            int smallIconId = Resource.Drawable.maui_splash_image;

            Notification notification = new Notification.Builder(this, "ServiceChannel")
                .SetContentTitle("Update Available")
                .SetContentText($"An update is available for one or more apps")
                .SetSmallIcon(smallIconId)
                .SetOngoing(true)
                .Build();

            StartForeground(100, notification);

            //apps.Clear();

            //var appsFromServer = await appService.GetAppInformationsFromServer();
            //appsFromServer.ForEach(x => apps.Add(new AppsOverviewViewModel() { App = x }));
            //var packages = packageService.GetAllPackages();

            //foreach (var item in packages)
            //{
            //    var appMatch = apps.FirstOrDefault(x => x.App.PackageName == item.PackageName);

            //    if (appMatch != null)
            //    {
            //        appMatch.InstalledVersion = item.VersionCode;



            //        if (appMatch.App.CurrentReleaseVersion > item.VersionCode)
            //        {


            //        }

            //    }
            //}



        }





    }
}
