using System.Text.Json.Serialization;

namespace Syac.Patient.Domain.Abstractions;

public abstract class AggregateRoot<TId> where TId : IEquatable<TId>, IComparable<TId>
{
    protected readonly ICollection<EventData> _AggregateEvents = [];

    public TId Id { get; init; }

    // public DateTime _Created { get; init; } = DateTime.Now;

    [JsonIgnore]
    public IReadOnlyCollection<EventData> Events => _AggregateEvents.ToList().AsReadOnly();

    protected AggregateRoot()
    {
        Id = GetDefaultId();
    }

    protected AggregateRoot(TId id)
    {
        Id = id;
    }
    
    protected void AddEvent(EventData @event)
    {
        _AggregateEvents.Add(@event);
    }

    protected void ClearEvents()
    {
        _AggregateEvents.Clear();
    }

    private static TId GetDefaultId()
    {
        if (typeof(TId) == typeof(Guid))
        {
            return (TId)(object)Guid.NewGuid();
        }

        return default!;
    }
}
