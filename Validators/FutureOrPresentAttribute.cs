using System.ComponentModel.DataAnnotations;

namespace TWTodos.Validators;

public class FutureOrPresentAttribute : ValidationAttribute
{
    public FutureOrPresentAttribute()
    {
        ErrorMessage = "O campo {0} deve ser preenchido com uma data presente ou futura.";
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }
        var date = (DateOnly)value;
        return date >= DateOnly.FromDateTime(DateTime.Now);
    }
}