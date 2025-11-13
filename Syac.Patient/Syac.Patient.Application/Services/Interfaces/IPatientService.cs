using Syac.Patient.Application.DataModels;

namespace Syac.Patient.Application.Services.Interfaces;

public interface IPatientService
{
    Task<Guid> CreatePatientAsync(PatientDataModel patientDto);
}
