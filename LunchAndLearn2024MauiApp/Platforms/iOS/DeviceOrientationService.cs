using UIKit;

namespace LunchAndLearn2024MauiApp.Services;
public class DeviceOrientationService : IDeviceOrientationService
{
    public DeviceOrientation GetOrientation()
    {
        var orientation = UIApplication.SharedApplication?.StatusBarOrientation;
        bool isPortrait = orientation == UIInterfaceOrientation.Portrait || orientation == UIInterfaceOrientation.PortraitUpsideDown;
        return isPortrait ? DeviceOrientation.Portrait : DeviceOrientation.Landscape;
    }
}
