using FluentValidation;
using Tn.Inventory.Application.Purchases.Constants;
using Tn.Inventory.Application.Purchases.Requests;

namespace Tn.Inventory.Application.Purchases.Validators;

public class CompletePurchaseRequestValidator : AbstractValidator<CompletePurchaseRequest>
{
    public CompletePurchaseRequestValidator()
    {
        RuleFor(x => x.PurchaseId)
            .NotEqual(0).WithMessage(ApplicationMessages.PurchaseIdIsRequired);

        RuleFor(x => x.DeliveryDate)
            .NotNull().WithMessage(ApplicationMessages.DeliveryDateIsRequired)
            .NotEmpty().WithMessage(ApplicationMessages.OrderNumberIsRequired);
    }
}