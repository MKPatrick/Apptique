namespace ApptiqueClient;

public partial class MainPage : ContentPage
{
    private IServiceTest Services;

    public MainPage(IServiceTest Services_)
    {
        InitializeComponent();
        Services = Services_;
    }


    private void MainPage_OnLoaded(object sender, EventArgs e)
    {
        Services.Start();
    }
}
