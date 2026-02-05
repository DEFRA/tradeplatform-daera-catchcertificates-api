// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;

/// <summary>
/// Storage facility details
/// </summary>
public class StorageFacility
{
    public Address Address { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// Consignments Arrival Date provided from /add-storage-facility-details
    /// </summary>
    public string DateOfUnloading { get; set; }

    /// <summary>
    /// Storage facility approval number provided from add-storage-facility-approval
    /// </summary>
    public string ApprovalNumber { get; set; }

    /// <summary>
    /// Product Handling provided from add-storage-facility-approval
    /// </summary>
    public string productHandling { get; set; }
}
