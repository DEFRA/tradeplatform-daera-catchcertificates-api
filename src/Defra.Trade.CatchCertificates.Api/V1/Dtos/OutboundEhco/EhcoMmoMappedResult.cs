// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco
{
    /// <summary>
    /// Result of the mapping of MMO data onto EHCO question and answers.
    /// </summary>
    public class EhcoMmoMappedResult
    {
        /// <summary>
        /// EHCO question and answers.
        /// </summary>
        public IEnumerable<ApplicationFormItem> ResponseItems { get; set; }
    }
}
