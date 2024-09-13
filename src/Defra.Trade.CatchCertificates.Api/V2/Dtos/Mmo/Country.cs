// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;

public class Country
{
    public string OfficialCountryName { get; set; }

    public string IsoCodeAlpha2 { get; set; }

    public string IsoCodeAlpha3 { get; set; }

    public string IsoNumericCode { get; set; }
}