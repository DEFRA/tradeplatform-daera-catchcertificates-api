// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using AutoMapper;
using V1External = Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo;
using V1Internal = Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;
using V2External = Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;
using V2Internal = Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

namespace Defra.Trade.CatchCertificates.Api.Mappers;

public class OutboundMmoProfile : Profile
{
    public OutboundMmoProfile()
    {
        CreateMap<V1Internal.Address, V1External.Address>();
        CreateMap<V1Internal.Catch, V1External.Catch>();
        CreateMap<V1Internal.CatchCertificateCase, V1External.CatchCertificateCase>();
        CreateMap<V1Internal.Country, V1External.Country>();
        CreateMap<V1Internal.Exporter, V1External.Exporter>();
        CreateMap<V1Internal.Landing, V1External.Landing>();
        CreateMap<V1Internal.LandingValidation, V1External.LandingValidation>();
        CreateMap<V1Internal.ProcessingStatement, V1External.ProcessingStatement>();
        CreateMap<V1Internal.Product, V1External.Product>();
        CreateMap<V1Internal.StorageDocument, V1External.StorageDocument>();

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
    }
}