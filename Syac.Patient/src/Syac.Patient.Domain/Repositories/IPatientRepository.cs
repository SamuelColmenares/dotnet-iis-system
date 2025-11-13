using Syac.Patient.Domain.Aggregates;

namespace Syac.Patient.Domain.Repositories;

public interface IPatientRepository
{
    Task<PatientAggregate> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(PatientAggregate patient);
    Task UpdateAsync(PatientAggregate patient);
    Task DeleteAsync(Guid id);
}

