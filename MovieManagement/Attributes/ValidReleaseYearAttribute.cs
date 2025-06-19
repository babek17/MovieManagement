using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Attributes;

public class ValidReleaseYearAttribute: ValidationAttribute
{
    private readonly int _minYear;

    public ValidReleaseYearAttribute(int minYear = 1888)
    {
        _minYear = minYear;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int year)
        {
            int currentYear = DateTime.Now.Year;
            if (year < _minYear || year > currentYear)
            {
                return new ValidationResult($"Release year must be between {_minYear} and {currentYear}.");
            }
        }
        return ValidationResult.Success!;
    }
}