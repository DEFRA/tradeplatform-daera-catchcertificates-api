// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class Authority
{
    /// <summary>
    /// The address.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// The name of the company.
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// The date issued.
    /// </summary>
    public string DateIssued { get; set; }

    /// <summary>
    /// The email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The tel.
    /// </summary>
    public string Tel { get; set; }
}