// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.Models;

public abstract class DataRow
{
    public string Data { get; set; }

    public int SchemaVersion { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public string CreatedSystem { get; set; }

    public DateTimeOffset? LastUpdated { get; set; }

    public string LastUpdatedBy { get; set; }

    public string LastUpdatedSystem { get; set; }

    public string DocumentNumber { get; set; }
    public long Id { get; set; }
}