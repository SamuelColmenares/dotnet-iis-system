using System.ComponentModel.DataAnnotations;

namespace Syac.Patient.Api.Requests;

public sealed class PatientRequest
{
    [Required]
    public string? FirstName { get; set; }
    
    [Required]
    public string? LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string? DocumentType { get; set; }

    public string? DocumentNumber { get; set; }

    [Required]
    [Range(0, 4)]
    public int Gender { get; set; }
}
