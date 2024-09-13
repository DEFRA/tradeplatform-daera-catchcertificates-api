﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.V1.Validation.Mmo
{
    public class ExporterValidator : AbstractValidator<Exporter>
    {
        public ExporterValidator() : base()
        {
            RuleFor(x => x.Address).NotNull();
        }
    }
}
