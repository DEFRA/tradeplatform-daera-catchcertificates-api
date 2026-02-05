// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V3.Dtos;

public class StorageDocumentTests
{
    private static readonly Dictionary<string, string> _json = new()
    {
        ["null.json"] = "null",
        ["Empty.json"] = "{}",
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
