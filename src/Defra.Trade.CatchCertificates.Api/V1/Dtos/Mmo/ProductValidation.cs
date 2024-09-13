// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Collections.Generic;

namespace Defra.Trade.CatchCertificates.Api.V1.Dtos.Mmo
{
    public class ProductValidation
    {
        /// <remarks>Validation Success, Validation Failure - Overuse, Validation Failure - Weight</remarks>
        public string Status { get; set; }

        public double TotalWeightExported { get; set; }

        public double? WeightExceededAmount { get; set; }

        public IEnumerable<string> OveruseInfo { get; set; }
    }
}