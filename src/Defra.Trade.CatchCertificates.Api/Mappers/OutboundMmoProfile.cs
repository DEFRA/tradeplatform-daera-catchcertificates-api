// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using V2External = Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;
using V2Internal = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using V3External = Defra.Trade.CatchCertificates.Api.V3.Dtos.OutboundMmo;
using V3Internal = Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.Mappers;

public class OutboundMmoProfile : Profile
{
    public OutboundMmoProfile()
    {
        CreateMap<V2Internal.Address, V2External.Address>();
        CreateMap<V2Internal.Audit, V2External.Audit>();
        CreateMap<V2Internal.Authority, V2External.Authority>();
        CreateMap<V2Internal.Catch, V2External.Catch>();
        CreateMap<V2Internal.CatchCertificateCase, V2External.CatchCertificateCase>();
        CreateMap<V2Internal.CatchValidation, V2External.CatchValidation>();
        CreateMap<V2Internal.Country, V2External.Country>();
        CreateMap<V2Internal.DynamicsAddress, V2External.DynamicsAddress>();
        CreateMap<V2Internal.Exporter, V2External.Exporter>();
        CreateMap<V2Internal.Landing, V2External.Landing>();
        CreateMap<V2Internal.LandingValidation, V2External.LandingValidation>();
        CreateMap<V2Internal.ProcessingStatement, V2External.ProcessingStatement>();
        CreateMap<V2Internal.Product, V2External.Product>();
        CreateMap<V2Internal.ProductValidation, V2External.ProductValidation>();
        CreateMap<V2Internal.Risk, V2External.Risk>();
        CreateMap<V2Internal.StorageDocument, V2External.StorageDocument>();
        CreateMap<V2Internal.StorageFacility, V2External.StorageFacility>();
        CreateMap<V2Internal.Transportation, V2External.Transportation>();

        CreateMap<V3Internal.Address, V3External.Address>();
        CreateMap<V3Internal.Audit, V3External.Audit>();
        CreateMap<V3Internal.Authority, V3External.Authority>();
        CreateMap<V3Internal.Catch, V3External.Catch>();
        CreateMap<V3Internal.CatchCertificateCase, V3External.CatchCertificateCase>();
        CreateMap<V3Internal.CatchValidation, V3External.CatchValidation>();
        CreateMap<V3Internal.Country, V3External.Country>();
        CreateMap<V3Internal.DynamicsAddress, V3External.DynamicsAddress>();
        CreateMap<V3Internal.Exporter, V3External.Exporter>();
        CreateMap<V3Internal.Landing, V3External.Landing>();
        CreateMap<V3Internal.LandingValidation, V3External.LandingValidation>();
        CreateMap<V3Internal.ProcessingStatement, V3External.ProcessingStatement>();
        CreateMap<V3Internal.Product, V3External.Product>();
        CreateMap<V3Internal.ProductValidation, V3External.ProductValidation>();
        CreateMap<V3Internal.Risk, V3External.Risk>();
        CreateMap<V3Internal.StorageDocument, V3External.StorageDocument>();
        CreateMap<V3Internal.StorageFacility, V3External.StorageFacility>();
        CreateMap<V3Internal.Transportation, V3External.Transportation>();
    }
}
