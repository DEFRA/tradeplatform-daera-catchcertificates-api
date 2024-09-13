// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

public class CatchValidation
{
    /// <summary>
    /// The overuse information.
    /// </summary>
    public List<string> OveruseInfo { get; set; }

    /// <summary>
    /// The status of the validation
    /// </summary>
    /// <remarks>Validation Success, Validation Failure - Overuse, Validation Failure - Weight</remarks>
    /// <example>Validation Success</example>
    public string Status { get; set; }

    /// <summary>
    /// The total used weight against certificate in KG
    /// </summary>
    /// <example>10</example>
    public double TotalUsedWeightAgainstCertificate { get; set; }

    /// <summary>
    /// The weight exceeded amount in KG
    /// </summary>
    /// <example>10</example>
    public double? WeightExceededAmount { get; set; }
}