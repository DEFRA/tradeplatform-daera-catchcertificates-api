// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Text.Json.Serialization;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    public class Address
    {
        [JsonPropertyName("building_number")]
        public string BuildingNumber { get; set; }

        [JsonPropertyName("sub_building_name")]
        public string SubBuildingName { get; set; }

        [JsonPropertyName("building_name")]
        public string BuildingName { get; set; }

        [JsonPropertyName("street_name")]
        public string StreetName { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }
    }
}