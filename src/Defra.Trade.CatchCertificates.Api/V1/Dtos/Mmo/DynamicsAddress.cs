// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;

public class DynamicsAddress : DynamicsAddressBase
{
    [JsonPropertyName("_defra_country_value_Microsoft_Dynamics_CRM_associatednavigationproperty")]
    public string DefraCountryValueMicrosoftDynamicsCrmAssociatedNavigationProperty { get; set; }

    [JsonPropertyName("_defra_country_value_Microsoft_Dynamics_CRM_lookuplogicalname")]
    public string DefraCountryValueMicrosoftDynamicsCrmLookupLogicalname { get; set; }

    [JsonPropertyName("_defra_country_value_OData_Community_Display_V1_FormattedValue")]
    public string DefraCountryValueODataCommunityDisplayV1FormattedValue { get; set; }

    [JsonPropertyName("defra_fromcompanieshouse_OData_Community_Display_V1_FormattedValue")]
    public string DefraFromCompaniesHouseODataCommunityDisplayV1FormattedValue { get; set; }
}