// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

public class ProcessingStatement
{
    /// <summary>
    /// The authority.
    /// </summary>
    public Authority Authority { get; set; }

    /// <summary>
    /// The case type1.
    /// </summary>
    /// <remarks>
    /// CC, PS, SD
    /// </remarks>
    public string CaseType1 { get; set; }

    /// <summary>
    /// The case type2.
    /// </summary>
    /// <remarks>
    /// Real Time Validation - Success, Real Time Validation - Overuse Failure, Real Time Validation - Weight Failure, Void by an Exporter
    /// </remarks>
    public string CaseType2 { get; set; }

    /// <summary>
    /// Catch details in the processing statement.
    /// </summary>
    public IEnumerable<Catch> Catches { get; set; }

    /// <summary>
    /// Unique ID to improve the end to end traceability of an event
    /// </summary>
    public Guid CorrelationId { get; set; }

    /// <summary>
    /// The Devolved Authority, which is a mapping of the Exporters postcode to the authority to which it resides..
    /// </summary>
    /// <remarks>e.g. England, Scotland, Wales, Guernsey, Isle of Man, Jersey, Northern Ireland.</remarks>
    /// <example>Scotland</example>
    public string DA { get; set; }

    /// <summary>
    /// Document date of issue.
    /// </summary>
    /// <example>2020-12-01T16:45:11.000Z</example>
    public DateTimeOffset? DocumentDate { get; set; }

    /// <summary>
    /// Document identifier / number.
    /// </summary>
    /// <example>GBR-2020-PS-A30A11A52</example>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// URL to access the PDF version of the generated processing statement.
    /// </summary>
    /// <example>https://manage-fish-exports.service.gov.uk/url-to-certificate/file.pdf</example>
    public string DocumentUrl { get; set; }

    /// <summary>
    /// Where is the catch exported to.
    /// </summary>
    public Country ExportedTo { get; set; }

    /// <summary>
    /// Details of the Exporter organisation.
    /// </summary>
    public Exporter Exporter { get; set; }

    /// <summary>
    /// The exporter identifier.
    /// </summary>
    public string ExporterId { get; set; }

    /// <summary>
    /// The health certificate date.
    /// </summary>
    public string HealthCertificateDate { get; set; }

    /// <summary>
    /// The health certificate number.
    /// </summary>
    public string HealthCertificateNumber { get; set; }

    /// <summary>
    /// The total number of failed submission attempts executed by the user
    /// </summary>
    public int NumberOfFailedSubmissions { get; set; }

    /// <summary>
    /// Name of the inspector certifying the processing of the catch.
    /// </summary>
    /// <example>Jane Smith</example>
    public string PersonResponsible { get; set; }

    /// <summary>
    /// The plant address.
    /// </summary>
    public Address PlantAddress { get; set; }

    /// <summary>
    /// The plant approval number.
    /// </summary>
    public string PlantApprovalNumber { get; set; }

    /// <summary>
    /// The plant date of acceptance.
    /// </summary>
    public string PlantDateOfAcceptance { get; set; }

    /// <summary>
    /// Name of the Processing Plant.
    /// </summary>
    /// <example>MKRL Fish</example>
    public string PlantName { get; set; }

    /// <summary>
    /// User provided description of the commodities within the consignment.
    /// </summary>
    /// <example>Herring fillets (16041210) and Atlantic cod fishcakes (16041992)</example>
    public string ProcessedFisheryProducts { get; set; }

    /// <summary>
    /// Indicates if this was requested by an admin
    /// </summary>
    public bool RequestedByAdmin { get; set; }

    /// <summary>
    /// The version.
    /// </summary>
    public int? Version { get; set; }
}