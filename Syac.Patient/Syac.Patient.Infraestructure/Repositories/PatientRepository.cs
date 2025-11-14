using Syac.Patient.Domain.Aggregates;
using Syac.Patient.Domain.Repositories;
using Syac.Patient.Infraestructure.Contexts;
using Syac.Patient.Infraestructure.Mappings;

namespace Syac.Patient.Infraestructure.Repositories;

public class PatientRepository(PatientsSqlServerContext dbContext) : IPatientRepository
{
    public async Task<Guid> AddAsync(PatientAggregate patient)
    {
        var patientDto = patient.ToDto();
        var result = await dbContext.Patients.AddAsync(patientDto);

        if(result is null || result.Entity.Id == Guid.Empty)
        {
            throw new ApplicationException("Error adding patient to the database.");
        }

        await dbContext.SaveChangesAsync().ConfigureAwait(false);

        return result.Entity.Id;
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PatientAggregate> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(PatientAggregate patient)
    {
        throw new NotImplementedException();
    }
}
