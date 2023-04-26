using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using ApptiqueClient;

namespace ApptiqueClient.Services
{


    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class BootCompletedReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(Intent.ActionBootCompleted))
            {
                var serviceIntent = new Intent(context, typeof(UpdateCheckService));
                context.StartForegroundService(serviceIntent);
            }
        }
    }


}





