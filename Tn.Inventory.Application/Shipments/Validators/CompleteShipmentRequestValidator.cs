using FluentValidation;
using Tn.Inventory.Application.Shipments.Constants;
using Tn.Inventory.Application.Shipments.Requests;

namespace Tn.Inventory.Application.Shipments.Validators;

public class CompleteShipmentRequestValidator : AbstractValidator<CompleteShipmentRequest>
{
    public CompleteShipmentRequestValidator()
    {
        RuleFor(x => x.ShipmentId)
            .NotEqual(0).WithMessage(ApplicationMessages.ShipmentIdIsRequired);

        RuleFor(x => x.DeliveryDate)
            .NotNull().WithMessage(ApplicationMessages.DeliveryDateIsRequired)
            .NotEmpty().WithMessage(ApplicationMessages.OrderNumberIsRequired);
    }
}