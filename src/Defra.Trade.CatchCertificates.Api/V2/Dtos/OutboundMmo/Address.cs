// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

/// <summary>
/// Address details.
/// </summary>
public class Address
{
    /// <summary>
    /// The building name
    /// </summary>
    public string BuildingName { get; set; }

    /// <summary>
    /// The building number
    /// </summary>
    public string BuildingNumber { get; set; }

    /// <summary>
    /// City / town.
    /// </summary>
    /// <example>Crewe</example>
    public string City { get; set; }

    /// <summary>
    /// Name of the country in which the address resides.
    /// </summary>
    /// <example>United Kingdom of Great Britain and Northern Ireland</example>
    public string Country { get; set; }

    /// <summary>
    /// Name of the county in which the address resides.
    /// </summary>
    /// <example>Cheshire</example>
    public string County { get; set; }

    /// <summary>
    /// First line of the address.
    /// </summary>
    /// <example>Hornbeam House, Defra</example>
    public string Line1 { get; set; }

    /// <summary>
    /// Postcode.
    /// </summary>
    /// <example>CW1 6GJ</example>
    public string PostCode { get; set; }

    /// <summary>
    /// The street name
    /// </summary>
    public string StreetName { get; set; }

    /// <summary>
    /// The sub-building name
    /// </summary>
    public string SubBuildingName { get; set; }
}