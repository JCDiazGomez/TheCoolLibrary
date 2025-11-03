using AutoMapper;
using CoolLibrary.Application.DTO;
using CoolLibrary.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CoolLibrary.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthors _authorsRepository;
    private readonly ILogger<AuthorsController> _logger;
    private readonly IMapper _mapper;

    public AuthorsController(IAuthors authorsRepository, ILogger<AuthorsController> logger, IMapper mapper)
    {
        _authorsRepository = authorsRepository;
        _logger = logger;
        _mapper = mapper;
    }

 
    [HttpGet("with-books")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthorsWithBooks()
    {
        try
        {
            var authors = await _authorsRepository.GetAllAsync();
            var authorDTOs = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
            return Ok(authorDTOs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all authors with books");
            return StatusCode(500, "An error occurred while retrieving authors and their books");
        }
    }
}