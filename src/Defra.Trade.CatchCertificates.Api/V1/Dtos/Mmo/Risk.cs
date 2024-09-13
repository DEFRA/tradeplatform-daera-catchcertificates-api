// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    public class Risk
    {
        public string Vessel { get; set; }

        public string SpeciesRisk { get; set; }

        public string ExporterRiskScore { get; set; }

        public string LandingRiskScore { get; set; }

        /// <remarks>High, Low</remarks>
        public string HighOrLowRisk { get; set; }

        public bool IsSpeciesRiskEnabled { get; set; }

        public IEnumerable<string> OveruseInfo { get; set; }
    }
}