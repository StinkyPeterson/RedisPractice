using Application.Contracts.Contracts;
using Microsoft.AspNetCore.Mvc;
using RedisPractice.Domain.Entity;

namespace Presentation.Api.V1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost("create-person")]
    public async Task<ActionResult> CreatePerson([FromBody] Person person)
    {
        await _personService.CreatePerson(person);
        return Ok();
    }
    
    [HttpGet("get-persons")]
    public async Task<ActionResult<IReadOnlyCollection<Person>>> GetPersons([FromQuery] int skip, int take)
    {
        return Ok(await _personService.GetPersons(skip, take));
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<Person>> GetPersonById([FromQuery] Guid id)
    {
        return Ok(await _personService.GetPersonById(id));
    }
}