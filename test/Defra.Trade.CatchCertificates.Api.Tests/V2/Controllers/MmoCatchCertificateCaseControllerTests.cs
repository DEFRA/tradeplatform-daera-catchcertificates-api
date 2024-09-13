// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

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

public class MmoCatchCertificateCaseControllerTests
{
    private readonly MmoCatchCertificateCaseController _sut;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ICatchCertificateCaseRepository> _repository;

    public MmoCatchCertificateCaseControllerTests()
    {
        _mapper = new(MockBehavior.Strict);
        _repository = new(MockBehavior.Strict);
        _sut = new(_mapper.Object, _repository.Object);
    }

    [Fact]
    public async Task Upsert_CreatesTheCatchCertificateCase_WhenItDoesntAlreadyExist()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var dataRow = new CatchCertificateCaseDataRow();
        var request = new CatchCertificateCase
        {
            DocumentNumber = documentNumber
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(null as CatchCertificateCaseDataRow);

        _mapper.Setup(m => m.Map<CatchCertificateCaseDataRow>(request))
            .Returns(dataRow);

        _repository.Setup(m => m.CreateAsync(dataRow))
            .ReturnsAsync(null as CatchCertificateCaseDataRow);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Upsert_CreatesTheCatchCertificateCase_WhenItDoesAlreadyExistButWasCreatedMoreRecently()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var current = new CatchCertificateCaseDataRow
        {
            CreatedOn = DateTimeOffset.UtcNow
        };
        var request = new CatchCertificateCase
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
    public async Task Upsert_CreatesTheCatchCertificateCase_WhenItDoesAlreadyExistButWasUpdatedMoreRecently()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var current = new CatchCertificateCaseDataRow
        {
            LastUpdated = DateTimeOffset.UtcNow
        };
        var request = new CatchCertificateCase
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
    public async Task Upsert_CreatesTheCatchCertificateCase_WhenItDoesAlreadyExistAndIsOlder()
    {
        // arrange
        string documentNumber = Guid.NewGuid().ToString();
        var mapped = new CatchCertificateCaseDataRow();
        var current = new CatchCertificateCaseDataRow
        {
            LastUpdated = DateTimeOffset.UtcNow
        };
        var request = new CatchCertificateCase
        {
            DocumentNumber = documentNumber,
            LastUpdated = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(10))
        };

        _repository.Setup(m => m.GetByDocumentNumberAsync(documentNumber))
            .ReturnsAsync(current);

        _mapper.Setup(m => m.Map(request, current))
            .Returns(mapped);

        _repository.Setup(m => m.UpdateAsync(mapped))
            .ReturnsAsync(null as CatchCertificateCaseDataRow);

        // act
        var actual = await _sut.Upsert(request);

        // assert
        actual.Should().BeOfType<NoContentResult>();
    }
}