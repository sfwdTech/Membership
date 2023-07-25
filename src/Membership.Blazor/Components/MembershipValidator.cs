﻿using Membership.Shared.Interfaces;
using Membership.Shared.ValueObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Membership.Blazor.Components;
public class MembershipValidator<T> : ComponentBase
{
    [CascadingParameter]
    EditContext EditContext { get; set; }

    [Parameter]
    public IValidator<T> Validator { get; set; }

    ValidationMessageStore ValidationMessageStore { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        EditContext previousEditContext = EditContext;
        await base.SetParametersAsync(parameters);
        if (EditContext != previousEditContext)
        {
            ValidationMessageStore = new ValidationMessageStore(EditContext);
            EditContext.OnValidationRequested += ValidationRequested;
            EditContext.OnFieldChanged += FieldChanged;
        }
    }

    void HandleErrors(object model, IEnumerable<MembershipError> errors)
    {
        if (errors != null && errors.Any())
        {
            foreach (var error in errors)
            {
                ValidationMessageStore.Add(new FieldIdentifier(model, error.Code),
                    error.Description);
            }
        }
    }

    void ValidationRequested(object sender, ValidationRequestedEventArgs e)
    {
        ValidationMessageStore.Clear();
        var result = Validator.Validate((T)EditContext.Model);
        HandleErrors(EditContext.Model, result);
    }

    void FieldChanged(object sender, FieldChangedEventArgs e)
    {
        FieldIdentifier fieldIdentifier = e.FieldIdentifier;
        ValidationMessageStore.Clear(fieldIdentifier);
        var result = Validator.ValidateProperty((T)fieldIdentifier.Model,
            fieldIdentifier.FieldName);
        HandleErrors(fieldIdentifier.Model, result);
    }

    public void TrySetErrorsFromHttpRequestException(HttpRequestException ex)
    {
        if(ex.Data.Contains("Errors"))
        {
            IEnumerable<MembershipError> errors = ex.Data["Errors"] as IEnumerable<MembershipError>;

            if(errors != null && errors.Any())
            {
                ValidationMessageStore.Clear();
                HandleErrors(EditContext.Model, errors);
                EditContext.NotifyValidationStateChanged();
            }
        }
    }
}
