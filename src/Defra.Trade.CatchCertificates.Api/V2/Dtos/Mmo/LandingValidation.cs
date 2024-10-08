// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class LandingValidation
{
    /// <summary>
    /// Is legally due flag
    /// </summary>
    public bool? IsLegallyDue { get; set; }

    /// <summary>
    /// The landed weight exceeded by value
    /// </summary>
    public double? LandedWeightExceededBy { get; set; }

    /// <summary>
    /// The live export weight
    /// </summary>
    public double? LiveExportWeight { get; set; }

    /// <summary>
    /// The url for the raw landing document
    /// </summary>
    public string RawLandingsUrl { get; set; }

    /// <summary>
    /// The url for the sales note
    /// </summary>
    public string SalesNoteUrl { get; set; }

    /// <summary>
    /// The total estimated weight for exported species
    /// </summary>
    public double? TotalEstimatedForExportSpecies { get; set; }

    /// <summary>
    /// The total estimated weight plus the tolerance for exported species
    /// </summary>
    public double? TotalEstimatedWithTolerance { get; set; }

    /// <summary>
    /// The total live weight for the exported species
    /// </summary>
    public double? TotalLiveForExportSpecies { get; set; }

    /// <summary>
    /// The total weight recorded against the landing
    /// </summary>
    public double? TotalRecordedAgainstLanding { get; set; }

    /// <summary>
    /// The total landing declared weight for exported species
    /// </summary>
    public double? TotalWeightForSpecies { get; set; }
}