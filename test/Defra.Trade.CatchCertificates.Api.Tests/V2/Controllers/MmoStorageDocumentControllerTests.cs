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

public class MmoStorageDocumentControllerTests
{
    private readonly MmoStorageDocumentController _sut;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IStorageDocumentRepository> _repository;

    public MmoStorageDocumentControllerTests()
    {
        _mapper = new(MockBehavior.Strict);
        _repository = new(MockBehavior.Strict);
        _sut = new(_mapper.Object, _repository.Object);
    }

    [Fact]
    public async Task Upsert_CreatesTheStorageDocument_WhenItDoesntAlreadyExist()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var dataRow = new StorageDocumentDataRow();
        var request = new StorageDocument
        {
            DocumentNumber = documentNumber
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(null as StorageDocumentDataRow);

        _mapper.Setup(m => m.Map<StorageDocumentDataRow>(request))
            .Returns(dataRow);

        _repository.Setup(m => m.CreateAsync(dataRow))
            .ReturnsAsync(null as StorageDocumentDataRow);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Upsert_CreatesTheStorageDocument_WhenItDoesAlreadyExistButWasCreatedMoreRecently()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var current = new StorageDocumentDataRow
        {
            CreatedOn = DateTimeOffset.UtcNow
        };
        var request = new StorageDocument
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
    public async Task Upsert_CreatesTheStorageDocument_WhenItDoesAlreadyExistButWasUpdatedMoreRecently()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var current = new StorageDocumentDataRow
        {
            LastUpdated = DateTimeOffset.UtcNow
        };
        var request = new StorageDocument
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
    public async Task Upsert_CreatesTheStorageDocument_WhenItDoesAlreadyExistAndIsOlder()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var mapped = new StorageDocumentDataRow();
        var current = new StorageDocumentDataRow
        {
            LastUpdated = DateTimeOffset.UtcNow
        };
        var request = new StorageDocument
        {
            DocumentNumber = documentNumber,
            LastUpdated = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(10))
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(current);

        _mapper.Setup(m => m.Map(request, current))
            .Returns(mapped);

        _repository.Setup(m => m.UpdateAsync(mapped))
            .ReturnsAsync(null as StorageDocumentDataRow);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }
}