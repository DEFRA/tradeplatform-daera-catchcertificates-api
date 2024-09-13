// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;

public class CatchCertificateCase : MessageCore<Exporter, Country>
{
    public IEnumerable<Audit> Audits { get; set; }

    /// <remarks>DRAFT, COMPLETE, VOID</remarks>
    public string CertStatus { get; set; }

    public bool? IsDirectLanding { get; set; }

    public bool? IsUnblocked { get; set; }

    public IEnumerable<Landing> Landings { get; set; }
}