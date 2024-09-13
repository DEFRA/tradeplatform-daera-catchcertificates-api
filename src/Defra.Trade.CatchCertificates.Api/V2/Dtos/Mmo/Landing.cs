// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Text.Json.Serialization;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class Landing
{
    public string AdminCommodityCode { get; set; }

    public string AdminPresentation { get; set; }

    public string AdminSpecies { get; set; }

    public string AdminState { get; set; }

    public string CatchArea { get; set; }

    public string CnCode { get; set; }

    public string CommodityCodeDescription { get; set; }

    public bool? DataEverExpected { get; set; }

    [JsonPropertyName("dateDataReceived")]
    public string DataReceivedDate { get; set; }

    public string FishingLicenceNumber { get; set; }

    public string FishingLicenceValidTo { get; set; }

    public string Flag { get; set; }

    public string HomePort { get; set; }

    public string Id { get; set; }

    public long? Imo { get; set; }

    public bool? Is14DayLimitReached { get; set; }

    public bool? IsLate { get; set; }

    public string LandingDataEndDate { get; set; }

    public bool? LandingDataExpectedAtSubmission { get; set; }

    public string LandingDataExpectedDate { get; set; }

    public DateTimeOffset? LandingDate { get; set; }

    public string LicenceHolder { get; set; }

    public int? NumberOfTotalSubmissions { get; set; }

    public string Presentation { get; set; }

    public Risk Risking { get; set; }

    public string ScientificName { get; set; }

    public string Source { get; set; }

    public string Species { get; set; }

    public string SpeciesAlias { get; set; }

    public string SpeciesAnomaly { get; set; }

    public bool? SpeciesOverriddenByAdmin { get; set; }

    public string State { get; set; }

    public string Status { get; set; }

    public LandingValidation Validation { get; set; }

    public string VesselAdministration { get; set; }

    public double? VesselLength { get; set; }

    public string VesselName { get; set; }

    public bool? VesselOverriddenByAdmin { get; set; }

    public string VesselPln { get; set; }

    public bool? WasValidationDueAtPointOfApplication { get; set; }

    public double? Weight { get; set; }
}