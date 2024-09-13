// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    /// <remarks>Validation Success, Validation Failure - Overuse, Validation Failure - Weight, Validation Failure - Species, Validation Failure - No Landing Data, Pending Landing Data</remarks>
    public class Landing
    {
        public string Status { get; set; }

        public string Id { get; set; }

        public DateTimeOffset LandingDate { get; set; }

        public bool? WasValidationDueAtPointOfApplication { get; set; }

        public string Species { get; set; }

        public string CnCode { get; set; }

        public string CommodityCodeDescription { get; set; }

        public string ScientificName { get; set; }

        public bool Is14DayLimitReached { get; set; }

        public string State { get; set; }

        public string Presentation { get; set; }

        public string VesselName { get; set; }

        public string VesselPln { get; set; }

        public double? VesselLength { get; set; }

        public string LicenceHolder { get; set; }

        public string Source { get; set; }

        public string SpeciesAlias { get; set; }

        public double Weight { get; set; }

        public int NumberOfTotalSubmissions { get; set; }

        public bool? VesselOverriddenByAdmin { get; set; }

        public LandingValidation Validation { get; set; }

        public Risk Risking { get; set; }
    }
}