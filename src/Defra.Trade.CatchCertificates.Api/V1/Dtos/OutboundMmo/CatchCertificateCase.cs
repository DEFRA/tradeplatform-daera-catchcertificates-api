// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo
{
    public class CatchCertificateCase
    {
        /// <summary>
        /// The Devolved Authority, which is a mapping of the Exporters postcode to the authority to which it resides.
        /// </summary>
        /// <remarks>e.g. England, Scotland, Wales, Guernsey, Isle of Man, Jersey, Northern Ireland.</remarks>
        /// <example>Scotland</example>
        public string DA { get; set; }

        /// <summary>
        /// Status of the certificate / document.
        /// </summary>
        /// <remarks>DRAFT, COMPLETE, VOID</remarks>
        /// <example>COMPLETE</example>
        public string CertStatus { get; set; }

        /// <summary>
        /// Document date of issue.
        /// </summary>
        /// <example>2020-12-09T08:56:21.000</example>
        public DateTimeOffset? DocumentDate { get; set; }

        /// <summary>
        /// Document identifier / number.
        /// </summary>
        /// <example>GBR-2020-CC-8D02AD4D9</example>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// URL to access the PDF version of the generated catch certificate.
        /// </summary>
        /// <example>https://manage-fish-exports.service.gov.uk/url-to-certificate/file.pdf</example>
        public string DocumentUrl { get; set; }

        /// <summary>
        /// Details of the Exporter organisation.
        /// </summary>
        public Exporter Exporter { get; set; }

        /// <summary>
        /// Catch landing details.
        /// </summary>
        public IEnumerable<Landing> Landings { get; set; }

        /// <summary>
        /// Where is the catch exported to
        /// </summary>
        public Country ExportedTo { get; set; }
    }
}