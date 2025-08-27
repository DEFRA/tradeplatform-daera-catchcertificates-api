// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;

public class CatchCertificateCase : CatchCertificateCaseBase
{

    /// <summary>
    /// The landings for this catch certificate case
    /// </summary>
    public IEnumerable<Landing> Landings { get; set; }

}
