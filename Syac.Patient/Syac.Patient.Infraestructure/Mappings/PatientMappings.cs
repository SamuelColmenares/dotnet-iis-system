using Syac.Patient.Domain.Aggregates;
using Syac.Patient.Domain.Enums;
using Syac.Patient.Domain.ValueObjects;
using Syac.Patient.Infraestructure.Dtos;
using Syac.Patient.Infraestructure.Enums;

namespace Syac.Patient.Infraestructure.Mappings;

public static class PatientMappings
{
    public static PatientDto ToDto(this PatientAggregate patient)
    {
        var result = Enum.TryParse(
            typeof(GendersDtoEnum),
            patient.Gender.Value.ToString(),
            out var genderObj);

        if (!result)
            throw new ArgumentException("Invalid Aggregate Gender.");

        return new()
        {
            Id = patient.Id,
            FirstName = patient.Name!.FirstName,
            LastName = patient.Name!.LastName,
            DateOfBirth = patient.DateOfBirth!.Value,
            DocumentType = patient.Document.DocumentType.Type,
            DocumentNumber = patient.Document.DocumentNumber,
            Gender = (GendersDtoEnum)genderObj!
        };
    }

    public static PatientAggregate ToDomain(this PatientDto patientDto)
    {
        var result = Enum.TryParse(
            typeof(Genders),
            patientDto.ToString(),
            out var genderObj);

        if (!result)
            throw new ArgumentException("Invalid Dto Gender.");

        return PatientAggregate.Create(
        new Name(patientDto.FirstName, patientDto.LastName),
        new Document(
            DocumentType.FromString(patientDto.DocumentType),
            patientDto.DocumentNumber),
        new DateOfBirth(patientDto.DateOfBirth),
        new Gender((Genders)genderObj!));
    }
}
