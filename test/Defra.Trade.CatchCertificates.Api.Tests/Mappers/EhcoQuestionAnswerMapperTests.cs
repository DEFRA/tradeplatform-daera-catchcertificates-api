// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Mappers;
using FluentAssertions;
using Xunit;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using DtosOutboundEhco = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.Tests.Mappers
{
    public class EhcoQuestionAnswerMapperTests
    {
        [Theory]
        [InlineData("ENGLAND", "England", "[\"Great Britain\"]")]
        [InlineData("SCOTLAND", "Scotland", "[\"Great Britain\"]")]
        [InlineData("WALES", "Wales", "[\"Great Britain\"]")]
        [InlineData("GUERNSEY", null, "[\"Great Britain\"]")]
        [InlineData("ISLE OF MAN", null, "[\"Great Britain\"]")]
        [InlineData("JERSEY", null, "[\"Great Britain\"]")]
        [InlineData("NORTHERN IRELAND", null, "[\"Northern Ireland\"]")]
        [InlineData("", null, null)]
        public void Map_DevolvedAuthorities_Success(string inboundDA, string expectedRegion, string expectedOrigin)
        {
            var itemUnderTest = BuildItemUnderTest();

            var questions = new List<DtosOutboundEhco.ApplicationFormItem>
            {
                new() { FormQuestionId = 123L },
                new() { FormQuestionId = 234L }
            };

            var catchCertificate = new DtosMmo.CatchCertificateCase
            {
                DA = inboundDA
            };

            var mappings = new Dictionary<long, Models.Enums.MmoFieldMap>
            {
                { 123L, Models.Enums.MmoFieldMap.DARegion },
                { 234L, Models.Enums.MmoFieldMap.DAOrigin }
            };

            var result = itemUnderTest.Map(questions, false, catchCertificate, mappings);

            VerifyQuestionAnswer(result, 123L, expectedRegion);
            VerifyQuestionAnswer(result, 234L, expectedOrigin);
        }

        [Theory]
        [InlineData("John Smith", "Distribution", true, "John Smith Distribution")]
        [InlineData(null, "Distribution", true, "Distribution")]
        [InlineData("John Smith", null, true, "John Smith")]
        [InlineData(null, null, true, null)]
        [InlineData("John Smith", "Distribution", false, null)]
        public void Map_ExporterFullNameCompanyName_Success(string exporterFullName, string exporterCompanyName, bool isOrganisationMatched, string expectedAnswer)
        {
            var itemUnderTest = BuildItemUnderTest();

            var questions = new List<DtosOutboundEhco.ApplicationFormItem>
            {
                new() { FormQuestionId = 45L }
            };

            var catchCertificate = new DtosMmo.CatchCertificateCase
            {
                Exporter = new DtosMmo.Exporter
                {
                    FullName = exporterFullName,
                    CompanyName = exporterCompanyName
                }
            };

            var mappings = new Dictionary<long, Models.Enums.MmoFieldMap>
            {
                { 45L, Models.Enums.MmoFieldMap.ExporterFullNameCompanyName }
            };

            var result = itemUnderTest.Map(questions, isOrganisationMatched, catchCertificate, mappings);

            VerifyQuestionAnswer(result, 45L, expectedAnswer);
        }

        [Theory]
        [InlineData("123 The Road", "Village", "Town", "M1 6HT", true, "123 The Road, Village, Town, M1 6HT")]
        [InlineData("123 The Road", null, "Town", null, true, "123 The Road, Town")]
        [InlineData(null, null, null, null, true, null)]
        [InlineData("123 The Road", "Village", "Town", "M1 6HT", false, null)]
        public void Map_ExporterAddress_Success(string line1, string line2, string city, string postcode, bool isOrganisationMatched, string expectedAnswer)
        {
            var itemUnderTest = BuildItemUnderTest();

            var questions = new List<DtosOutboundEhco.ApplicationFormItem>
            {
                new() { FormQuestionId = 777L }
            };

            var catchCertificate = new DtosMmo.CatchCertificateCase
            {
                Exporter = new DtosMmo.Exporter
                {
                    Address = new DtosMmo.Address
                    {
                        Line1 = line1,
                        Line2 = line2,
                        City = city,
                        PostCode = postcode
                    }
                }
            };

            var mappings = new Dictionary<long, Models.Enums.MmoFieldMap>
            {
                { 777L, Models.Enums.MmoFieldMap.ExporterAddress }
            };

            var result = itemUnderTest.Map(questions, isOrganisationMatched, catchCertificate, mappings);

            VerifyQuestionAnswer(result, 777L, expectedAnswer);
        }

        [Theory]
        [InlineData(100.5, 50, "100.5 KGM", "50 KGM")]
        [InlineData(75, 0, "75 KGM", null)]
        [InlineData(0, 45, null, "45 KGM")]
        [InlineData(0, 0, null, null)]
        public void Map_LandingNetWeight_Success(double weight1, double weight2, string expectedAnswer1, string expectedAnswer2)
        {
            var itemUnderTest = BuildItemUnderTest();

            var questions = new List<DtosOutboundEhco.ApplicationFormItem>
            {
                new() { FormQuestionId = 468L }
            };

            var catchCertificate = new DtosMmo.CatchCertificateCase
            {
                Landings = new[] { weight1, weight2 }.Select(weight => new DtosMmo.Landing { Weight = weight })
            };

            var mappings = new Dictionary<long, Models.Enums.MmoFieldMap>
            {
                { 468L, Models.Enums.MmoFieldMap.LandingNetWeight }
            };

            var result = itemUnderTest.Map(questions, false, catchCertificate, mappings);

            VerifyQuestionAnswer(result, 468L, expectedAnswer1);
            VerifyQuestionAnswer(result, 468L, expectedAnswer2, 1);
        }

        [Theory]
        [InlineData("COD", "HAD", "COD", "HAD")]
        [InlineData("COD", null, "COD", null)]
        [InlineData(null, "HAD", null, "HAD")]
        [InlineData(null, null, null, null)]
        public void Map_LandingSpecies_Success(string species1, string species2, string expectedAnswer1, string expectedAnswer2)
        {
            var itemUnderTest = BuildItemUnderTest();

            var questions = new List<DtosOutboundEhco.ApplicationFormItem>
            {
                new() { FormQuestionId = 33L }
            };

            var catchCertificate = new DtosMmo.CatchCertificateCase
            {
                Landings = new[] { species1, species2 }.Select(s => new DtosMmo.Landing { Species = s })
            };

            var mappings = new Dictionary<long, Models.Enums.MmoFieldMap>
            {
                { 33L, Models.Enums.MmoFieldMap.LandingSpecies }
            };

            var result = itemUnderTest.Map(questions, false, catchCertificate, mappings);

            VerifyQuestionAnswer(result, 33L, expectedAnswer1);
            VerifyQuestionAnswer(result, 33L, expectedAnswer2, 1);
        }

        [Theory]
        [InlineData(100, -1, -1, "100 KGM")]
        [InlineData(100, 50, -1, "150 KGM")]
        [InlineData(45.5, 10.25, 2.3, "58.05 KGM")]
        [InlineData(0, 0, 0, null)]
        [InlineData(-1, -1, -1, null)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Bug", "S1244:Floating point numbers should not be tested for equality", Justification = "used for test data setup")]
        public void Map_ConsignmentNetWeight_Success(double weight1, double weight2, double weight3, string expectedAnswer)
        {
            var itemUnderTest = BuildItemUnderTest();

            var questions = new List<DtosOutboundEhco.ApplicationFormItem>
            {
                new() { FormQuestionId = 909L }
            };

            var catchCertificate = new DtosMmo.CatchCertificateCase
            {
                Landings = new[] { weight1, weight2, weight3 }.Where(w => w != -1).Select(weight => new DtosMmo.Landing { Weight = weight })
            };

            var mappings = new Dictionary<long, Models.Enums.MmoFieldMap>
            {
                { 909L, Models.Enums.MmoFieldMap.ConsignmentNetWeight }
            };

            var result = itemUnderTest.Map(questions, false, catchCertificate, mappings);

            VerifyQuestionAnswer(result, 909L, expectedAnswer);
        }

        private static EhcoQuestionAnswerMapper BuildItemUnderTest()
        {
            var mapper = new MapperConfiguration(
                    c => c.AddProfile<OutboundEhcoProfile>())
                .CreateMapper();

            return new EhcoQuestionAnswerMapper(mapper);
        }

        private static void VerifyQuestionAnswer(IEnumerable<DtosOutboundEhco.ApplicationFormItem> items, long formQuestionId, string expectedAnswer, int pageOccurrence = 0)
        {
            if (expectedAnswer != null)
            {
                items.Should().Contain(a => a.FormQuestionId == formQuestionId && a.Answer == expectedAnswer && a.PageOccurrence == pageOccurrence);
            }
            else
            {
                items.Should().NotContain(a => a.FormQuestionId == formQuestionId && a.PageOccurrence == pageOccurrence);
            }
        }
    }
}