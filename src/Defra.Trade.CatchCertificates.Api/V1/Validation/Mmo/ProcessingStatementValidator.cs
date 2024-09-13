// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo;

public class ProcessingStatementValidator : AbstractValidator<ProcessingStatement>
{
    public ProcessingStatementValidator() : base()
    {
        this.AddCoreValidation<ProcessingStatement, Exporter, Country>();
    }
}