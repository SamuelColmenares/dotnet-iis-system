using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syac.Patient.Domain.Repositories;
using Syac.Patient.Infraestructure.Contexts;
using Syac.Patient.Infraestructure.Repositories;

namespace Syac.Patient.Infraestructure.Extensions;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddPatitentInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PatientsSqlServerContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PatientsDatabase")));

        return services;
    }

    public static IServiceCollection AddPatientRepository(this IServiceCollection services)
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
        return services;
    }   
}
