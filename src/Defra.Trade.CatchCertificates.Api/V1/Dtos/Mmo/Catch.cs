// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    public class Catch
    {
        public string ForeignCatchCertificateNumber { get; set; }

        public string Species { get; set; }

        public string Id { get; set; }

        public string ScientificName { get; set; }

        public string CnCode { get; set; }

        public double ImportedWeight { get; set; }

        public double UsedWeightAgainstCertificate { get; set; }

        public double ProcessedWeight { get; set; }

        public CatchValidation Validation { get; set; }
    }
}