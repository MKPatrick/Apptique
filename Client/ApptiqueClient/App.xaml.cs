using ApptiqueClient.Services;

namespace ApptiqueClient;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage(new UpdateCheckService());
    }
}