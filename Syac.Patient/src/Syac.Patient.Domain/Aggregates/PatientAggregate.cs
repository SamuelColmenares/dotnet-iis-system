using Syac.Patient.Domain.Abstractions;
using Syac.Patient.Domain.Enums;
using Syac.Patient.Domain.ValueObjects;

namespace Syac.Patient.Domain.Aggregates;

public partial class PatientAggregate: AggregateRoot<Guid>
{
    public Name? Name { get; init; }

    public Document Document { get; init; } 

    public DateOfBirth? DateOfBirth { get; init; }

    public Gender Gender { get; init; } = new Gender(Genders.NotDefined);
}
