using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Validation;

public class DynamicValidationAttribute : ValidationAttribute
{
    private readonly Func<bool> _condition;
    private readonly ValidationAttribute _innerAttribute;

    public DynamicValidationAttribute(Func<bool> condition, ValidationAttribute innerAttribute) {
        _condition = condition;
        _innerAttribute = innerAttribute;
    }

    public override bool IsValid(object? value) => !_condition() || _innerAttribute.IsValid(value);
    public override string FormatErrorMessage(string name) => _innerAttribute.FormatErrorMessage(name);
}