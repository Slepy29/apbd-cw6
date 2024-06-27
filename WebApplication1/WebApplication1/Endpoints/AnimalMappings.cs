using WebApplication1.Models;
using WebApplication1.Models.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Endpoints;

public static class AnimalMappings
{
    public static void MapAnimalEndpoints(this WebApplication app)
    {
        app.MapGet("/animals", (AnimalRepository db, string orderBy) =>
        {
            List<Animal> animals = db.GetAnimals(orderBy);

            return Results.Ok(animals);
        });

        app.MapGet("/animals/{id}", (int id, AnimalRepository db) =>
        {
            var animal = db.GetAnimals("").Find(animal => animal.IdAnimal == id);

            return animal is not null
                ? Results.Ok(animal)
                : Results.NotFound();
        });

        app.MapPost("/animals", (AddAnimal animal, AnimalRepository db) =>
        {
            db.AddAnimal(animal);
            return Results.Created("", animal);
        });

        app.MapPut("/animals/{id}", (int id, ChangeAnimal animal, AnimalRepository db) =>
        {
            var animalToUpdate = db.GetAnimals("").FirstOrDefault(a => a.IdAnimal == id);

            if (animalToUpdate == null)
            {
                return Results.NotFound("Cannot find animal with this id");
            }

            db.UpdateAnimal(id, animal);
            return Results.Created($"/animals/{id}", animal);
        });

        app.MapDelete("/animals/{id}", (int id, AnimalRepository db) =>
        {
            var animalToDelete = db.GetAnimals("").FirstOrDefault(a => a.IdAnimal == id);

            if (animalToDelete == null)
            {
                return Results.NotFound("Cannot find animal with this id");
            }

            db.RemoveAnimal(id);
            return Results.NoContent();
        });
    }
}