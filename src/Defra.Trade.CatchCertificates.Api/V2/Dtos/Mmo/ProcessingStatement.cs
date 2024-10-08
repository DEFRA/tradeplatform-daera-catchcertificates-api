// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class ProcessingStatement : MessageCore<Exporter, Country>
{
    /// <summary>
    /// The authority.
    /// </summary>
    public Authority Authority { get; set; }

    /// <summary>
    /// The catches.
    /// </summary>
    public IEnumerable<Catch> Catches { get; set; }

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
    /// The person responsible.
    /// </summary>
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
    /// The name of the plant.
    /// </summary>
    public string PlantName { get; set; }

    /// <summary>
    /// The processed fishery products.
    /// </summary>
    public string ProcessedFisheryProducts { get; set; }

    /// <summary>
    /// The version.
    /// </summary>
    public int? Version { get; set; }
}