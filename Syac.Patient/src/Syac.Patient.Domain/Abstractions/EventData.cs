namespace Syac.Patient.Domain.Abstractions;

public class EventData
{
    public Guid Id { get; init; }
    public Enum Action { get; init; }
    public DateTime CreatedAt { get; init; }
    public string Value { get; init; }
}
