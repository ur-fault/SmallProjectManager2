using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Validation;

public class ToPastAttribute : ValidationAttribute
{
    private readonly DateTime _minimumDate;
    private readonly DateTime _maximumDate;
    private readonly bool _allowNull;

    public ToPastAttribute(int yearsInThePast, bool allowNull = false) {
        _minimumDate = DateTime.Today.AddYears(-yearsInThePast);
        _maximumDate = DateTime.Today;
        _allowNull = allowNull;
    }

    public override bool IsValid(object? value) {
        if (_allowNull && value is null)
            return true;
        return value is DateTime date && date >= _minimumDate && date <= _maximumDate;
    }

    public override string FormatErrorMessage(string name) =>
        $"The filed {name} must be between {_minimumDate} and {_maximumDate}";
}