using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Syac.Patient.Application.Services;
using Syac.Patient.Application.Services.Interfaces;
using Syac.Patient.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


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

#region OTel

var serviceName = "Syac.Patient.Api";

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService(serviceName))

    // ---------- TRACING ----------
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:4317");
        })
    )

    // ---------- METRICS ----------
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://otel-collector:4317");
        })
    );

// ---------- LOGGING ----------
builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(o =>
{
    o.IncludeScopes = true;
    o.ParseStateValues = true;
    o.AddOtlpExporter(exporter =>
    {
        exporter.Endpoint = new Uri("http://otel-collector:4317");
    });
});

#endregion


#region DI Registrations

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services
    .AddPatitentInfraestructure(builder.Configuration)
    .AddPatientRepository();

#endregion


var app = builder.Build();

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
