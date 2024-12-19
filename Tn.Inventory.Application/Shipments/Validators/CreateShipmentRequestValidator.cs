using FluentValidation;
using Tn.Inventory.Application.Purchases.Constants;
using Tn.Inventory.Application.Shipments.Requests;

namespace Tn.Inventory.Application.Shipments.Validators;

public class CreateShipmentRequestValidator : AbstractValidator<CreateShipmentRequest>
{
    public CreateShipmentRequestValidator()
    {
        RuleFor(x => x.SourceWarehouseId)
            .NotEqual(0).WithMessage(ApplicationMessages.SourceWarehouseIdIsRequired);
        RuleFor(x => x.TargetWarehouseId)
            .NotEqual(0).WithMessage(ApplicationMessages.TargetWarehouseIdIsRequired);

        RuleFor(x => x.Description)
            .NotNull().WithMessage(ApplicationMessages.DesriptionIsRequired)
            .NotEmpty().WithMessage(ApplicationMessages.DesriptionIsRequired);

        RuleFor(x => x.ShippingAddress)
            .NotNull().WithMessage(ApplicationMessages.ShippingAddressIsRequired)
            .NotEmpty().WithMessage(ApplicationMessages.ShippingAddressIsRequired);

        RuleFor(x => x.ReceiverName)
            .NotNull().WithMessage(ApplicationMessages.ReceiverNameIsRequired)
            .NotEmpty().WithMessage(ApplicationMessages.ReceiverNameIsRequired);

        RuleFor(x => x.ReceiverPhoneNumber)
            .NotNull().WithMessage(ApplicationMessages.ReceiverPhoneNumberIsRequired)
            .NotEmpty().WithMessage(ApplicationMessages.ReceiverPhoneNumberIsRequired);

        RuleFor(x => x.Details)
            .NotNull().WithMessage(ApplicationMessages.DetailsIsRequired);
    }
}