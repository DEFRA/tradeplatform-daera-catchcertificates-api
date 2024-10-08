// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Text.Json.Serialization;

namespace Defra.Trade.CatchCertificates.Api.Models;

public abstract class DynamicsAddressBase
{
    [JsonPropertyName("defra_addressid")]
    public Guid? DefraAddressId { get; set; }

    [JsonPropertyName("defra_buildingname")]
    public string DefraBuildingName { get; set; }

    [JsonPropertyName("_defra_country_value")]
    public Guid? DefraCountryValue { get; set; }

    [JsonPropertyName("defra_county")]
    public string DefraCounty { get; set; }

    [JsonPropertyName("defra_dependentlocality")]
    public string DefraDependentLocality { get; set; }

    [JsonPropertyName("defra_fromcompanieshouse")]
    public bool? DefraFromCompaniesHouse { get; set; }

    [JsonPropertyName("defra_internationalpostalcode")]
    public string DefraInternationalPostalCode { get; set; }

    [JsonPropertyName("defra_locality")]
    public string DefraLocality { get; set; }

    [JsonPropertyName("defra_postcode")]
    public string DefraPostcode { get; set; }

    [JsonPropertyName("defra_premises")]
    public string DefraPremises { get; set; }

    [JsonPropertyName("defra_street")]
    public string DefraStreet { get; set; }

    [JsonPropertyName("defra_subbuildingname")]
    public string DefraSubBuildingName { get; set; }

    [JsonPropertyName("defra_towntext")]
    public string DefraTownText { get; set; }

    [JsonPropertyName("defra_uprn")]
    public string DefraUprn { get; set; }
}