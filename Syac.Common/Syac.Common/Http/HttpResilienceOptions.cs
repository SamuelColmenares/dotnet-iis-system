namespace Syac.Common.Http;

public class HttpResilienceOptions
{
    public int MaxRetryAttempts { get; set; } = 3;
    public TimeSpan DelayBetweenRetries { get; set; } = TimeSpan.FromSeconds(2);
    public TimeSpan TotalTimeout { get; set; } = TimeSpan.FromSeconds(30);

    public static HttpResilienceOptions Default => new();
}
