// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using Defra.Trade.Common.ExternalApi.Auditing.Data;
using Defra.Trade.Common.ExternalApi.Auditing.Models;
using Defra.Trade.Common.ExternalApi.Auditing.Models.Enums;
using Moq;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers;

public static class TestVerificationHelpers
{
    public static void VerifyAuditLogged(
        this Mock<IAuditRepository> auditRepoMock,
        AuditLogType logType,
        Guid? clientId,
        string systemRequestId,
        string httpMethod,
        string path,
        string queryString,
        int statusCode,
        DateTimeOffset loggedOnOrAfter,
        bool hasRequestBody,
        bool hasResponseBody,
        string clientIPAddress)
    {
        Func<AuditLog, bool> isAuditMatch = (a) =>
        {
            if (a == null)
                return false;

            return a.LogType == logType
                   && a.ClientId == clientId
                   && a.SystemRequestId == systemRequestId
                   && !string.IsNullOrWhiteSpace(a.TraceId)
                   && a.HttpMethod == httpMethod
                   && (a.Path == null ? path == null : a.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
                   && (a.QueryString == null ? queryString == null : a.QueryString.Equals(queryString, StringComparison.OrdinalIgnoreCase))
                   && a.HttpStatusCode == statusCode
                   && a.Timestamp >= loggedOnOrAfter
                   && string.IsNullOrEmpty(a.Data.RequestData) != hasRequestBody
                   && string.IsNullOrEmpty(a.Data.ResponseData) != hasResponseBody
                   && (a.ClientIPAddress == null ? clientIPAddress == null : a.ClientIPAddress.Equals(clientIPAddress, StringComparison.OrdinalIgnoreCase));
        };

        auditRepoMock.Verify(r =>
            r.CreateAsync(
                It.Is<AuditLog>(a => isAuditMatch(a))));
    }
}