// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundEhco
{
    /// <summary>
    /// Request for a mapping from MMO data onto EHCO question and answers.
    /// </summary>
    public class EhcoMmoMappingRequest
    {
        /// <summary>
        /// Organisation Id of the user performing the mapping.
        /// </summary>
        public Guid? OrganisationId { get; set; }

        /// <summary>
        /// MMO document numbers to use as the data source for the mapping.
        /// </summary>
        public IEnumerable<string> MmoDocumentNumbers { get; set; }

        /// <summary>
        /// EHCO question and answers onto which to save the mapped answers.
        /// </summary>
        public IEnumerable<ApplicationFormItem> ResponseItems { get; set; }
    }
}
