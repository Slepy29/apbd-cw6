using WebApplication1.Models;

namespace WebApplication1.Validators;

public class AnimalValidators
{
    public static IEnumerable<string> Validate(Animal animal)
    {
        if (animal.IdAnimal < 0)
        {
            yield return "Animals ID has to be greater or equal to 0";
        }

        if (string.IsNullOrWhiteSpace(animal.Name))
        {
            yield return "Animals first name is required";
        }

        if (string.IsNullOrWhiteSpace(animal.Category))
        {
            yield return "Animals category is required";
        }

        if (string.IsNullOrWhiteSpace(animal.Area))
        {
            yield return "Animals area is required";
        }
    }
}