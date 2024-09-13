// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo
{
    public class StorageDocument
    {
        /// <summary>
        /// The Devolved Authority, which is a mapping of the Exporters postcode to the authority to which it resides.
        /// </summary>
        /// <remarks>e.g. England, Scotland, Wales, Guernsey, Isle of Man, Jersey, Northern Ireland.</remarks>
        /// <example>Scotland</example>
        public string DA { get; set; }

        /// <summary>
        /// Document number.
        /// </summary>
        /// <example>GBR-2020-SD-EC3EB8941</example>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// URL to access the PDF version of the generated storage document.
        /// </summary>
        /// <example>https://manage-fish-exports.service.gov.uk/url-to-certificate/file.pdf</example>
        public string DocumentUrl { get; set; }

        /// <summary>
        /// Document date of issue.
        /// </summary>
        /// <example>2020-12-01T15:24:54.000Z</example>
        public DateTimeOffset? DocumentDate { get; set; }

        /// <summary>
        /// Details of the Exporter organisation.
        /// </summary>
        public Exporter Exporter { get; set; }

        /// <summary>
        /// Company name.
        /// </summary>
        /// <example>Abc Ltd</example>
        public string CompanyName { get; set; }

        /// <summary>
        /// Product details.
        /// </summary>
        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// Where is the catch exported to
        /// </summary>
        public Country ExportedTo { get; set; }
    }
}