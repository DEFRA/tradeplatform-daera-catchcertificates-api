// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System;
using Defra.Trade.CatchCertificates.Api.Exceptions;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.Exceptions;

public class MmoDocumentNotFoundExceptionTests
{
    [Fact]
    public void Constructor_NoArgs_Success()
    {
        Assert.NotNull(new MmoDocumentNotFoundException());
    }

    [Fact]
    public void Constructor_Message_Success()
    {
        var ex = new MmoDocumentNotFoundException("Test Exception Message.");

        Assert.NotNull(ex);
        Assert.Equal("Test Exception Message.", ex.Message);
    }

    [Fact]
    public void Constructor_MessageWithInnerException_Success()
    {
        var ex = new MmoDocumentNotFoundException("Test Exception Message and Inner Exception.", new InvalidOperationException());

        Assert.NotNull(ex);
        Assert.Equal("Test Exception Message and Inner Exception.", ex.Message);
        Assert.IsType<InvalidOperationException>(ex.InnerException);
    }

    [Fact]
    public void Constructor_MessageWithDocumentNumber_Success()
    {
        string documentNumber = "GBR-2023-CC-123UVWXYZ";
        var ex = new MmoDocumentNotFoundException("Test Exception Message and Document Number Exception.", documentNumber);

        Assert.NotNull(ex);
        Assert.Equal("Test Exception Message and Document Number Exception.", ex.Message);
        Assert.Equal(documentNumber, ex.DocumentNumber);
    }

    [Fact]
    public void Constructor_MessageWithDocumentNumberAndWithInnerException_Success()
    {
        string documentNumber = "GBR-2023-CC-123UVWXYZ";
        var ex = new MmoDocumentNotFoundException("Test Exception Message and Document Number Exception.", documentNumber, new InvalidOperationException());

        Assert.NotNull(ex);
        Assert.Equal("Test Exception Message and Document Number Exception.", ex.Message);
        Assert.Equal(documentNumber, ex.DocumentNumber);
        Assert.IsType<InvalidOperationException>(ex.InnerException);
    }
}