using CommunityToolkit.Mvvm.ComponentModel;

namespace LunchAndLearn2024MauiApp.ViewModels;
public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    [ObservableProperty]
    string? title;
    [ObservableProperty]
    bool isNotBusy;

    public bool IsInitialized { get; set; }
}
