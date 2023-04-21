namespace ApptiqueClient
{
    public interface IStateChangedService
    {
        event EventHandler UpdateUI;
        void Register();
        void UnRegister();
    }
}
