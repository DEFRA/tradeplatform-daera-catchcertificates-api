// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using AutoMapper;
using Defra.Trade.CatchCertificates.Api.Data;
using Defra.Trade.CatchCertificates.Api.Models;
using Defra.Trade.CatchCertificates.Api.V2.Controllers;
using Defra.Trade.CatchCertificates.Api.V2.Dtos.Mmo;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Defra.Trade.CatchCertificates.Api.Tests.V2.Controllers;

public class MmoProcessingStatementControllerTests
{
    private readonly MmoProcessingStatementController _sut;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IProcessingStatementRepository> _repository;

    public MmoProcessingStatementControllerTests()
    {
        _mapper = new(MockBehavior.Strict);
        _repository = new(MockBehavior.Strict);
        _sut = new(_mapper.Object, _repository.Object);
    }

    [Fact]
    public async Task Upsert_CreatesTheProcessingStatement_WhenItDoesntAlreadyExist()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var dataRow = new ProcessingStatementDataRow();
        var request = new ProcessingStatement
        {
            DocumentNumber = documentNumber
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(null as ProcessingStatementDataRow);

        _mapper.Setup(m => m.Map<ProcessingStatementDataRow>(request))
            .Returns(dataRow);

        _repository.Setup(m => m.CreateAsync(dataRow))
            .ReturnsAsync(null as ProcessingStatementDataRow);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Upsert_CreatesTheProcessingStatement_WhenItDoesAlreadyExistButWasCreatedMoreRecently()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var current = new ProcessingStatementDataRow
        {
            CreatedOn = DateTimeOffset.UtcNow
        };
        var request = new ProcessingStatement
        {
            DocumentNumber = documentNumber,
            LastUpdated = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(-10))
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(current);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Upsert_CreatesTheProcessingStatement_WhenItDoesAlreadyExistButWasUpdatedMoreRecently()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var current = new ProcessingStatementDataRow
        {
            LastUpdated = DateTimeOffset.UtcNow
        };
        var request = new ProcessingStatement
        {
            DocumentNumber = documentNumber,
            LastUpdated = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(-10))
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(current);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Upsert_CreatesTheProcessingStatement_WhenItDoesAlreadyExistAndIsOlder()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var mapped = new ProcessingStatementDataRow();
        var current = new ProcessingStatementDataRow
        {
            LastUpdated = DateTimeOffset.UtcNow
        };
        var request = new ProcessingStatement
        {
            DocumentNumber = documentNumber,
            LastUpdated = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(10))
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(current);

        _mapper.Setup(m => m.Map(request, current))
            .Returns(mapped);

        _repository.Setup(m => m.UpdateAsync(mapped))
            .ReturnsAsync(null as ProcessingStatementDataRow);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }
}