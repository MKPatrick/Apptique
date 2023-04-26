using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using ApptiqueClient.Services;
using Java.Lang;

namespace ApptiqueClient;

[Activity(Label = "Apptique", Theme = "@style/Maui.SplashTheme", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    public MainActivity()
    {
        ActivityCurrent = this;
    }

    public static MainActivity ActivityCurrent { get; set; }


    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        var updateServiceIntent = new Intent(this, typeof(UpdateCheckService));
        StartService(updateServiceIntent);
        RegisterBootCompletedReceiver();
    }

    private void RegisterBootCompletedReceiver()
    {
        var filter = new IntentFilter(Intent.ActionBootCompleted);
        filter.AddCategory(Intent.CategoryDefault);
        var receiver = new ComponentName(this, Class.FromType(typeof(BootCompletedReceiver)));
        PackageManager.SetComponentEnabledSetting(receiver, ComponentEnabledState.Enabled,
            ComponentEnableOption.DontKillApp);
        RegisterReceiver(new BootCompletedReceiver(), filter);
    }
}