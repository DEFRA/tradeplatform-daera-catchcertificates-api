// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.V2.Dtos.OutboundMmo;

public class DynamicsAddress
{
    /// <summary>
    /// The id of the address in dynamics
    /// </summary>
    /// <example>37BD4881-FC5D-430E-8FFE-99F5E6D8CA7A</example>
    public Guid? DefraAddressId { get; set; }

    /// <summary>
    /// The name of the building in dynamics
    /// </summary>
    public string DefraBuildingName { get; set; }

    /// <summary>
    /// The name of the country in dynamics
    /// </summary>
    public Guid? DefraCountryValue { get; set; }

    /// <summary>
    /// The country in dynamics
    /// </summary>
    public string DefraCounty { get; set; }

    /// <summary>
    /// The dependent locality in dynamics
    /// </summary>
    public string DefraDependentLocality { get; set; }

    /// <summary>
    /// Is this address from companies house
    /// </summary>
    public bool? DefraFromCompaniesHouse { get; set; }

    /// <summary>
    /// The international postcode in dynamics
    /// </summary>
    public string DefraInternationalPostalCode { get; set; }

    /// <summary>
    /// The locality in dynamics
    /// </summary>
    public string DefraLocality { get; set; }

    /// <summary>
    /// The postcode in dynamics
    /// </summary>
    public string DefraPostcode { get; set; }

    /// <summary>
    /// The premises name in dynamics
    /// </summary>
    public string DefraPremises { get; set; }

    /// <summary>
    /// The street in dynamics
    /// </summary>
    public string DefraStreet { get; set; }

    /// <summary>
    /// The sub-building name in dynamics
    /// </summary>
    public string DefraSubBuildingName { get; set; }

    /// <summary>
    /// The name of the town in dynamics
    /// </summary>
    public string DefraTownText { get; set; }

    /// <summary>
    /// The Unique Property Reference Number in dynamics
    /// </summary>
    public string DefraUprn { get; set; }
}