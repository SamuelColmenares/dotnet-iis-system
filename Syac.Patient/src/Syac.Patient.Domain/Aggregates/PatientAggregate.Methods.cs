using Syac.Patient.Domain.ValueObjects;

namespace Syac.Patient.Domain.Aggregates;

public partial class PatientAggregate
{
    private PatientAggregate(
        Guid id,
        Name name,
        Document document,
        DateOfBirth dateOfBirth,
        Gender gender
        )
    {
        Id = id;
        Name = name;
        Document = document;
        DateOfBirth = dateOfBirth;
        Gender = gender;
    }

    public static PatientAggregate Create(Name name,
        Document document,
        DateOfBirth dateOfBirth,
        Gender gender)
    {
        var aggregate = new PatientAggregate(
           Guid.NewGuid(),
           name,
           document,
           dateOfBirth,
           gender);

        aggregate.AddEvent(
            new Events.PatientCreatedDomainEvent(aggregate).EventData);

        return aggregate;
    }
}
