// See https://aka.ms/new-console-template for more information

using Bogus;
using Champ.API;
using Champ.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

var conf = new ConfigurationBuilder()
    .AddJsonFile("/home/student/RiderProjects/Champ/src/Champ.API/appsettings.json")
    .Build();

var loggerFactory = LoggerFactory.Create(builder => builder.AddSimpleConsole());

var context = new ApplicationContext(conf, loggerFactory.CreateLogger<DbContext>());

var customInstantiator = new Faker<Patient>("ru")
    .CustomInstantiator(f => new Patient() {
        GenderId = f.PickRandom(1, 2),
        Address = f.Address.FullAddress(),
        DateOfBirth = f.Person.DateOfBirth,
        Email = f.Person.Email,
        FirstName = f.Person.FirstName,
        LastName = f.Person.LastName,
        MiddleName = f.PickRandom(f.Person.UserName, ""),
        Photo = GetPhoto().Result,
        PassportNumber = f.Random.Long(0L, 9999999999L),
        PhoneNumber = f.Person.Phone
    });
var patients = customInstantiator.GenerateLazy(100);

await context.Patients.AddRangeAsync(patients);
await context.SaveChangesAsync();
return;

async Task<byte[]> GetPhoto() {
    using var httpClient = new HttpClient();
    var bytes = await httpClient.GetByteArrayAsync("https://thispersondoesnotexist.com/");
    return bytes;
}