using Syac.Patient.Application.Enums;

namespace Syac.Patient.Application.DataModels;

public sealed class PatientDataModel
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public required string DocumentType { get; set; }
    public string DocumentNumber { get; set; } = Guid.NewGuid().ToString();
    public required GendersDataModelEnum Gender { get; set; }
}
