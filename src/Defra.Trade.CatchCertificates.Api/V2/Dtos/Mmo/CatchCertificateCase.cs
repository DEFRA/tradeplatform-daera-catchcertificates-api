// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class CatchCertificateCase : MessageCore<Exporter, Country>
{
    /// <summary>
    /// List of audits against the catch certificate case
    /// </summary>
    public IEnumerable<Audit> Audits { get; set; }

    /// <summary>
    /// Certificate Status
    /// </summary>
    /// <remarks>BLOCKED, COMPLETE</remarks>
    public string CertStatus { get; set; }

    /// <summary>
    /// Flag to inform whether validation failure occurred in one of more landings
    /// </summary>
    public bool? FailureIrrespectiveOfRisk { get; set; }

    /// <summary>
    /// Indicates if the transport of the landings are via a fishing vessel
    /// </summary>
    public bool? IsDirectLanding { get; set; }

    /// <summary>
    /// Flag to identify when an application has been pre approved by an IUU officer
    /// </summary>
    public bool? IsUnblocked { get; set; }

    /// <summary>
    /// The landings for this catch certificate case
    /// </summary>
    public IEnumerable<Landing> Landings { get; set; }

    /// <summary>
    /// Indicates whether this submission has a multi vessel schedule
    /// </summary>
    public bool? MultiVesselSchedule { get; set; }

    /// <summary>
    /// Indicates that an incorrect species has been added to a landing on this certificate
    /// </summary>
    public bool? SpeciesOverriddenByAdmin { get; set; }

    /// <summary>
    /// The transportation details for this catch certificate case
    /// </summary>
    public Transportation Transportation { get; set; }

    /// <summary>
    /// Schema version
    /// </summary>
    /// <remarks>Constant value of 2</remarks>
    public int? Version { get; set; }

    /// <summary>
    /// Indicates that an unlicensed vessel has been added to a landing on this certificate
    /// </summary>
    public bool? VesselOverriddenByAdmin { get; set; }
}