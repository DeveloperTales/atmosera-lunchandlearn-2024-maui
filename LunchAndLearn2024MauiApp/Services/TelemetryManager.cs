using System.Diagnostics;

namespace LunchAndLearn2024MauiApp.Services;
public class TelemetryManager() : ITelemetry
{
    public void TrackEvent(string name, IDictionary<string, string> properties = null)
    {
        SendEvent(name, properties);
        Trace.WriteLine($"{name}: {string.Concat(properties)}");
    }

    public void TrackException(Exception ex, IDictionary<string, string> properties = null)
    {
        properties ??= new Dictionary<string, string>
                {
                    { "CRASHMESSAGE", ex.Message }
                };

        SendEvent("CRASH", properties);
        Trace.TraceError("CRASH", ex);
    }

    private void SendEvent(string eventId, string paramName, string value)
    {
    }

    private void SendEvent(string eventId, IDictionary<string, string> parameters)
    {
    }
}