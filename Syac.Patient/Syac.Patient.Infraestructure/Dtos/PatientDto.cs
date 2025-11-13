using Syac.Patient.Infraestructure.Enums;

namespace Syac.Patient.Infraestructure.Dtos;

public class PatientDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public required string DocumentType { get; set; }
    public string DocumentNumber { get; set; } = Guid.NewGuid().ToString();
    public required GendersDtoEnum Gender { get; set; }
}
