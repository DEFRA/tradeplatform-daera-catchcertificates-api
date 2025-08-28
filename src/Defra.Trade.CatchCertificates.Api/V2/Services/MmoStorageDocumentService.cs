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

public class MmoStorageDocumentService(IMapper mapper, IStorageDocumentRepository repository, ILogger<MmoStorageDocumentService> logger)
    : GenericMmoService<StorageDocument, StorageDocumentDataRow>(mapper, repository, logger)
{
    protected override void LogCreateSuccess(string documentNumber) => _logger.MmoStorageDocumentCreateSuccess(documentNumber);

    protected override void LogUpdateSuccess(string documentNumber) => _logger.MmoStorageDocumentUpdateSuccess(documentNumber);
}
