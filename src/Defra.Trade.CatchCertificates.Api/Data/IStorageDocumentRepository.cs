// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Defra.Trade.CatchCertificates.Api.Models;

namespace Defra.Trade.CatchCertificates.Api.Data
{
    public interface IStorageDocumentRepository
    {
        Task<Tuple<IEnumerable<StorageDocumentDataRow>, long>> GetPaginatedAsync(long pageNumber, long pageSize);

        Task<StorageDocumentDataRow> GetByDocumentNumberAsync(string documentNumber);

        Task<StorageDocumentDataRow> CreateAsync(StorageDocumentDataRow dataRow);

        Task<StorageDocumentDataRow> UpdateAsync(StorageDocumentDataRow dataRow);
    }
}