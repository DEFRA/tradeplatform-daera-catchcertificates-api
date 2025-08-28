// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using V2External = Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;
using V2Internal = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using V3Internal = Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.Mappers;

public class OutboundMmoProfile : Profile
{
    public OutboundMmoProfile()
    {
        CreateMap<V2Internal.CatchCertificateCase, V2External.CatchCertificateCase>();
        CreateMap<V2Internal.ProcessingStatement, V2External.ProcessingStatement>();
        CreateMap<V2Internal.StorageDocument, V2External.StorageDocument>();

        CreateMap<V3Internal.Address, V2External.Address>();
        CreateMap<V3Internal.Audit, V2External.Audit>();
        CreateMap<V3Internal.Authority, V2External.Authority>();
        CreateMap<V3Internal.Catch, V2External.Catch>();
        CreateMap<V3Internal.CatchCertificateCase, V2External.CatchCertificateCase>();
        CreateMap<V3Internal.CatchValidation, V2External.CatchValidation>();
        CreateMap<V3Internal.Country, V2External.Country>();
        CreateMap<V3Internal.DynamicsAddress, V2External.DynamicsAddress>();
        CreateMap<V3Internal.Exporter, V2External.Exporter>();
        CreateMap<V3Internal.Landing, V2External.Landing>();
        CreateMap<V3Internal.LandingValidation, V2External.LandingValidation>();
        CreateMap<V3Internal.ProcessingStatement, V2External.ProcessingStatement>();
        CreateMap<V3Internal.Product, V2External.Product>();
        CreateMap<V3Internal.ProductValidation, V2External.ProductValidation>();
        CreateMap<V3Internal.Risk, V2External.Risk>();
        CreateMap<V3Internal.StorageDocument, V2External.StorageDocument>();
        CreateMap<V3Internal.StorageFacility, V2External.StorageFacility>();
        CreateMap<V3Internal.Transportation, V2External.Transportation>();
    }
}
