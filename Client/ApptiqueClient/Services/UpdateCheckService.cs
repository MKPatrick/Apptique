using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using ApptiqueClient.Platforms.Android;
using ApptiqueClient.ViewModel;
using Microsoft.Extensions.Hosting;
using Timer = System.Timers.Timer;

namespace ApptiqueClient.Services;

[Service]
public class UpdateCheckService : Service, IServiceTest, IHostedService
{
    private readonly List<AppsOverviewViewModel> apps = new();
    private readonly AppService appService = new(new HttpClient());
    private readonly PackageService packageService = new();
    private Timer timer;
    private bool UpdatePending;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var startService = new Intent(MainActivity.ActivityCurrent, typeof(UpdateCheckService));
        startService.SetAction("START_SERVICE");
        MainActivity.ActivityCurrent.StartService(startService);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        var stopIntent = new Intent(MainActivity.ActivityCurrent, Class);
        stopIntent.SetAction("STOP_SERVICE");
        MainActivity.ActivityCurrent.StartService(stopIntent);

        return Task.CompletedTask;
    }


    public void Start()
    {
        var startService = new Intent(MainActivity.ActivityCurrent, typeof(UpdateCheckService));
        startService.SetAction("START_SERVICE");
        MainActivity.ActivityCurrent.StartService(startService);
    }

    public void Stop()
    {
        var stopIntent = new Intent(MainActivity.ActivityCurrent, Class);
        stopIntent.SetAction("STOP_SERVICE");
        MainActivity.ActivityCurrent.StartService(stopIntent);
    }

    public override void OnCreate()
    {


        timer = new Timer(TimeSpan.FromHours(2));
        timer.Elapsed += Timer_Elapsed;

        base.OnCreate();
    }


    public override IBinder OnBind(Intent intent)
    {
        throw new NotImplementedException();
    }

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags,
        int startId)
    {
        if (intent.Action == "START_SERVICE")
        {
            timer.Start();
        }
        else if (intent.Action == "STOP_SERVICE")
        {
            StopForeground(true);
            StopSelfResult(startId);
        }

        return StartCommandResult.NotSticky;
    }


    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        IsUpdateAvailable();


        if (UpdatePending) RegisterNotification();
    }

    private async Task IsUpdateAvailable()
    {
        apps.Clear();
        var appsFromServer = await appService.GetAppInformationsFromServer();
        appsFromServer.ForEach(x => apps.Add(new AppsOverviewViewModel { App = x }));
        var packages = packageService.GetAllPackages();
        foreach (var item in packages)
        {
            var appMatch = apps.FirstOrDefault(x => x.App.PackageName == item.PackageName);
            if (appMatch != null)
            {
                appMatch.InstalledVersion = item.VersionCode;
                UpdatePending = false;


                if (appMatch.App.CurrentReleaseVersion > item.VersionCode)
                    UpdatePending = true;
                else
                    UpdatePending = false;
            }
        }
    }

    private void RegisterNotification()
    {
        var channel = new NotificationChannel("ServiceChannel", "Update Checker", NotificationImportance.Max);
        var manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(NotificationService);
        manager.CreateNotificationChannel(channel);

        var smallIconId = Resource.Drawable.download;

        var clickIntent = new Intent(MainActivity.ActivityCurrent, typeof(MainActivity));
        var pendingIntent =
            PendingIntent.GetActivity(MainActivity.ActivityCurrent, 0, clickIntent, PendingIntentFlags.OneShot);

        var notification = new Notification.Builder(this, "ServiceChannel")
            .SetContentTitle("Update Available")
            .SetContentText("An update is available for one or more apps")
            .SetSmallIcon(smallIconId)
            .SetOngoing(true)
            .SetContentIntent(pendingIntent)
            .Build();

        StartForeground(100, notification);
    }
}