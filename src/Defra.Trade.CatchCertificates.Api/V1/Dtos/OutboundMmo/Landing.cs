// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.OutboundMmo
{
    /// <summary>
    /// Landing information.
    /// </summary>
    public class Landing
    {
        /// <summary>
        /// Status of the landing.
        /// </summary>
        /// <remarks>Validation Success, Validation Failure - Overuse, Validation Failure - Weight, Validation Failure - Species, Validation Failure - No Landing Data, Pending Landing Data</remarks>
        /// <example>Validation Success</example>
        public string Status { get; set; }

        /// <summary>
        /// Unique identifier for the landing.
        /// </summary>
        /// <example>GBR-2020-CC-8D02AD4D9-1607504140</example>
        public string Id { get; set; }

        /// <summary>
        /// Recorded date of the landing.
        /// </summary>
        /// <example>2020-11-02T00:00:00</example>
        public DateTimeOffset LandingDate { get; set; }

        /// <summary>
        /// Common species code.
        /// </summary>
        /// <example>MAC</example>
        public string Species { get; set; }

        /// <summary>
        /// Combined Nomenclature Code.
        /// </summary>
        /// <example>0101</example>
        public string CnCode { get; set; }

        /// <summary>
        /// Combined Nomenclature Description.
        /// </summary>
        public string CommodityCodeDescription { get; set; }

        /// <summary>
        /// Scientific name.
        /// </summary>
        /// <example>Conger conger</example>
        public string ScientificName { get; set; }

        /// <summary>
        /// State of the catch.
        /// </summary>
        /// <remarks>e.g. ALI, BOI, DRI, FRE, FRO, SAL</remarks>
        /// <example>FRE</example>
        public string State { get; set; }

        /// <summary>
        /// Presentation of the catch.
        /// </summary>
        /// <remarks>e.g. WHL, GUT, GUH, GHT, HEA, TAL, WNG, FIL, FIS, CLA, ROE</remarks>
        /// <example>WHL</example>
        public string Presentation { get; set; }

        /// <summary>
        /// Name of the vessel that landed the catch.
        /// </summary>
        /// <example>MARIGOLD</example>
        public string VesselName { get; set; }

        /// <summary>
        /// Port Letter and Number of the vessel landing the catch.
        /// </summary>
        /// <example>WK34</example>
        public string VesselPln { get; set; }

        /// <summary>
        /// Overall length of the vessel landing the catch in metres.
        /// </summary>
        /// <example>5.83</example>
        public double? VesselLength { get; set; }

        /// <summary>
        /// Licence holder for the vessel.
        /// </summary>
        public string LicenceHolder { get; set; }

        /// <summary>
        /// Source of the landing declaration data.
        /// </summary>
        /// <remarks>LANDING_DECLARATION, CATCH_RECORDING, ELOG</remarks>
        /// <example>CATCH_RECORDING</example>
        public string Source { get; set; }

        /// <summary>
        /// Certified export weight in Kg of catch.
        /// </summary>
        /// <example>1000</example>
        public double Weight { get; set; }

        /// <summary>
        /// Number of times a landing has been submitted for certification.
        /// </summary>
        /// <example>1</example>
        public int NumberOfTotalSubmissions { get; set; }

        /// <summary>
        /// Validation detail for the landing.
        /// </summary>
        public LandingValidation Validation { get; set; }
    }
}