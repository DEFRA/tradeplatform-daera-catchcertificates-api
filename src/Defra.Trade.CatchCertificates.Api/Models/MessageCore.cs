// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Text.Json.Serialization;

namespace Defra.Trade.CatchCertificates.Api.Models;

public abstract class MessageCore<TExporter, TCountry> : IUpdateTracked
{
    /// <summary>
    ///
    /// </summary>
    /// <remarks>CC, PS, SD</remarks>
    public string CaseType1 { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <remarks>Real Time Validation - Rejected, Pending Landing Data, Retrospective - Success, Retrospective - Failure, Real Time Validation - Successful, Void by an Exporter</remarks>
    public string CaseType2 { get; set; }

    /// <summary>
    /// Unique ID to improve the end to end traceability of an event
    /// </summary>
    [JsonPropertyName("_correlationId")]
    public Guid? CorrelationId { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string DA { get; set; }

    /// <summary>
    /// The date of the catch certificate case
    /// </summary>
    public DateTimeOffset? DocumentDate { get; set; }

    /// <summary>
    /// The document number of the catch certificate case
    /// </summary>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// UK Authority QR Code used to view the document via a QR code
    /// </summary>
    public string DocumentUrl { get; set; }

    /// <summary>
    /// The country exported to
    /// </summary>
    public TCountry ExportedTo { get; set; }

    /// <summary>
    /// The exporter details
    /// </summary>
    public TExporter Exporter { get; set; }

    /// <summary>
    /// Date/time when the messaged was created/updated.
    /// </summary>
    /// <example>2020-11-20T12:22:30Z</example>
    public DateTimeOffset? LastUpdated { get; set; }

    /// <summary>
    /// User Id who caused the change.
    /// </summary>
    /// <example>2AE36373-4C00-45CE-BC7C-B80962BC8ED9</example>
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// System responsible for the change.
    /// </summary>
    /// <example>MMO</example>
    public string LastUpdatedSystem { get; set; }

    /// <summary>
    /// The total number of failed submission attempts executed by the user
    /// </summary>
    public int? NumberOfFailedSubmissions { get; set; }

    /// <summary>
    /// Flag to identify applications made by an EACC
    /// </summary>
    public bool? RequestedByAdmin { get; set; }
}