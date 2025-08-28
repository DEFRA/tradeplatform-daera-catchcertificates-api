// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.V3.Dtos.Mmo;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.CatchCertificates.Api.Services;

public abstract class GenericMmoService<TIncoming, TDataRow>
    where TIncoming : MessageCore<Exporter, Country>
    where TDataRow : DataRow
{
    private readonly IMapper _mapper;
    private readonly IDataRowRepository<TDataRow> _repository;
    protected readonly ILogger _logger;

    protected GenericMmoService(
        IMapper mapper,
        IDataRowRepository<TDataRow> repository,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(logger);
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task UpsertItem(TIncoming item)
    {
        var existing = await _repository.GetByDocumentNumberAsync(item.DocumentNumber);

        if (existing is null)
        {
            var dataRow = _mapper.Map<TDataRow>(item);

            await _repository.CreateAsync(dataRow);

            LogCreateSuccess(item.DocumentNumber);
        }
        else
        {
            var existingTimestamp = existing.LastUpdated ?? existing.CreatedOn;

            if (item.LastUpdated >= existingTimestamp)
            {
                existing = _mapper.Map(item, existing);

                await _repository.UpdateAsync(existing);

                LogUpdateSuccess(item.DocumentNumber);
            }
        }
    }

    protected abstract void LogCreateSuccess(string documentNumber);

    protected abstract void LogUpdateSuccess(string documentNumber);
}
