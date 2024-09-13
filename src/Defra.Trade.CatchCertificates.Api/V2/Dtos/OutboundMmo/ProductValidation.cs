// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

public class ProductValidation
{
    /// <summary>
    /// The overuse information.
    /// </summary>
    public IEnumerable<string> OveruseInfo { get; set; }

    /// <summary>
    /// The status.
    /// </summary>
    /// <remarks>
    /// Validation Success, Validation Failure - Overuse, Validation Failure - Weight
    /// </remarks>
    public string Status { get; set; }

    /// <summary>
    /// The total weight exported.
    /// </summary>
    public double TotalWeightExported { get; set; }

    /// <summary>
    /// The weight exceeded amount.
    /// </summary>
    public double? WeightExceededAmount { get; set; }
}