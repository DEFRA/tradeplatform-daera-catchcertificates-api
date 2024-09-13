// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V2.Validation.Mmo;

public class StorageDocumentValidator : AbstractValidator<StorageDocument>
{
    public StorageDocumentValidator(
        IValidator<Exporter> exporterValidator,
        IValidator<Product> productValidator,
        IValidator<Country> countryValidator,
        IValidator<Authority> authorityValidator,
        IValidator<Transportation> transportationValidator) : base()
    {
        this.AddCoreValidation<StorageDocument, Exporter, Country>();

        RuleFor(x => x.Version).Equal(2);

        RuleFor(x => x.Exporter).SetValidator(exporterValidator);

        RuleFor(x => x.NumberOfFailedSubmissions).NotNull();

        RuleFor(x => x.CompanyName).NotNull();

        RuleFor(x => x.Products)
            .NotNull()
            .ForEach(x => x.SetValidator(productValidator));

        RuleFor(x => x.ExportedTo).SetValidator(countryValidator);

        RuleFor(x => x.Authority).NotNull().SetValidator(authorityValidator);

        RuleFor(x => x.Transportation).SetValidator(transportationValidator);

        RuleFor(x => x.DA).NotNull();

        RuleFor(x => x.CorrelationId).NotNull();

        RuleFor(x => x.RequestedByAdmin).NotNull();

        RuleFor(x => x.StorageFacilities).NotNull();
    }
}