// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

/// <summary>
/// Exporter information.
/// </summary>
public class Exporter
{
    /// <summary>
    /// The unique id of the organisation/account
    /// </summary>
    /// <example>37BD4881-FC5D-430E-8FFE-99F5E6D8CA7A</example>
    public string AccountId { get; set; }

    /// <summary>
    /// Address of the exporting company.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// Exporting company name.
    /// </summary>
    /// <example>Abc Ltd</example>
    public string CompanyName { get; set; }

    /// <summary>
    /// The unique id of the contact
    /// </summary>
    /// <example>37BD4881-FC5D-430E-8FFE-99F5E6D8CA7A</example>
    public string ContactId { get; set; }

    /// <summary>
    /// Additional address information from dynamics
    /// </summary>
    public DynamicsAddress DynamicsAddress { get; set; }

    /// <summary>
    /// Full name of the exporter's representative.
    /// </summary>
    /// <example>John Smith</example>
    public string FullName { get; set; }
}