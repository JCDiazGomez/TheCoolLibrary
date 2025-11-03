using AutoMapper;
using CoolLibrary.Application.DTO;
using CoolLibrary.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoolLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LoansController : ControllerBase
{
    private readonly LoanRequestService _service;
    private readonly ILogger<LoansController> _logger;

    public LoansController(LoanRequestService service, ILogger<LoansController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoanResponseDTO>> RequestLoan([FromBody] LoanRequestDTO request)
    {
        var (ok, error, loan) = await _service.RequestLoanAsync(request);
        if (!ok)
        {
            return BadRequest(error);
        }
        return Ok(loan);
    }

    [HttpGet("availability/{bookId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AvailabilityDTO>> GetAvailability(int bookId)
    {
        var result = await _service.GetAvailabilityAsync(bookId);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
