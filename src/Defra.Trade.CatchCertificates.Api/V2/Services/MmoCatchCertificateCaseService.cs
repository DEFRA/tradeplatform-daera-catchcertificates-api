// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Extensions;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.Services;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.CatchCertificates.Api.V2.Services;

public class MmoCatchCertificateCaseService(IMapper mapper, ICatchCertificateCaseRepository repository, ILogger<MmoCatchCertificateCaseService> logger)
    : GenericMmoService<CatchCertificateCase, CatchCertificateCaseDataRow>(mapper, repository, logger)
{
    protected override void LogCreateSuccess(string documentNumber) => _logger.MmoCatchCertificateCaseCreateSuccess(documentNumber);
    protected override void LogUpdateSuccess(string documentNumber) => _logger.MmoCatchCertificateCaseUpdateSuccess(documentNumber);
}
