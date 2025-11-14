using Syac.Patient.Application.DataModels;
using Syac.Patient.Application.Enums;
using Syac.Patient.Domain.Aggregates;
using Syac.Patient.Domain.Enums;
using Syac.Patient.Domain.ValueObjects;

namespace Syac.Patient.Application.Mappings;

public static class PatientMappings
{
    public static PatientDataModel ToDataModel(this PatientAggregate patient)
    {
        var result = Enum.TryParse(
            typeof(GendersDataModelEnum),
            patient.Gender.Value.ToString(),
            out var genderObj);

        if (!result)
            throw new ArgumentException("Invalid Aggregate Gender in Application layer.");

        return new()
        {
            Id = patient.Id,
            FirstName = patient.Name!.FirstName,
            LastName = patient.Name!.LastName,
            DateOfBirth = patient.DateOfBirth!.Value,
            DocumentType = patient.Document.DocumentType.Type,
            DocumentNumber = patient.Document.DocumentNumber,
            Gender = (GendersDataModelEnum)genderObj!
        };
    }

    public static PatientAggregate ToDomain(this PatientDataModel patientDataModel)
    {
        var result = Enum.TryParse(
            typeof(Genders),
            patientDataModel.Gender.ToString(),
            out var genderObj);

        if (!result)
            throw new ArgumentException("Invalid Datamodel Gender.");

        return PatientAggregate.Create(
        new Name(patientDataModel.FirstName, patientDataModel.LastName),
        new Document(
            DocumentType.FromString(patientDataModel.DocumentType),
            patientDataModel.DocumentNumber),
        new DateOfBirth(patientDataModel.DateOfBirth),
        new Gender((Genders)genderObj!));
    }
}
