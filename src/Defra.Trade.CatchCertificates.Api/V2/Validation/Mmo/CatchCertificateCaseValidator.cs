// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class CatchCertificateCaseValidator : AbstractValidator<CatchCertificateCase>
{
    public CatchCertificateCaseValidator(
        IValidator<Landing> landingValidator,
        IValidator<Country> countryValidator,
        IValidator<Exporter> exporterValidator,
        IValidator<Audit> auditValidator,
        IValidator<Transportation> transportValidator)
    {
        this.AddCoreValidation<CatchCertificateCase, Exporter, Country>();

        RuleFor(x => x.Version)
            .Must(x => x.Equals(2));

        RuleFor(x => x.Exporter)
            .SetValidator(exporterValidator)
            .ChildRules(exporter =>
            {
                exporter.RuleFor(x => x.FullName)
                    .NotNull();
            });

        RuleFor(x => x.ExportedTo)
            .NotNull()
            .SetValidator(countryValidator);

        RuleFor(x => x.CertStatus)
            .NotNull();

        RuleFor(x => x.DA)
           .NotNull();

        RuleFor(x => x.Transportation)
            .NotNull()
            .SetValidator(transportValidator);

        RuleForEach(x => x.Landings)
            .SetValidator(landingValidator);

        RuleForEach(x => x.Audits)
            .SetValidator(auditValidator);

        RuleFor(x => x.FailureIrrespectiveOfRisk)
            .NotNull();

        RuleFor(x => x.MultiVesselSchedule)
            .NotNull();
    }
}