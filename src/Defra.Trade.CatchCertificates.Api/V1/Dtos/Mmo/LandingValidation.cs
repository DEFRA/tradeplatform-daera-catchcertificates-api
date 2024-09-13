// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    public class LandingValidation
    {
        public double LiveExportWeight { get; set; }

        public double? TotalWeightForSpecies { get; set; }

        public double? TotalLiveForExportSpecies { get; set; }

        public double? TotalEstimatedForExportSpecies { get; set; }

        public double? TotalEstimatedWithTolerance { get; set; }

        public double? TotalRecordedAgainstLanding { get; set; }

        public double? LandedWeightExceededBy { get; set; }

        public string RawLandingsUrl { get; set; }

        public string SalesNoteUrl { get; set; }

        public bool IsLegallyDue { get; set; }
    }
}
