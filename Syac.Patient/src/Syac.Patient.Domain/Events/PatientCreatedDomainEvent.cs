using Syac.Patient.Domain.Abstractions;
using Syac.Patient.Domain.Aggregates;
using Syac.Patient.Domain.Enums;
using System.Text.Json;

namespace Syac.Patient.Domain.Events;

public class PatientCreatedDomainEvent
{
    public EventData EventData { get; init; }

    public PatientCreatedDomainEvent(
        PatientAggregate patient)
    {

        EventData = new EventData
        {
            Id = Guid.NewGuid(),
            Action = ActionsEnum.PatientCreated,
            CreatedAt = DateTime.UtcNow,
            Value = JsonSerializer.Serialize(patient)
        };
    }
}
