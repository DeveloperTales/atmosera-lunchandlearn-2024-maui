
using CommunityToolkit.Mvvm.ComponentModel;
using LunchAndLearn2024MauiApp.Services;

namespace LunchAndLearn2024MauiApp.ViewModels;
public partial class AboutViewModel : BaseViewModel
{
    IDeviceOrientationService _deviceOrientationService;

    [ObservableProperty]
    string deviceOrientation;

    public AboutViewModel(IDeviceOrientationService deviceOrientationService)
    {
        _deviceOrientationService = deviceOrientationService;
        deviceOrientation = $"Current orientation: {_deviceOrientationService.GetOrientation()}";
    }

    public void UpdateDeviceOrientationString()
    {
        DeviceOrientation = $"Current orientation: {_deviceOrientationService.GetOrientation()}";
    }

}
