// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo
{
    /// <summary>
    /// Catch information.
    /// </summary>
    public class Catch
    {
        /// <summary>
        /// Certificate number for the catch where the catch was landed and certified in a non-UK Port.
        /// </summary>
        /// <example>ABC1234</example>
        public string ForeignCatchCertificateNumber { get; set; }

        /// <summary>
        /// Unique identifier for the catch.
        /// </summary>
        /// <example>GBR-2020-CC-1A799A2C3-8373773293</example>
        public string Id { get; set; }

        /// <summary>
        /// Common species code for the catch.
        /// </summary>
        /// <example>ACL</example>
        public string Species { get; set; }

        /// <summary>
        /// Scientific name for the catch.
        /// </summary>
        /// <example>Conger conger</example>

        public string ScientificName { get; set; }

        /// <summary>
        /// Combined Nomenclature Code for the product.
        /// </summary>
        /// <example></example>
        public string CnCode { get; set; }

        /// <summary>
        /// Weight in Kg of the imported catch.
        /// </summary>
        /// <example>890</example>
        public double ImportedWeight { get; set; }

        /// <summary>
        /// Total amount of the imported weight in Kg used before processing.
        /// </summary>
        /// <example>800</example>
        public double UsedWeightAgainstCertificate { get; set; }

        /// <summary>
        /// Processed weight in Kg.
        /// </summary>
        /// <example>895</example>
        public double ProcessedWeight { get; set; }
    }
}