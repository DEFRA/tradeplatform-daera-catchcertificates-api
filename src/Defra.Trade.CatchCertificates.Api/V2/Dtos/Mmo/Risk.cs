// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class Risk
{
    /// <summary>
    /// The vessel name
    /// </summary>
    public string Vessel { get; set; }

    /// <summary>
    /// The species risk
    /// </summary>
    public string SpeciesRisk { get; set; }

    /// <summary>
    /// The exporter's risk score
    /// </summary>
    public string ExporterRiskScore { get; set; }

    /// <summary>
    /// The landing risk score
    /// </summary>
    public string LandingRiskScore { get; set; }

    /// <summary>
    /// The high or low risk definition
    /// </summary>
    /// <remarks>High, Low</remarks>
    public string HighOrLowRisk { get; set; }

    /// <summary>
    /// List of overuse information
    /// </summary>
    public IEnumerable<string> OveruseInfo { get; set; }

    /// <summary>
    /// Is the species risk enabled?
    /// </summary>
    public bool? IsSpeciesRiskEnabled { get; set; }
}