// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Models.Enums;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using DtosOutboundEhco = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.Mappers
{
    public class EhcoQuestionAnswerMapper(IMapper mapper) : IEhcoQuestionAnswerMapper
    {
        private readonly IMapper _mapper = mapper;

        public IEnumerable<DtosOutboundEhco.ApplicationFormItem> Map(
            IEnumerable<DtosOutboundEhco.ApplicationFormItem> questions,
            bool isOrganisationMatched,
            DtosMmo.CatchCertificateCase catchCertificate,
            IDictionary<long, MmoFieldMap> questionMappings)
        {
            var mappedAnswers = new List<DtosOutboundEhco.ApplicationFormItem>();

            foreach (var item in questions)
            {
                if (!questionMappings.ContainsKey(item.FormQuestionId))
                {
                    continue;
                }

                var mmoFieldMap = questionMappings[item.FormQuestionId];

                if (!IsSensitiveInformationAllowed(mmoFieldMap, isOrganisationMatched))
                {
                    continue;
                }

                var answers = MapAnswers(mmoFieldMap, item, catchCertificate);

                mappedAnswers.AddRange(answers);
            }

            return mappedAnswers;
        }

        private static bool IsSensitiveInformationAllowed(MmoFieldMap mmoFieldMap, bool isOrganisationMatched)
        {
            if (isOrganisationMatched)
            {
                return true;
            }

            if (mmoFieldMap != MmoFieldMap.ExporterAddress
                && mmoFieldMap != MmoFieldMap.ExporterFullNameCompanyName)
            {
                return true;
            }

            return false;
        }

        private static string MapDARegion(DtosMmo.CatchCertificateCase certificate)
        {
            return certificate.DA?.Trim().ToUpper() switch
            {
                "ENGLAND" => "England",
                "SCOTLAND" => "Scotland",
                "WALES" => "Wales",
                _ => null
            };
        }

        private static string MapDAOrigin(DtosMmo.CatchCertificateCase certificate)
        {
            const string greatBritain = "[\"Great Britain\"]";

            return certificate.DA?.Trim().ToUpper() switch
            {
                "ENGLAND" => greatBritain,
                "SCOTLAND" => greatBritain,
                "WALES" => greatBritain,
                "GUERNSEY" => greatBritain,
                "ISLE OF MAN" => greatBritain,
                "JERSEY" => greatBritain,
                "NORTHERN IRELAND" => "[\"Northern Ireland\"]",
                _ => null
            };
        }

        private static string MapExporterFullNameCompanyName(DtosMmo.CatchCertificateCase certificate)
        {
            string[] nameParts =
            [
                certificate.Exporter?.FullName,
                certificate.Exporter?.CompanyName
            ];

            string result = string.Join(" ", nameParts.Where(s => !string.IsNullOrWhiteSpace(s)));

            return string.IsNullOrWhiteSpace(result) ? null : result;
        }

        private static string MapExporterAddress(DtosMmo.CatchCertificateCase certificate)
        {
            string[] addressParts =
            [
                certificate.Exporter?.Address?.Line1,
                certificate.Exporter?.Address?.Line2,
                certificate.Exporter?.Address?.City,
                certificate.Exporter?.Address?.PostCode
            ];

            string fullAddress = string.Join(", ", addressParts.Where(s => !string.IsNullOrWhiteSpace(s)));

            return string.IsNullOrWhiteSpace(fullAddress) ? null : fullAddress;
        }

        private static string MapLandingNetWeight(DtosMmo.Landing landing)
        {
            return landing.Weight > 0
                ? $"{landing.Weight} KGM"
                : null;
        }

        private static string MapConsignmentNetWeight(DtosMmo.CatchCertificateCase certificate)
        {
            double total = certificate.Landings?.Sum(l => l.Weight) ?? 0;

            return total > 0 ? $"{total} KGM" : null;
        }

        private List<DtosOutboundEhco.ApplicationFormItem> MapAnswers(MmoFieldMap mmoFieldMap,
            DtosOutboundEhco.ApplicationFormItem item, DtosMmo.CatchCertificateCase certificate)
        {
            var answers = new List<DtosOutboundEhco.ApplicationFormItem>();

            var clone = _mapper.Map<DtosOutboundEhco.ApplicationFormItem>(item);
            clone.Answer = null;
            clone.PageOccurrence = 0;

            clone.Answer = mmoFieldMap switch
            {
                MmoFieldMap.DARegion => MapDARegion(certificate),
                MmoFieldMap.DAOrigin => MapDAOrigin(certificate),
                MmoFieldMap.ExporterFullNameCompanyName => MapExporterFullNameCompanyName(certificate),
                MmoFieldMap.ExporterAddress => MapExporterAddress(certificate),
                MmoFieldMap.ConsignmentNetWeight => MapConsignmentNetWeight(certificate),
                _ => clone.Answer
            };

            if (clone.Answer != null)
            {
                answers.Add(clone);
            }

            int index = 0;

            foreach (var landing in certificate.Landings ?? [])
            {
                clone = _mapper.Map<DtosOutboundEhco.ApplicationFormItem>(item);
                clone.PageOccurrence = index;

                clone.Answer = mmoFieldMap switch
                {
                    MmoFieldMap.LandingSpecies => landing.Species,
                    MmoFieldMap.LandingNetWeight => MapLandingNetWeight(landing),
                    _ => null
                };

                if (clone.Answer != null)
                {
                    answers.Add(clone);
                }

                index++;
            }

            return answers;
        }
    }
}
