using System.ComponentModel.DataAnnotations;
using AGIEvents.Lib.ViewModels;

namespace AGIEvents.Lib.Validation;

public class AnyRequiredFieldNotEmpty : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var instance = (AddLeadViewModel)validationContext.ObjectInstance;
        var requiredFields = new List<string> { instance.FirstName, instance.LastName, instance.Company };
        return requiredFields.Any(field => !string.IsNullOrWhiteSpace(field))
            ? ValidationResult.Success
            : new ValidationResult("At least First Name, Last Name or Company is required.");
    }
}