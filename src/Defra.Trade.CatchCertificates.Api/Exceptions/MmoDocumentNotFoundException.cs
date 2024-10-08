// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;

namespace Defra.Trade.CatchCertificates.Api.Exceptions;

public class MmoDocumentNotFoundException : Exception
{
    public MmoDocumentNotFoundException()
    {
    }

    public MmoDocumentNotFoundException(string message) : base(message)
    {
    }

    public MmoDocumentNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public MmoDocumentNotFoundException(string message, string documentNumber)
        : base(message)
    {
        DocumentNumber = documentNumber;
    }

    public MmoDocumentNotFoundException(string message, string documentNumber, Exception innerException)
        : base(message, innerException)
    {
        DocumentNumber = documentNumber;
    }

    public string DocumentNumber { get; private set; }
}