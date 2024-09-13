// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Dtos;

public class StorageDocumentTests
{
    private static readonly Dictionary<string, string> _json = new()
    {
        ["null.json"] = "null",
        ["Empty.json"] = "{}",
        ["Sample.json"] = "{\"version\": 2,\"exporter\": {\"contactId\": \"a contact id\",\"accountId\": \"an account id\",\"dynamicsAddress\": {\"defra_addressid\": \"00185463-69c2-e911-a97a-000d3a2cbad9\",\"defra_buildingname\": \"Lancaster House\",\"defra_fromcompanieshouse\": false,\"defra_fromcompanieshouse_OData_Community_Display_V1_FormattedValue\": \"No\",\"defra_postcode\": \"NE4 7YJ\",\"defra_premises\": \"23\",\"defra_street\": \"Newcastle upon Tyne\",\"defra_towntext\": \"Newcastle upon Tyne\",\"_defra_country_value\": \"f49cf73a-fa9c-e811-a950-000d3a3a2566\",\"_defra_country_value_Microsoft_Dynamics_CRM_associatednavigationproperty\": \"defra_Country\",\"_defra_country_value_Microsoft_Dynamics_CRM_lookuplogicalname\": \"defra_country\",\"_defra_country_value_OData_Community_Display_V1_FormattedValue\": \"United Kingdom of Great Britain and Northern Ireland\"},\"companyName\": \"FISH LTD\",\"address\": {\"building_number\": \"123\",\"sub_building_name\": \"Unit 1\",\"building_name\": \"CJC Fish Ltd\",\"street_name\": \"17  Old Edinburgh Road\",\"county\": \"West Midlands\",\"country\": \"England\",\"line1\": \"Vue Red\",\"city\": \"ROWTR\",\"postCode\": \"WN90 23A\"}},\"documentUrl\": \"http://tst-gov.uk/asfd9asdfasdf0jsaf.pdf\",\"documentDate\": \"2019-01-01T05:05:05\",\"caseType1\": \"SD\",\"caseType2\": \"Real Time Validation - Overuse Failure\",\"numberOfFailedSubmissions\": 4,\"documentNumber\": \"GBR-SD-234234-234-234\",\"companyName\": \"Bob's Fisheries LTD\",\"exportedTo\": {\"officialCountryName\": \"Nigeria\",\"isoCodeAlpha2\": \"NG\",\"isoCodeAlpha3\": \"NGR\",\"isoNumericCode\": \"3166\"},\"products\": [{\"id\": \"some-product-id-1\",\"foreignCatchCertificateNumber\": \"FR-SD-234234-23423-234234\",\"species\": \"HER\",\"cnCode\": \"324234324432234\",\"scientificName\": \"scientific name\",\"importedWeight\": 500,\"exportedWeight\": 700,\"dateOfUnloading\": \"2023-08-25\",\"placeOfUnloading\": \"Dover\",\"transportUnloadedFrom\": \"Train\",\"validation\": {\"status\": \"Validation Failure - Overuse\",\"totalWeightExported\": 700,\"weightExceededAmount\": 200,\"overuseInfo\": [\"GBR-SD-123234-123-234”,”GBR-SD-123234-123-234\"]}},{\"id\": \"some-product-id-2\",\"foreignCatchCertificateNumber\": \"IRL-SD-4324-423423-234234\",\"species\": \"SAL\",\"cnCode\": \"523842358\",\"scientificName\": \"scientific name\",\"importedWeight\": 200,\"exportedWeight\": 300,\"validation\": {\"status\": \"Validation Success\",\"totalWeightExported\": 300}}],\"storageFacilities\": [{\"name\": \"name\",\"address\": {\"line1\": \"MMO SUB, LANCASTER HOUSE, HAMPSHIRE COURT\",\"city\": \"NEWCASTLE UPON TYNE\",\"postCode\": \"NE4 7YH\",\"sub_building_name\": \"MMO SUB\",\"building_number\": \"\",\"building_name\": \"LANCASTER HOUSE\",\"street_name\": \"HAMPSHIRE COURT\",\"county\": \"TYNESIDE\",\"country\": \"ENGLAND\"}}],\"transportation\": {\"modeofTransport\": \"Vessel\",\"name\": \"consequat\",\"flag\": \"consequat cupidatat labore esse anim\",\"containerId\": \"velit quis commodo aliqua sint\",\"exportLocation\": \"DOVER\",\"exportDate\": \"2023-01-01\"},\"da\": \"Northern Ireland\",\"_correlationId\": \"c03483ba-86ed-49be-ba9d-695ea27b3951\",\"requestedByAdmin\": false,\"authority\": {\"name\": \"Illegal Unreported and Unregulated (IUU) Fishing Team\",\"companyName\": \"Marine Management Organisation\",\"address\": {\"building_number\": \"123\",\"sub_building_name\": \"Unit 1\",\"building_name\": \"CJC Fish Ltd\",\"street_name\": \"17  Old Edinburgh Road\",\"county\": \"West Midlands\",\"country\": \"England\",\"line1\": \"123 Unit 1 CJC Fish Ltd 17 Old Edinburgh Road\",\"city\": \"ROWTR\",\"postCode\": \"WN90 23A\"},\"tel\": \"0300 123 1032\",\"email\": \"ukiuuslo@marinemanagement.org.uk\",\"dateIssued\": \"2023-09-01\"}}"
    };

    public static IEnumerable<object[]> DeserializeJson_Losslessly_TheoryData => _json.Keys.Select(k => new object[] { k }).ToArray();

    [Theory]
    [MemberData(nameof(DeserializeJson_Losslessly_TheoryData))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows", Justification = "Data Enumerates")]
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

        var deserialized = JsonSerializer.Deserialize<StorageDocument>(input, options);
        string reserialized = JsonSerializer.Serialize(deserialized, options);
        var actual = JToken.Parse(reserialized);

        actual.Should().BeEquivalentTo(expected);
    }
}