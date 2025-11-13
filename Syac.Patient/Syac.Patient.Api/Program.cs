using Syac.Patient.Application.Services;
using Syac.Patient.Application.Services.Interfaces;
using Syac.Patient.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

#region DI Registrations

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services
    .AddPatitentInfraestructure(builder.Configuration)
    .AddPatientRepository();

#endregion

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient API V1");
    opt.RoutePrefix = string.Empty;
    opt.DocumentTitle = "Patient API Documentation";
});
app.MapControllers();
app.UseCors("AllowAll");
app.Run();
