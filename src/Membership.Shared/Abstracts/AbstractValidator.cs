namespace Membership.Shared.Abstracts;
public abstract class AbstractValidator<T> : IValidator<T>
{
    readonly IMembershipMessageLocalizer _localizer;

    public AbstractValidator(IMembershipMessageLocalizer localizer)
    {
        _localizer = localizer;
    }

    public IEnumerable<MembershipError> Validate(T entity)
    {
        List<MembershipError> errors = new();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var propertyErrors = ValidateProperty(entity, property.Name);
            if(propertyErrors.Any())
                errors.AddRange(propertyErrors);
        }
        return errors;
    }

    protected abstract void ValidatePropertyRules(T entity, string propertyName, List<MembershipError> errors);

    public IEnumerable<MembershipError> ValidateProperty(T entity, string propertyName)
    {
        List<MembershipError> errors = new();
        ValidatePropertyRules(entity, propertyName, errors);
        return errors;
    }

    protected bool ValidateRule(Func<bool> predicate, string propertyName, string errorMessageKey, List<MembershipError> errors)
    {
        bool result = true;
        if (!predicate())
        {
            errors.Add(new (propertyName, _localizer[errorMessageKey]));
            result = false;
        }
        return result;
    }
}
