// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoMapper;
using Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco;

namespace Defra.Trade.CatchCertificates.Api.Mappers
{
    public class OutboundEhcoProfile : Profile
    {
        public OutboundEhcoProfile()
        {
            CreateMap<ApplicationFormItem, ApplicationFormItem>();
        }
    }
}
