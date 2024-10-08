// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class Catch
{
    /// <summary>
    /// The cn code.
    /// </summary>
    public string CnCode { get; set; }

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
    /// The processed weight.
    /// </summary>
    public double? ProcessedWeight { get; set; }

    /// <summary>
    /// The name of the scientific.
    /// </summary>
    public string ScientificName { get; set; }

    /// <summary>
    /// The species.
    /// </summary>
    public string Species { get; set; }

    /// <summary>
    /// The used weight against certificate.
    /// </summary>
    public double? UsedWeightAgainstCertificate { get; set; }

    /// <summary>
    /// The validation.
    /// </summary>
    public CatchValidation Validation { get; set; }
}