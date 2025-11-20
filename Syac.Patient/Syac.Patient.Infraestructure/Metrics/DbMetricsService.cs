using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Syac.Patient.Infraestructure.Metrics;

public class DbMetricsService(Meter meter)
{
    private readonly Histogram<double> _dbQueryDuration = meter.CreateHistogram<double>("syac.patient.db.query.duration", "ms",
        description: "Duration of database queries in milliseconds");

    public async Task<T> Measure<T>(Func<Task<T>> dbOperation, string queryType)
    {
        var sw = Stopwatch.StartNew();
        T result = await dbOperation();
        sw.Stop();
        _dbQueryDuration.Record(
            sw.Elapsed.TotalMilliseconds,
            new KeyValuePair<string, object>("query", queryType));
        return result;
    }

    public async Task MeasureCommand(Func<Task> command, string queryType)
    {
        var sw = Stopwatch.StartNew();
        await command();
        sw.Stop();
        _dbQueryDuration.Record(
            sw.Elapsed.TotalMilliseconds,
            new KeyValuePair<string, object>("query", queryType));
    }

}
