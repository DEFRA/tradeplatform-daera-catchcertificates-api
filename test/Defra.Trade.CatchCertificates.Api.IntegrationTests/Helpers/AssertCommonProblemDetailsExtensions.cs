// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using System.Net;
using Defra.Trade.Common.Api.Dtos;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.IntegrationTests.Helpers
{
    public static class CommonProblemDetailsAssertions
    {
        public static void Verify(CommonProblemDetails problem, HttpStatusCode status)
        {
            Assert.Equal((int)status, problem.Status);
            Assert.NotNull(problem.Title);
            Assert.True(Uri.IsWellFormedUriString(problem.Type, UriKind.RelativeOrAbsolute));
        }
    }
}
