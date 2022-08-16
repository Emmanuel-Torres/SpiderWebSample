using Microsoft.AspNetCore.Mvc;
using SpiderWeb.LicensingApi.Interfaces;
using SpiderWeb.LicensingApi.Models;

namespace SpiderWeb.LicensingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IPeopleStorage _peopleStorage;

    public PeopleController(ILogger<PeopleController> logger, IPeopleStorage peopleStorage)
    {
        _logger = logger;
        _peopleStorage = peopleStorage;
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> Get()
    {
        return await _peopleStorage.GetPeople();
    }

    [HttpPost]
    public async Task Post(Person person)
    {
        await _peopleStorage.AddPerson(person);
    }
}