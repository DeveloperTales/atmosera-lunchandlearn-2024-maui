using LunchAndLearn2024Models;

namespace LunchAndLearn2024MauiApp.Services;
public interface ITelemetry
{
    void TrackException(Exception ex, IDictionary<string, string> properties = null);

    void TrackEvent(string name, IDictionary<string, string> properties = null);
}
