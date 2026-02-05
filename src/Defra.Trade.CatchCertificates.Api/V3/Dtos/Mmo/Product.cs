// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;

public class Product
{
    /// <summary>
    /// The cn code.
    /// </summary>
    public string CnCode { get; set; }

    /// <summary>
    /// The date of unloading.
    /// </summary>
    public string DateOfUnloading { get; set; }

    /// <summary>
    /// The exported weight.
    /// </summary>
    public double? ExportedWeight { get; set; }

    /// <summary>
    /// The foreign catch certificate number.
    /// </summary>
    public string ForeignCatchCertificateNumber { get; set; }

    /// <summary>
    /// The identifier.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The imported weight.
    /// </summary>
    public double? ImportedWeight { get; set; }
    /// <summary>
    /// Name of the country issuing the catch certificate
    /// </summary>
    public string IssuingCountry { get; set; }

    /// <summary>
    /// net weight of product on arrival provided on /add-product-to-this-consignments
    /// </summary>
    public string NetWeightProductArrival { get; set; }

    /// <summary>
    /// net weight of fishery products on arrival provided on /add-product-to-this-consignments
    /// </summary>

    public string NetWeightFisheryProductArrival { get; set; }

    /// <summary>
    /// net weight of product on departure provided on /departure-product-summary
    /// </summary>
    public string NetWeightProductDeparture { get; set; }

    /// <summary>
    /// net weight of fishery products on departure provided on /departure-product-summary
    /// </summary>
    public string NetWeightFisheryProductDeparture { get; set; }

    /// <summary>
    /// The place of unloading.
    /// </summary>
    public string PlaceOfUnloading { get; set; }

    /// <summary>
    /// The name of the scientific.
    /// </summary>
    public string ScientificName { get; set; }

    /// <summary>
    /// The species.
    /// </summary>
    public string Species { get; set; }

    /// <summary>
    /// The transport unloaded from.
    /// </summary>
    public string TransportUnloadedFrom { get; set; }

    /// <summary>
    /// The validation.
    /// </summary>
    public ProductValidation Validation { get; set; }
}
