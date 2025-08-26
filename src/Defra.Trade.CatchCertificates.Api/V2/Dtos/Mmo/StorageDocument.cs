// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

/// <summary>
/// Schema for Storage Document data transfer to Defra Trade (CHIP).
/// </summary>
public class StorageDocument : MessageCore<V3.Dtos.Mmo.Exporter, V3.Dtos.Mmo.Country>
{
    /// <summary>
    /// The authority.
    /// </summary>
    public V3.Dtos.Mmo.Authority Authority { get; set; }

    /// <summary>
    /// The name of the company.
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// The exporter identifier.
    /// </summary>
    public string ExporterId { get; set; }

    /// <summary>
    /// The products.
    /// </summary>
    public IEnumerable<V3.Dtos.Mmo.Product> Products { get; set; }

    /// <summary>
    /// The storage facilities.
    /// </summary>
    public IEnumerable<V3.Dtos.Mmo.StorageFacility> StorageFacilities { get; set; }

    /// <summary>
    /// The transportation.
    /// </summary>
    public V3.Dtos.Mmo.Transportation Transportation { get; set; }

    /// <summary>
    /// The version.
    /// </summary>
    public int? Version { get; set; }
}
