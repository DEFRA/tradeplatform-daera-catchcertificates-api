// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Enums;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class TransportationValidator : AbstractValidator<Transportation>
{
    public TransportationValidator()
    {
        RuleFor(x => x.ModeOfTransport)
            .NotNull()
            .IsInEnum();

        When(x => x != null && x.ModeOfTransport == ModeOfTransport.Train, () =>
        {
            RuleFor(x => x.BillNumber)
                .NotNull();

            RuleFor(x => x.ExportLocation)
                .NotNull();
        });

        When(x => x != null && x.ModeOfTransport == ModeOfTransport.Plane, () =>
        {
            RuleFor(x => x.FlightNumber)
                .NotNull();

            RuleFor(x => x.ContainerId)
                .NotNull();

            RuleFor(x => x.ExportLocation)
                .NotNull();
        });

        When(x => x != null && x.ModeOfTransport == ModeOfTransport.Vessel, () =>
        {
            RuleFor(x => x.Name)
                .NotNull();

            RuleFor(x => x.Flag)
                .NotNull();

            RuleFor(x => x.ContainerId)
                .NotNull();

            RuleFor(x => x.ExportLocation)
                .NotNull();
        });
    }
}