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

public class ProcessingStatementTests
{
    private static readonly Dictionary<string, string> _json = new()
    {
        ["null.json"] = "null",
        ["Empty.json"] = "{}",
        ["Sample.json"] = "{\"version\": 2,\"exporter\": {\"contactId\": \"a contact id\",\"accountId\": \"an account id\",\"dynamicsAddress\": {\"defra_addressid\": \"00185463-69c2-e911-a97a-000d3a2cbad9\",\"defra_buildingname\": \"Lancaster House\",\"defra_fromcompanieshouse\": false,\"defra_fromcompanieshouse_OData_Community_Display_V1_FormattedValue\": \"No\",\"defra_postcode\": \"NE4 7YJ\",\"defra_premises\": \"23\",\"defra_street\": \"Newcastle upon Tyne\",\"defra_towntext\": \"Newcastle upon Tyne\",\"_defra_country_value\": \"f49cf73a-fa9c-e811-a950-000d3a3a2566\",\"_defra_country_value_Microsoft_Dynamics_CRM_associatednavigationproperty\": \"defra_Country\",\"_defra_country_value_Microsoft_Dynamics_CRM_lookuplogicalname\": \"defra_country\",\"_defra_country_value_OData_Community_Display_V1_FormattedValue\": \"United Kingdom of Great Britain and Northern Ireland\"},\"companyName\": \"FISH LTD\",\"address\": {\"building_number\": \"123\",\"sub_building_name\": \"Unit 1\",\"building_name\": \"CJC Fish Ltd\",\"street_name\": \"17  Old Edinburgh Road\",\"county\": \"West Midlands\",\"country\": \"England\",\"line1\": \"123 Unit 1 CJC Fish Ltd 17 Old Edinburgh Road\",\"city\": \"ROWTR\",\"postCode\": \"WN90 23A\"}},\"documentUrl\": \"http://tst-gov.uk/asfd9asdfasdf0jsaf.pdf\",\"documentDate\": \"2019-01-01T05:05:05\",\"caseType1\": \"PS\",\"caseType2\": \"Real Time Validation - Overuse Failure\",\"numberOfFailedSubmissions\": 4,\"documentNumber\": \"GBR-PS-234234-234-234\",\"plantName\": \"Fish Food LTD.\",\"plantAddress\": {\"building_number\": \"123\",\"sub_building_name\": \"Unit 1\",\"building_name\": \"CJC Fish Ltd\",\"street_name\": \"17  Old Edinburgh Road\",\"county\": \"West Midlands\",\"country\": \"England\",\"line1\": \"123 Unit 1 CJC Fish Ltd 17 Old Edinburgh Road\",\"city\": \"Baureport\",\"postCode\": \"NE40 3AJ\"},\"plantApprovalNumber\": \"32423940234234\",\"plantDateOfAcceptance\": \"2019-01-01\",\"personResponsible\": \"Mr. Bob\",\"processedFisheryProducts\": \"Cooked Squid Rings (1605540090), Cooked Atlantic Cold Water Prawns (1605211096),\",\"exportedTo\": {\"officialCountryName\": \"Nigeria\",\"isoCodeAlpha2\": \"NG\",\"isoCodeAlpha3\": \"NGR\",\"isoNumericCode\": \"3166\"},\"catches\": [{\"foreignCatchCertificateNumber\": \"FR-PS-234234-23423-234234\",\"id\": \"GBR-PS-234234-234-234-1234567890\",\"species\": \"HER\",\"cnCode\": \"324234324432234\",\"scientificName\": \"scientific name\",\"importedWeight\": 500,\"usedWeightAgainstCertificate\": 700,\"processedWeight\": 800,\"validation\": {\"status\": \"Validation Failure - Overuse\",\"totalUsedWeightAgainstCertificate\": 700,\"weightExceededAmount\": 300,\"overuseInfo\": [\"GBR-PS-123234-123-234”,”GBR-PS-123234-123-234\"]}},{\"foreignCatchCertificateNumber\": \"IRL-PS-4324-423423-234234\",\"id\": \"GBR-PS-234234-234-234-1234567890\",\"species\": \"SAL\",\"cnCode\": \"523842358\",\"scientificName\": \"scientific name\",\"importedWeight\": 200,\"usedWeightAgainstCertificate\": 100,\"processedWeight\": 150,\"validation\": {\"status\": \"Validation Success\",\"totalUsedWeightAgainstCertificate\": 200}}],\"healthCertificateNumber\": \"20/2/123456\",\"healthCertificateDate\": \"2023-08-25\",\"da\": \"Northern Ireland\",\"_correlationId\": \"c03483ba-86ed-49be-ba9d-695ea27b3951\",\"requestedByAdmin\": true,\"authority\": {\"name\": \"Illegal Unreported and Unregulated (IUU) Fishing Team\",\"companyName\": \"Marine Management Organisation\",\"address\": {\"building_number\": \"123\",\"sub_building_name\": \"Unit 1\",\"building_name\": \"CJC Fish Ltd\",\"street_name\": \"17  Old Edinburgh Road\",\"county\": \"West Midlands\",\"country\": \"England\",\"line1\": \"123 Unit 1 CJC Fish Ltd 17 Old Edinburgh Road\",\"city\": \"ROWTR\",\"postCode\": \"WN90 23A\"},\"tel\": \"0300 123 1032\",\"email\": \"ukiuuslo@marinemanagement.org.uk\",\"dateIssued\": \"2023-09-01\"}}"
    };

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

        var deserialized = JsonSerializer.Deserialize<ProcessingStatement>(input, options);
        string reserialized = JsonSerializer.Serialize(deserialized, options);
        var actual = JToken.Parse(reserialized);

        actual.Should().BeEquivalentTo(expected);
    }
}