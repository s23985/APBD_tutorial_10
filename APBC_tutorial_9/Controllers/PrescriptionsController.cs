using APBC_tutorial_9.Models.Dtos.Prescription;
using APBC_tutorial_9.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBC_tutorial_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDto request)
    {
        var result = await _prescriptionService.AddPrescriptionAsync(request);
        if (result == false)
            return BadRequest("Invalid prescription data.");
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> ThrowUnhandledError([FromHeader] string userAgent)
    {
        throw new Exception("Unhandled.");
    }
}