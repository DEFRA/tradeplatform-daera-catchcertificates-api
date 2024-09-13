// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models.Enums;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using DtosOutboundEhco = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.Mappers
{
    public interface IEhcoQuestionAnswerMapper
    {
        IEnumerable<DtosOutboundEhco.ApplicationFormItem> Map(
            IEnumerable<DtosOutboundEhco.ApplicationFormItem> questions,
            bool isOrganisationMatched,
            DtosMmo.CatchCertificateCase catchCertificate,
            IDictionary<long, MmoFieldMap> questionMappings);
    }
}
