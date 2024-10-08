// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class Exporter
{
    public string ContactId { get; set; }

    public string AccountId { get; set; }

    public string FullName { get; set; }

    public string CompanyName { get; set; }

    public Address Address { get; set; }

    public DynamicsAddress DynamicsAddress { get; set; }
}