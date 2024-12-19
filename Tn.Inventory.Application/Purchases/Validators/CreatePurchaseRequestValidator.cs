using FluentValidation;
using Tn.Inventory.Application.Purchases.Constants;
using Tn.Inventory.Application.Purchases.Requests;

namespace Tn.Inventory.Application.Purchases.Validators;

public class CreatePurchaseRequestValidator : AbstractValidator<CreatePurchaseRequest>
{
    public CreatePurchaseRequestValidator()
    {
        RuleFor(x => x.SupplierId)
            .NotEqual(0).WithMessage(ApplicationMessages.SupplierIdIsRequired);

        RuleFor(x => x.OrderNumber)
            .NotNull().WithMessage(ApplicationMessages.OrderNumberIsRequired)
            .NotEmpty().WithMessage(ApplicationMessages.OrderNumberIsRequired);

        RuleFor(x => x.Details)
            .NotNull().WithMessage(ApplicationMessages.DetailsIsRequired);
    }
}