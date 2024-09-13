// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

/// <summary>
/// Schema for Storage Document data transfer to Defra Trade (CHIP).
/// </summary>
public class StorageDocument : MessageCore<Exporter, Country>
{
    /// <summary>
    /// The authority.
    /// </summary>
    public Authority Authority { get; set; }

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
    public IEnumerable<Product> Products { get; set; }

    /// <summary>
    /// The storage facilities.
    /// </summary>
    public IEnumerable<StorageFacility> StorageFacilities { get; set; }

    /// <summary>
    /// The transportation.
    /// </summary>
    public Transportation Transportation { get; set; }

    /// <summary>
    /// The version.
    /// </summary>
    public int? Version { get; set; }
}