using SpiderWeb.LicensingApi.Models;

namespace SpiderWeb.LicensingApi.Interfaces;

public interface IPeopleStorage
{
    public Task AddPerson(Person person);
    public Task<IEnumerable<Person>> GetPeople();
}