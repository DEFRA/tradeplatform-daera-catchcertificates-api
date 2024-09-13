// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class Audit
{
    /// <summary>
    /// The audit operation being undertaken
    /// </summary>
    /// <remarks>INVESTIGATED, VOIDED, PREAPPROVED</remarks>
    public string AuditOperation { get; set; }

    /// <summary>
    /// The status of the investigation
    /// </summary>
    /// <remarks>DATA_ERROR_NFA, MINOR_VERBAL, MINOR_WRITTEN, UNDER_INVESTIGATION, OPEN_UNDER_ENQUIRY, CLOSED_NFA, USER_EDUCATION_PROVIDED</remarks>
    public string InvestigationStatus { get; set; }

    /// <summary>
    /// The user details
    /// </summary>
    public string User { get; set; }

    /// <summary>
    /// When and/or where the audit is being conducted
    /// </summary>
    public string AuditAt { get; set; }


}