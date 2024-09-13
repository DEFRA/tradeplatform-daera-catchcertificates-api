// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

/// <summary>
/// Landing information.
/// </summary>
public class Landing
{
    /// <summary>
    /// The commodity code changed by the admin
    /// </summary>
    public string AdminCommodityCode { get; set; }

    /// <summary>
    /// The presentation added from admin app
    /// </summary>
    public string AdminPresentation { get; set; }

    /// <summary>
    /// The species added from admin app
    /// </summary>
    public string AdminSpecies { get; set; }

    /// <summary>
    /// The state added from admin app
    /// </summary>
    public string AdminState { get; set; }

    /// <summary>
    /// The catch area fish was caught in
    /// </summary>
    /// <remarks>FAO18, FAO21, FAO27, FAO31, FAO34, FAO37, FAO41, FAO47, FAO48, FAO51, FAO57, FAO58, FAO61, FAO67, FAO71, FAO77, FAO81, FAO87, FAO88</remarks>
    public string CatchArea { get; set; }

    /// <summary>
    /// Combined Nomenclature Code.
    /// </summary>
    /// <example>0101</example>
    public string CnCode { get; set; }

    /// <summary>
    /// Combined Nomenclature Description.
    /// </summary>
    public string CommodityCodeDescription { get; set; }

    /// <summary>
    /// Is data ever expected for this landing?
    /// </summary>
    public bool? DataEverExpected { get; set; }

    /// <summary>
    /// The date at which landing data is received
    /// </summary>
    public string DataReceivedDate { get; set; }

    /// <summary>
    /// The licence for the vessel used in the landing
    /// </summary>
    public string FishingLicenceNumber { get; set; }

    /// <summary>
    /// The valid to of the licence for the vessel used in the landing
    /// </summary>
    public string FishingLicenceValidTo { get; set; }

    /// <summary>
    /// The flag of the vessel used within the landing
    /// </summary>
    public string Flag { get; set; }

    /// <summary>
    /// The home port of the vessel used within the landing
    /// </summary>
    public string HomePort { get; set; }

    /// <summary>
    /// Unique identifier for the landing.
    /// </summary>
    /// <example>GBR-2020-CC-8D02AD4D9-1607504140</example>
    public string Id { get; set; }

    /// <summary>
    /// The International Maritime Organization assigned to vessel on the landing
    /// </summary>
    public long? Imo { get; set; }

    /// <summary>
    /// Is the 14 day limit reached?
    /// </summary>
    /// <remarks>Default is false</remarks>
    public bool? Is14DayLimitReached { get; set; }

    /// <summary>
    /// Is Landing data received late?
    /// </summary>
    public bool? IsLate { get; set; }

    /// <summary>
    /// The date at which landing data is assumed unavailable for this landing
    /// </summary>
    public string LandingDataEndDate { get; set; }

    /// <summary>
    /// Is Landing data expected at submission?
    /// </summary>
    public bool? LandingDataExpectedAtSubmission { get; set; }

    /// <summary>
    /// The date that landing data is expected for this landing
    /// </summary>
    public string LandingDataExpectedDate { get; set; }

    /// <summary>
    /// Recorded date of the landing.
    /// </summary>
    /// <example>2020-11-02T00:00:00</example>
    public DateTimeOffset LandingDate { get; set; }

    /// <summary>
    /// Licence holder for the vessel.
    /// </summary>
    public string LicenceHolder { get; set; }

    /// <summary>
    /// Number of times a landing has been submitted for certification.
    /// </summary>
    /// <example>1</example>
    public int NumberOfTotalSubmissions { get; set; }

    /// <summary>
    /// Presentation of the catch.
    /// </summary>
    /// <remarks>e.g. WHL, GUT, GUH, GHT, HEA, TAL, WNG, FIL, FIS, CLA, ROE</remarks>
    /// <example>WHL</example>
    public string Presentation { get; set; }

    /// <summary>
    /// The landing risk
    /// </summary>
    public Risk Risking { get; set; }

    /// <summary>
    /// Scientific name.
    /// </summary>
    /// <example>Conger conger</example>
    public string ScientificName { get; set; }

    /// <summary>
    /// Source of the landing declaration data.
    /// </summary>
    /// <remarks>LANDING_DECLARATION, CATCH_RECORDING, ELOG</remarks>
    /// <example>CATCH_RECORDING</example>
    public string Source { get; set; }

    /// <summary>
    /// Common species code.
    /// </summary>
    /// <example>MAC</example>
    public string Species { get; set; }

    /// <summary>
    /// The status (Y/N) of certain species mismatching during real-time validation and retrospective validation
    /// </summary>
    public string SpeciesAlias { get; set; }

    /// <summary>
    /// The species code when species mismatching occurs during real-time validation and retrospective validation
    /// </summary>
    public string SpeciesAnomaly { get; set; }

    /// <summary>
    /// Are the species details overridden by the admin?
    /// </summary>
    public bool? SpeciesOverriddenByAdmin { get; set; }

    /// <summary>
    /// State of the catch.
    /// </summary>
    /// <remarks>e.g. ALI, BOI, DRI, FRE, FRO, SAL</remarks>
    /// <example>FRE</example>
    public string State { get; set; }

    /// <summary>
    /// Status of the landing.
    /// </summary>
    /// <remarks>Validation Success, Validation Failure - Overuse, Validation Failure - Weight, Validation Failure - Species, Validation Failure - No Landing Data, Pending Landing Data</remarks>
    /// <example>Validation Success</example>
    public string Status { get; set; }

    /// <summary>
    /// Validation detail for the landing.
    /// </summary>
    public LandingValidation Validation { get; set; }

    /// <summary>
    /// The administration of the vessel
    /// </summary>
    public string VesselAdministration { get; set; }

    /// <summary>
    /// Overall length of the vessel landing the catch in metres.
    /// </summary>
    /// <example>5.83</example>
    public double VesselLength { get; set; }

    /// <summary>
    /// Name of the vessel that landed the catch.
    /// </summary>
    /// <example>MARIGOLD</example>
    public string VesselName { get; set; }

    /// <summary>
    /// Are the vessel details overridden by the admin?
    /// </summary>
    /// <remarks>Default is false</remarks>
    public bool? VesselOverriddenByAdmin { get; set; }

    /// <summary>
    /// Port Letter and Number of the vessel landing the catch.
    /// </summary>
    /// <example>WK34</example>
    public string VesselPln { get; set; }

    /// <summary>
    /// Was validation due at the point of application
    /// </summary>
    public bool? WasValidationDueAtPointOfApplication { get; set; }

    /// <summary>
    /// Certified export weight in Kg of catch.
    /// </summary>
    /// <example>1000</example>
    public double Weight { get; set; }
}