namespace Handymen_UI_Consumer.Models;

public class AppState
{
    public event Action OnChange;
    public HandymanUILibrary.Models.OrderModel SelectedOrder { get; private set; }
    internal void SetOrder(HandymanUILibrary.Models.OrderModel order)
    {
        SelectedOrder = order;
        NotifyStateChanged();

    }
    private void NotifyStateChanged() => OnChange?.Invoke();
}
