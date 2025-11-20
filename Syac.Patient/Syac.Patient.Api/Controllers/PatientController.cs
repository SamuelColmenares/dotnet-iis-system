using Microsoft.AspNetCore.Mvc;
using Syac.Patient.Api.Requests;
using Syac.Patient.Api.Routes;
using Syac.Patient.Application.DataModels;
using Syac.Patient.Application.Enums;
using Syac.Patient.Application.Services.Interfaces;
using Syac.Patient.Infraestructure.Metrics;

namespace Syac.Patient.Api.Controllers;

[Route(PatientRoutes.BaseRoute)]
public class PatientController(IPatientService patientService, MetricsService metrics) : ControllerBase
{
    [HttpPost()]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePatient([FromBody] PatientRequest patient)
    {
        var patientDataMOdel = new PatientDataModel
        {
            FirstName = patient.FirstName!,
            LastName = patient.LastName!,
            DateOfBirth = patient.DateOfBirth,
            DocumentType = patient.DocumentType!,
            DocumentNumber = patient.DocumentNumber,
            Gender = (GendersDataModelEnum)patient.Gender
        };
        
        var result = await patientService.CreatePatientAsync(patientDataMOdel);
        metrics.AddHit($"{Request.Method} /{PatientRoutes.BaseRoute}");

        return CreatedAtAction(nameof(CreatePatient), new { id = result }, result);
        
    }
}
