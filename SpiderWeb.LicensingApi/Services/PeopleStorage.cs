using Azure;
using Azure.Data.Tables;
using SpiderWeb.LicensingApi.Interfaces;
using SpiderWeb.LicensingApi.Models;

namespace SpiderWeb.LicensingApi.Services;

public class PeopleStorage : IPeopleStorage
{
    private const string PARTITION_KEY = "ALL";
    private readonly TableClient _tableClient;
    public PeopleStorage(IConfiguration configuration)
    {
        var tableServiceClient = new TableServiceClient(configuration["TABLE_STORAGE_CONNECTION_STRING"]);
        _tableClient = tableServiceClient.GetTableClient(tableName: "Person");

        _tableClient.CreateIfNotExists();
    }

    public async Task AddPerson(Person person)
    {
        var tablePerson = new TablePerson
        {
            PartitionKey = PARTITION_KEY,
            RowKey = Guid.NewGuid().ToString(),
            FirstName = person.FirstName,
            LastName = person.LastName,
        };

        await _tableClient.AddEntityAsync<TablePerson>(tablePerson);
    }

    public Task<IEnumerable<Person>> GetPeople()
    {
        var people = _tableClient.Query<TablePerson>();
        return Task.FromResult(people.Select(p => p.ToPerson()));
    }
}


public class TablePerson : ITableEntity
{
    public string PartitionKey { get; set; } = default!;
    public string RowKey { get; set; } = default!;
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; } = default!;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public Person ToPerson()
    {
        return new Person(FirstName, LastName);
    }
}