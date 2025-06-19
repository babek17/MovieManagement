using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Attributes;

public class PastDateAttribute: ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime dateValue)
        {
            return dateValue <= DateTime.Today;
        }
        return true;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} cannot be a future date.";
    }
}