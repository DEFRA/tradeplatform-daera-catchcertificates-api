// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using FluentValidation;

namespace Defra.Trade.CatchCertificates.Api.Models;

public static class MessageCoreValidation
{
    public static void AddCoreValidation<TModel, TExporter, TCountry>(this AbstractValidator<TModel> validator)
        where TModel : MessageCore<TExporter, TCountry>
    {
        validator.RuleFor(x => x.Exporter).NotNull();

        validator.RuleFor(x => x.CaseType1).NotNull();

        validator.RuleFor(x => x.CaseType2).NotNull();

        validator.RuleFor(x => x.DocumentNumber).NotEmpty();

        validator.RuleFor(x => x.LastUpdated).NotNull().GreaterThanOrEqualTo(new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero)).LessThanOrEqualTo(new DateTimeOffset(2999, 12, 31, 23, 59, 59, TimeSpan.Zero));

        validator.RuleFor(x => x.LastUpdatedBy).NotNull().Length(1, 100);

        validator.RuleFor(x => x.LastUpdatedSystem).NotNull().Length(1, 10);
    }
}