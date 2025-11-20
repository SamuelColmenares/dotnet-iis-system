using System.Diagnostics.Metrics;

namespace Syac.Patient.Infraestructure.Metrics;

public class MetricsService(Meter meter)
{
    private readonly Counter<long> _requestCounter = meter.CreateCounter<long>("syac.patient.requests.count",
        description: "Total number of requests received");

    public void AddHit(string route)
    {
        _requestCounter.Add(1, new KeyValuePair<string, object>("route", route));
    }
}
