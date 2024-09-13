// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo;

public class StorageDocument : MessageCore<Exporter, Country>
{
    public string CompanyName { get; set; }

    public string ExporterId { get; set; }

    public IEnumerable<Product> Products { get; set; }
}