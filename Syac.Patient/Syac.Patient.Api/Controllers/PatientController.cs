using Microsoft.AspNetCore.Mvc;
using Syac.Patient.Api.Requests;
using Syac.Patient.Application.DataModels;
using Syac.Patient.Application.Enums;
using Syac.Patient.Application.Services.Interfaces;

namespace Syac.Patient.Api.Controllers;

[Route("patients")]
public class PatientController(IPatientService patientService) : ControllerBase
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

        //return CreatedAtAction(nameof(CreatePatient), new { id = result }, result);
        return Ok(result);
    }
}
