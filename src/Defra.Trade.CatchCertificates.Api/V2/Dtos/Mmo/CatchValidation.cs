// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class CatchValidation
{
    /// <summary>
    /// The overuse information.
    /// </summary>
    public List<string> OveruseInfo { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <remarks>Validation Success, Validation Failure - Overuse, Validation Failure - Weight</remarks>
    public string Status { get; set; }

    /// <summary>
    /// The total used weight against certificate.
    /// </summary>
    public double? TotalUsedWeightAgainstCertificate { get; set; }

    /// <summary>
    /// The weight exceeded amount.
    /// </summary>
    public double? WeightExceededAmount { get; set; }
}