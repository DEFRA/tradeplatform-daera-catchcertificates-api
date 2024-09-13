// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

public class CatchCertificateCase
{
    /// <summary>
    /// List of audits against the catch certificate case
    /// </summary>
    public IEnumerable<Audit> Audits { get; set; }

    /// <summary>
    /// Case Type 1
    /// </summary>
    /// <remarks>CC, PS, SD</remarks>
    /// <example>CC</example>
    public string CaseType1 { get; set; }

    /// <summary>
    /// Case Type 2
    /// </summary>
    /// <remarks>Real Time Validation - Rejected, Pending Landing Data, Retrospective - Success, Retrospective - Failure, Real Time Validation - Successful, Void by an Exporter, Void by SMO/PMO</remarks>
    /// <example>Real Time Validation - Rejected</example>
    public string CaseType2 { get; set; }

    /// <summary>
    /// Status of the certificate / document.
    /// </summary>
    /// <remarks>DRAFT, COMPLETE, VOID</remarks>
    /// <example>COMPLETE</example>
    public string CertStatus { get; set; }

    /// <summary>
    /// Unique ID to improve the end to end traceability of an event
    /// </summary>
    /// <example>37BD4881-FC5D-430E-8FFE-99F5E6D8CA7A</example>
    public Guid? CorrelationId { get; set; }

    /// <summary>
    /// The Devolved Authority, which is a mapping of the Exporters postcode to the authority to which it resides.
    /// </summary>
    /// <remarks>e.g. England, Scotland, Wales, Guernsey, Isle of Man, Jersey, Northern Ireland.</remarks>
    /// <example>Scotland</example>
    public string DA { get; set; }

    /// <summary>
    /// Document date of issue.
    /// </summary>
    /// <example>2020-12-09T08:56:21.000</example>
    public DateTimeOffset? DocumentDate { get; set; }

    /// <summary>
    /// Document identifier / number.
    /// </summary>
    /// <example>GBR-2020-CC-8D02AD4D9</example>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// URL to access the PDF version of the generated catch certificate.
    /// </summary>
    /// <example>https://manage-fish-exports.service.gov.uk/url-to-certificate/file.pdf</example>
    public string DocumentUrl { get; set; }

    /// <summary>
    /// Where is the catch exported to
    /// </summary>
    public Country ExportedTo { get; set; }

    /// <summary>
    /// Details of the Exporter organisation.
    /// </summary>
    public Exporter Exporter { get; set; }

    /// <summary>
    /// Flag to inform whether validation failure occurred in one of more landings
    /// </summary>
    /// <example>true</example>
    public bool? FailureIrrespectiveOfRisk { get; set; }

    /// <summary>
    /// Indicates if the transport of the landings are via a fishing vessel
    /// </summary>
    /// <example>true</example>
    public bool? IsDirectLanding { get; set; }

    /// <summary>
    /// Flag to identify when an application has been pre approved by an IUU officer
    /// </summary>
    /// <example>true</example>
    public bool? IsUnblocked { get; set; }

    /// <summary>
    /// Catch landing details.
    /// </summary>
    public IEnumerable<Landing> Landings { get; set; }

    /// <summary>
    /// Indicates whether this submission has a multi vessel schedule
    /// </summary>
    /// <example>true</example>
    public bool? MultiVesselSchedule { get; set; }

    /// <summary>
    /// The total number of failed submission attempts executed by the user
    /// </summary>
    /// <example>10</example>
    public int? NumberOfFailedSubmissions { get; set; }

    /// <summary>
    /// Flag to identify applications made by an EACC
    /// </summary>
    /// <example>true</example>
    public bool? RequestedByAdmin { get; set; }

    /// <summary>
    /// Indicates that an incorrect species has been added to a landing on this certificate
    /// </summary>
    /// <example>true</example>
    public bool? SpeciesOverriddenByAdmin { get; set; }

    /// <summary>
    /// The transportation details for this catch certificate case
    /// </summary>
    public Transportation Transportation { get; set; }

    /// <summary>
    /// Schema version
    /// </summary>
    /// <remarks>1 or 2</remarks>
    /// <example>1</example>
    public int? Version { get; set; }

    /// <summary>
    /// Indicates that an unlicensed vessel has been added to a landing on this certificate
    /// </summary>
    /// <example>true</example>
    public bool? VesselOverriddenByAdmin { get; set; }
}