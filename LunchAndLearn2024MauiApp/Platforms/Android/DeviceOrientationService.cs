using Android.Content;
using Android.Runtime;
using Android.Views;

namespace LunchAndLearn2024MauiApp.Services;

public class DeviceOrientationService : IDeviceOrientationService
{
    public DeviceOrientation GetOrientation()
    {
        var windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
        var orientation = windowManager?.DefaultDisplay?.Rotation;
        bool isLandscape = orientation == SurfaceOrientation.Rotation90 || orientation == SurfaceOrientation.Rotation270;
        return isLandscape ? DeviceOrientation.Landscape : DeviceOrientation.Portrait;
    }
}
