// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

/// <summary>
/// Product information.
/// </summary>
public class Product
{
    /// <summary>
    /// Combined Nomenclature Code for the product.
    /// </summary>
    /// <example>1604142800</example>
    public string CnCode { get; set; }

    /// <summary>
    /// The date of unloading.
    /// </summary>
    public string DateOfUnloading { get; set; }

    /// <summary>
    /// Weight in Kg for the exported product.
    /// </summary>
    /// <example>2500</example>
    public double ExportedWeight { get; set; }

    /// <summary>
    /// Certificate number for the catch where the catch was landed and certified in a non UK Port.
    /// </summary>
    /// <example>ABC1234</example>
    public string ForeignCatchCertificateNumber { get; set; }

    /// <summary>
    /// Unique identifier for the product.
    /// </summary>
    /// <example>GBR-2020-CC-4D780B2B3-8082999589</example>
    public string Id { get; set; }

    /// <summary>
    /// Weight in Kg of the imported product.
    /// </summary>
    /// <example>19000</example>
    public double ImportedWeight { get; set; }

    /// <summary>
    /// The place of unloading.
    /// </summary>
    public string PlaceOfUnloading { get; set; }

    /// <summary>
    /// Scientific name for the catch.
    /// </summary>
    /// <example>Conger conger</example>
    public string ScientificName { get; set; }

    /// <summary>
    /// Common species code for the catch.
    /// </summary>
    /// <example>SKJ</example>
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