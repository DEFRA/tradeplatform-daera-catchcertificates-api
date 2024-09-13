// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

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