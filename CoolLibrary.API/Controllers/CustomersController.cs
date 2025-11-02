using AutoMapper;
using CoolLibrary.Domain.Contracts;
using CoolLibrary.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoolLibrary.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CustomersController : ControllerBase
{
    private readonly ICustomers _customersRepository;
    private readonly ILogger<CustomersController> _logger;
    private readonly IMapper _mapper;

    public CustomersController(ICustomers customersRepository, ILogger<CustomersController> logger, IMapper mapper)
    {
        _customersRepository = customersRepository;
        _logger = logger;
        _mapper = mapper;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
    {
        try
        {
            var customers = await _customersRepository.GetAllAsync();
            var customerDTOs = _mapper.Map<IEnumerable<Customer>>(customers);
            return Ok(customerDTOs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all customers");
            return StatusCode(500, "An error occurred while retrieving customers");
        }
    }
}
