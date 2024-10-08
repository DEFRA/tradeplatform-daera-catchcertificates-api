// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Dtos;

public class CatchCertificateCaseTests
{
    public static IEnumerable<object[]> DeserializeJson_Losslessly_TheoryData => _json.Keys.Select(k => new object[] { k }).ToArray();

    [Theory]
    [MemberData(nameof(DeserializeJson_Losslessly_TheoryData))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows", Justification = "Data enumerates")]
    public void DeserializeJson_Losslessly(string jsonName)
    {
        string input = _json[jsonName];
        var expected = JToken.Parse(input);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        var deserialized = JsonSerializer.Deserialize<CatchCertificateCase>(input, options);
        string reserialized = JsonSerializer.Serialize(deserialized, options);
        var actual = JToken.Parse(reserialized);

        actual.Should().BeEquivalentTo(expected);
    }

    private static readonly Dictionary<string, string> _json = new()
    {
        ["null.json"] = "null",
        ["Empty.json"] = "{}",
        ["Sample.json"] = "{\"version\": 2,\"exporter\": {\"contactId\": \"a contact id\",\"accountId\": \"an account id\",\"dynamicsAddress\": {\"defra_addressid\": \"00185463-69c2-e911-a97a-000d3a2cbad9\",\"defra_buildingname\": \"Lancaster House\",\"defra_fromcompanieshouse\": false,\"defra_fromcompanieshouse_OData_Community_Display_V1_FormattedValue\": \"No\",\"defra_postcode\": \"NE4 7YJ\",\"defra_premises\": \"23\",\"defra_street\": \"Newcastle upon Tyne\",\"defra_towntext\": \"Newcastle upon Tyne\",\"_defra_country_value\": \"f49cf73a-fa9c-e811-a950-000d3a3a2566\",\"_defra_country_value_Microsoft_Dynamics_CRM_associatednavigationproperty\": \"defra_Country\",\"_defra_country_value_Microsoft_Dynamics_CRM_lookuplogicalname\": \"defra_country\",\"_defra_country_value_OData_Community_Display_V1_FormattedValue\": \"United Kingdom of Great Britain and Northern Ireland\"},\"fullName\": \"Bob\",\"companyName\": \"FISH LTD\",\"address\": {\"building_number\": \"123\",\"sub_building_name\": \"Unit 1\",\"building_name\": \"CJC Fish Ltd\",\"street_name\": \"17  Old Edinburgh Road\",\"county\": \"West Midlands\",\"country\": \"England\",\"line1\": \"123 Unit 1 CJC Fish Ltd 17 Old Edinburgh Road\",\"city\": \"ROWTR\",\"postCode\": \"WN90 23A\"}},\"da\": \"Northern Ireland\",\"certStatus\": \"COMPLETE\",\"caseType1\": \"CC\",\"caseType2\": \"Pending Landing Data\",\"documentNumber\": \"GBR-2023-CC-C3A826515\",\"documentUrl\": \"http://tst-gov.uk/asfd9asdfasdf0jsaf.pdf\",\"documentDate\": \"2019-01-01T05:05:05\",\"numberOfFailedSubmissions\": 5,\"isDirectLanding\": true,\"exportedTo\": {\"officialCountryName\": \"Nigeria\",\"isoCodeAlpha2\": \"NG\",\"isoCodeAlpha3\": \"NGR\",\"isoNumericCode\": \"3166\"},\"multiVesselSchedule\": false,\"landings\": [{\"status\": \"Pending Landing Data\",\"id\": \"2342340-24513aga-adfrewbf\",\"landingDate\": \"2023-06-10T01:34:05\",\"is14DayLimitReached\": false,\"species\": \"Atlantic Herring (HER)\",\"state\": \"Fresh\",\"presentation\": \"Gutted\",\"cnCode\": \"some commodity code\",\"adminCommodityCode\": \"admin commodity code\",\"commodityCodeDescription\": \"some decriptions\",\"scientificName\": \"scientific name\",\"vesselName\": \"The vessel Name\",\"licenceHolder\": \"The vessel Master\",\"vesselPln\": \"GB-3423\",\"vesselLength\": 10,\"vesselAdministration\": \"Northern Ireland\",\"flag\": \"GBR\",\"homePort\": \"PLYMOUTH\",\"catchArea\": \"FAO18\",\"fishingLicenceNumber\": \"50424\",\"fishingLicenceValidTo\": \"2030-12-31T00:00:00\",\"weight\": 500,\"speciesAlias\": \"Y\",\"speciesAnomaly\": \"SQR\",\"numberOfTotalSubmissions\": 1,\"vesselOverriddenByAdmin\": true,\"adminSpecies\": \"Sand smelt (ATP)\",\"adminState\": \"Fresh\",\"adminPresentation\": \"Whole\",\"speciesOverriddenByAdmin\": true,\"validation\": {\"liveExportWeight\": 529,\"rawLandingsUrl\": \"http://www.google.co.uk\",\"salesNoteUrl\": \"http://www.google.co.uk\",\"totalLiveForExportSpecies\": 529,\"isLegallyDue\": false},\"risking\": {\"vessel\": \"0.5\",\"speciesRisk\": \"0.5\",\"exporterRiskScore\": \"0.5\",\"landingRiskScore\": \"0.5\",\"highOrLowRisk\": \"Low\",\"isSpeciesRiskEnabled\": false,\"overuseInfo\": [\"GBR-2020-CC-06E48913F\",\"GBR-2020-CC-649E9E6F9\"]},\"dataEverExpected\": true,\"landingDataExpectedDate\": \"2023-05-26\",\"landingDataEndDate\": \"2023-06-05\"}],\"_correlationId\": \"c03483ba-86ed-49be-ba9d-695ea27b3951\",\"requestedByAdmin\": false,\"isUnblocked\": true,\"vesselOverriddenByAdmin\": false,\"speciesOverriddenByAdmin\": true,\"failureIrrespectiveOfRisk\": false,\"audits\": [{\"auditOperation\": \"INVESTIGATED\",\"user\": \"in eu\",\"auditAt\": \"dolore\",\"investigationStatus\": \"MINOR_VERBAL\"},{\"auditOperation\": \"INVESTIGATED\",\"user\": \"pariatur culpa consequat aliquip\",\"auditAt\": \"nulla dolore eu sint culpa\",\"investigationStatus\": \"USER_EDUCATION_PROVIDED\"},{\"auditOperation\": \"PREAPPROVED\",\"user\": \"ad dolor ipsum deserunt\",\"auditAt\": \"minim enim\",\"investigationStatus\": \"UNDER_INVESTIGATION\"}],\"transportation\": {\"modeofTransport\": \"Vessel\",\"name\": \"consequat\",\"flag\": \"consequat cupidatat labore esse anim\",\"containerId\": \"velit quis commodo aliqua sint\",\"exportLocation\": \"DOVER\"},\"lastUpdated\": \"2023-10-31T15:05:03.0764591Z\",\"lastUpdatedBy\": \"MMO\",\"lastUpdatedSystem\": \"MMO\"}"
    };
}