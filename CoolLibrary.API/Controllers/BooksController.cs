using AutoMapper;
using CoolLibrary.Application.DTO;
using CoolLibrary.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CoolLibrary.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly IBooks _booksRepository;
    private readonly ILogger<BooksController> _logger;
    private readonly IMapper _mapper;

    public BooksController(IBooks booksRepository, ILogger<BooksController> logger, IMapper mapper)
    {
        _booksRepository = booksRepository;
        _logger = logger;
        _mapper = mapper;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
    {
        try
        {
            var books = await _booksRepository.GetAllAsync();
            var bookDTOs = _mapper.Map<IEnumerable<BookDTO>>(books);
            return Ok(bookDTOs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all books");
            return StatusCode(500, "An error occurred while retrieving books");
        }
    }
}
