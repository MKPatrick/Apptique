namespace ApptiqueClient.Platforms.Android
{

    public class StateChangedService : IStateChangedService
    {
        public event EventHandler UpdateUI;
        private DateTime LastTriggeredEvent = DateTime.MinValue;

        public void Register()
        {
      
            Platform.ActivityStateChanged += StateHasChanged;
        }

        private void StateHasChanged(object sender, ActivityStateChangedEventArgs e)
        {
            if (e.State == ActivityState.Resumed)
            {
                if(DateTime.Now> LastTriggeredEvent.AddSeconds(1))
                {
                    LastTriggeredEvent=DateTime.Now;
                    UpdateUI?.Invoke(this, EventArgs.Empty);
                }
      
            }
        }

        public void UnRegister()
        {
            Platform.ActivityStateChanged -= StateHasChanged;
        }

    }
}
