using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]                 
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    [HttpGet]
    public IActionResult GetAnimals()
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Select * from Animal;";

        var reader = command.ExecuteReader();
        
        var animals = new List<Animal>();
        int animalOriginal = reader.GetOrdinal("IdAnimal");
        int nameOriginal = reader.GetOrdinal("Name");
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(animalOriginal),
                Name = reader.GetString(nameOriginal)
            });
        }
        return Ok(animals);
    }
}