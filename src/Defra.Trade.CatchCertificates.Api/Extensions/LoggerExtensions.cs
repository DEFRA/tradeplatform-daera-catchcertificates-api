// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.CatchCertificates.Api.Extensions;

[ExcludeFromCodeCoverage]
public static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 10, EventName = nameof(MmoCatchCertificateCaseCreateSuccess), Level = LogLevel.Information, Message = "MMO (Internal) Catch Certificate Case with document number: {DocumentNumber} created")]
    public static partial void MmoCatchCertificateCaseCreateSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 11, EventName = nameof(MmoCatchCertificateCaseUpdateSuccess), Level = LogLevel.Information, Message = "MMO (Internal) Catch Certificate Case with document number: {DocumentNumber} updated")]
    public static partial void MmoCatchCertificateCaseUpdateSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 20, EventName = nameof(MmoProcessingStatementCreateSuccess), Level = LogLevel.Information, Message = "MMO (Internal) Processing Statement with document number: {DocumentNumber} created")]
    public static partial void MmoProcessingStatementCreateSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 21, EventName = nameof(MmoProcessingStatementUpdateSuccess), Level = LogLevel.Information, Message = "MMO (Internal) Processing Statement with document number: {DocumentNumber} updated")]
    public static partial void MmoProcessingStatementUpdateSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 30, EventName = nameof(MmoStorageDocumentCreateSuccess), Level = LogLevel.Information, Message = "MMO (Internal) Storage Document with document number: {DocumentNumber} created")]
    public static partial void MmoStorageDocumentCreateSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 31, EventName = nameof(MmoStorageDocumentUpdateSuccess), Level = LogLevel.Information, Message = "MMO (Internal) Storage Document with document number: {DocumentNumber} updated")]
    public static partial void MmoStorageDocumentUpdateSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 40, EventName = nameof(CatchCertificateCaseGetByIdSuccess), Level = LogLevel.Information, Message = "External request for Catch Certificate Case with document number: {DocumentNumber} retrieved")]
    public static partial void CatchCertificateCaseGetByIdSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 41, EventName = nameof(CatchCertificateCaseGetByIdNotFound), Level = LogLevel.Information, Message = "External request for Catch Certificate Case with document number: {DocumentNumber} not found")]
    public static partial void CatchCertificateCaseGetByIdNotFound(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 50, EventName = nameof(ProcessingStatementGetByIdSuccess), Level = LogLevel.Information, Message = "External request for Catch Certificate Case with document number: {DocumentNumber} retrieved externally")]
    public static partial void ProcessingStatementGetByIdSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 51, EventName = nameof(ProcessingStatementGetByIdNotFound), Level = LogLevel.Information, Message = "External request for Catch Certificate Case with document number: {DocumentNumber} not found")]
    public static partial void ProcessingStatementGetByIdNotFound(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 60, EventName = nameof(StorageDocumentGetByIdSuccess), Level = LogLevel.Information, Message = "External request for Storage Document with document number: {DocumentNumber} retrieved")]
    public static partial void StorageDocumentGetByIdSuccess(this ILogger logger, string documentNumber);

    [LoggerMessage(EventId = 61, EventName = nameof(StorageDocumentGetByIdNotFound), Level = LogLevel.Information, Message = "External request for Storage Document with document number: {DocumentNumber} not found")]
    public static partial void StorageDocumentGetByIdNotFound(this ILogger logger, string documentNumber);
}
