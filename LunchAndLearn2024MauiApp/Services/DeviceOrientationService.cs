namespace LunchAndLearn2024MauiApp.Services;
public enum DeviceOrientation
{
    Undefined,
    Landscape,
    Portrait
}

public interface IDeviceOrientationService
{
    DeviceOrientation GetOrientation();
}
