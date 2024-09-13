// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class ProcessingStatementValidator : AbstractValidator<ProcessingStatement>
{
    public ProcessingStatementValidator(
        IValidator<Catch> catchValidator,
        IValidator<Country> countryValidator,
        IValidator<Exporter> exporterValidator,
        IValidator<Authority> authorityValidator,
        IValidator<Address> addressValidator) : base()
    {
        this.AddCoreValidation<ProcessingStatement, Exporter, Country>();

        RuleFor(x => x.Authority).NotNull().SetValidator(authorityValidator);

        RuleForEach(x => x.Catches).SetValidator(catchValidator);

        RuleFor(x => x.CorrelationId).NotNull();

        RuleFor(x => x.DA).NotNull();

        RuleFor(x => x.ExportedTo).SetValidator(countryValidator);

        RuleFor(x => x.Exporter).SetValidator(exporterValidator);

        RuleFor(x => x.HealthCertificateNumber).NotNull();

        RuleFor(x => x.HealthCertificateDate).NotNull();

        RuleFor(x => x.NumberOfFailedSubmissions).NotNull();

        RuleFor(x => x.PlantName).NotNull();

        RuleFor(x => x.PlantAddress).NotNull().SetValidator(addressValidator);

        RuleFor(x => x.PlantApprovalNumber).NotNull();

        RuleFor(x => x.PlantDateOfAcceptance).NotNull();

        RuleFor(x => x.RequestedByAdmin).NotNull();

        RuleFor(x => x.PersonResponsible).NotNull();

        RuleFor(x => x.Version).Equal(2);
    }
}