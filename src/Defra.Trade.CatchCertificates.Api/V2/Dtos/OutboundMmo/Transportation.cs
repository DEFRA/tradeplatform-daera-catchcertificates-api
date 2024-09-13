// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.CatchCertificates.Api.V2.Dtos.Enums;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

/// <summary>
/// Details of Transport (Vessel / flight number / railway bill / truck registration)
/// </summary>
public class Transportation
{
    /// <summary>
    /// The bill number
    /// </summary>
    public string BillNumber { get; set; }

    /// <summary>
    /// The container id
    /// </summary>
    public string ContainerId { get; set; }

    /// <summary>
    /// Export date
    /// </summary>
    public string ExportDate { get; set; }

    /// <summary>
    /// The export location
    /// </summary>
    public string ExportLocation { get; set; }

    /// <summary>
    /// The flag of the transportation
    /// </summary>
    public string Flag { get; set; }

    /// <summary>
    /// The flight number
    /// </summary>
    public string FlightNumber { get; set; }

    /// <summary>
    /// Has road transport document
    /// </summary>
    public bool? HasRoadTransportDocument { get; set; }

    /// <summary>
    /// The mode of transport
    /// </summary>
    /// <remarks>truck, train, plane, vessel</remarks>
    public ModeOfTransport ModeOfTransport { get; set; }

    /// <summary>
    /// Name of the transportation
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Nationality of the transport
    /// </summary>
    public string Nationality { get; set; }

    /// <summary>
    /// The registration of the transport
    /// </summary>
    public string Registration { get; set; }
}