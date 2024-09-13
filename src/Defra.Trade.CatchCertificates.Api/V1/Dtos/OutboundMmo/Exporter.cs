// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo
{
    /// <summary>
    /// Exporter information.
    /// </summary>
    public class Exporter
    {
        /// <summary>
        /// Full name of the exporter's representative.
        /// </summary>
        /// <example>John Smith</example>
        public string FullName { get; set; }

        /// <summary>
        /// Exporting company name.
        /// </summary>
        /// <example>Abc Ltd</example>
        public string CompanyName { get; set; }

        /// <summary>
        /// Address of the exporting company.
        /// </summary>
        public Address Address { get; set; }
    }
}
