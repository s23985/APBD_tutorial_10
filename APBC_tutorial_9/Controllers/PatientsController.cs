using APBC_tutorial_9.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBC_tutorial_9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _patientService.GetPatientFullInfoAsync(id);
        if (patient == null || patient.IdPatient == 0)
        {
            return NotFound();
        }
        return Ok(patient);
    }
}