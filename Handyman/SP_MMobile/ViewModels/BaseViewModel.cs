using CommunityToolkit.Mvvm.ComponentModel;

namespace SP_MMobile.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        public BaseViewModel()
        {

        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !isBusy;
    }
}
