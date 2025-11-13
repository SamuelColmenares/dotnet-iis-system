using Syac.Patient.Application.DataModels;
using Syac.Patient.Application.Mappings;
using Syac.Patient.Application.Services.Interfaces;
using Syac.Patient.Domain.Repositories;

namespace Syac.Patient.Application.Services;

public sealed class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<Guid> CreatePatientAsync(PatientDataModel patientDataModel)
    {
        var patientAggregate = patientDataModel.ToDomain();

        var createdPatient = await patientRepository.AddAsync(patientAggregate);
        return createdPatient;
    }
}
