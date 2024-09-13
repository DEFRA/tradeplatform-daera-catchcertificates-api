// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Exceptions;
using Defra.Trade.CatchCertificates.Api.Mappers;
using DtosMmo = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using DtosOutboundEhco = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.Services
{
    public class EhcoMmoMappingService(
        IMapper mapper, IEhcoQuestionAnswerMapper ehcoMapper,
        ICatchCertificateCaseRepository catchCertRepo,
        IEhcoMmoQuestionMappingRepository questionMappingRepo) : IEhcoMmoMappingService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IEhcoQuestionAnswerMapper _ehcoMapper = ehcoMapper;
        private readonly ICatchCertificateCaseRepository _catchCertRepo = catchCertRepo;
        private readonly IEhcoMmoQuestionMappingRepository _questionMappingRepo = questionMappingRepo;

        public async Task<DtosOutboundEhco.EhcoMmoMappedResult> ProcessMapping(DtosOutboundEhco.EhcoMmoMappingRequest mappingRequest)
        {
            var questionMappings = await _questionMappingRepo.GetQuestionMappings();

            string documentNumber = mappingRequest.MmoDocumentNumbers.Single();

            var catchCertDataRow = await _catchCertRepo.GetByDocumentNumberAsync(documentNumber)
                ?? throw new MmoDocumentNotFoundException("MMO Catch Certificate could not be found.", documentNumber);

            var certificate = _mapper.Map<DtosMmo.CatchCertificateCase>(catchCertDataRow);

            bool isOrgMatched = IsOrganisationMatched(mappingRequest, certificate);

            var mappedAnswers =
                _ehcoMapper.Map(mappingRequest.ResponseItems, isOrgMatched, certificate, questionMappings);

            var result = new DtosOutboundEhco.EhcoMmoMappedResult
            {
                ResponseItems = mappedAnswers
            };

            return result;
        }

        private static bool IsOrganisationMatched(DtosOutboundEhco.EhcoMmoMappingRequest mappingRequest,
            DtosMmo.CatchCertificateCase catchCertificate)
        {
            return mappingRequest.OrganisationId.GetValueOrDefault() != Guid.Empty
                   && mappingRequest.OrganisationId.GetValueOrDefault().ToString()
                       .Equals(catchCertificate.Exporter?.AccountId, StringComparison.OrdinalIgnoreCase);
        }
    }
}