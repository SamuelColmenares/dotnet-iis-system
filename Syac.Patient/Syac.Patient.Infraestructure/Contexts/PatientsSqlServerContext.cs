using Microsoft.EntityFrameworkCore;
using Syac.Patient.Infraestructure.Dtos;

namespace Syac.Patient.Infraestructure.Contexts;

public class PatientsSqlServerContext: DbContext
{
    public PatientsSqlServerContext(DbContextOptions<PatientsSqlServerContext> options) : base(options)
    {
    }

    public DbSet<PatientDto> Patients { get; set; }

}
