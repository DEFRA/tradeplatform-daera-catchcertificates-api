// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.Models;

public interface IUpdateTracked
{
    DateTimeOffset? LastUpdated { get; set; }

    string LastUpdatedBy { get; set; }

    string LastUpdatedSystem { get; set; }
}