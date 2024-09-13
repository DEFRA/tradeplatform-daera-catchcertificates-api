// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    public class Audit
    {
        /// <remarks>INVESTIGATED, VOIDED, PREAPPROVED</remarks>
        public string AuditOperation { get; set; }

        public string User { get; set; }

        public string AuditAt { get; set; }

        /// <remarks>DATA_ERROR_NFA, MINOR_VERBAL, MINOR_WRITTEN, UNDER_INVESTIGATION, OPEN_UNDER_ENQUIRY, CLOSED_NFA, USER_EDUCATION_PROVIDED</remarks>
        public string InvestigationStatus { get; set; }
    }
}