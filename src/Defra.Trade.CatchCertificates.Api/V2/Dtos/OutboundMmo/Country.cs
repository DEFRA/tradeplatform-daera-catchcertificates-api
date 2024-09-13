// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

/// <summary>
/// Definition of a country.
/// </summary>
public class Country
{
    /// <summary>
    /// The ISO 2 digit code of this country.
    /// </summary>
    /// <example>GB</example>
    public string IsoCodeAlpha2 { get; set; }

    /// <summary>
    /// The ISO 3 digit code of this country.
    /// </summary>
    /// <example>GBR</example>
    public string IsoCodeAlpha3 { get; set; }

    /// <summary>
    /// The ISO numeric code of this country.
    /// </summary>
    /// <example>826</example>
    public string IsoNumericCode { get; set; }

    /// <summary>
    /// Name of the country.
    /// </summary>
    /// <example>United Kingdom of Great Britain and Northern Ireland</example>
    public string OfficialCountryName { get; set; }
}