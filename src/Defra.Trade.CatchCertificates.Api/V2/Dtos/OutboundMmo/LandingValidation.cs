// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

/// <summary>
/// Landing validation information.
/// </summary>
public class LandingValidation
{
    /// <summary>
    /// Is legally due flag
    /// </summary>
    public bool? IsLegallyDue { get; set; }

    /// <summary>
    /// Difference between estimated with tolerance and the recorded weight in Kg.
    /// </summary>
    /// <example></example>
    public double? LandedWeightExceededBy { get; set; }

    /// <summary>
    /// Weight in Kg for the live exported catch.
    /// </summary>
    /// <example>1000</example>
    public double LiveExportWeight { get; set; }

    /// <summary>
    /// The url for the raw landing document
    /// </summary>
    public string RawLandingsUrl { get; set; }

    /// <summary>
    /// The url for the sales note
    /// </summary>
    public string SalesNoteUrl { get; set; }

    /// <summary>
    /// Total estimated weight in Kg for export species.
    /// </summary>
    /// <example>1000</example>
    public double? TotalEstimatedForExportSpecies { get; set; }

    /// <summary>
    /// Total estimated weight with tolerance in Kg.
    /// </summary>
    /// <example>1100</example>
    public double? TotalEstimatedWithTolerance { get; set; }

    /// <summary>
    /// Total weight in Kg of live species being exported.
    /// </summary>
    /// <example></example>
    public double? TotalLiveForExportSpecies { get; set; }

    /// <summary>
    /// Total recorded weight in Kg.
    /// </summary>
    /// <example>1000</example>
    public double? TotalRecordedAgainstLanding { get; set; }

    /// <summary>
    /// Total weight in Kg for species.
    /// </summary>
    /// <example></example>
    public double? TotalWeightForSpecies { get; set; }
}