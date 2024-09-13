// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;

public class ProcessingStatement : MessageCore<Exporter, Country>
{
    public IEnumerable<Catch> Catches { get; set; }

    public string ExporterId { get; set; }

    public string PersonResponsible { get; set; }

    public string PlantName { get; set; }

    public string ProcessedFisheryProducts { get; set; }
}