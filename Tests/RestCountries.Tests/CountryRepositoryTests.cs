//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\Tests\RestCountries.Tests\CountryRepositoryTests.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RestCountries.Data;
using RestCountries.Data.Models;
using Xunit;

namespace RestCountries.Tests;

public partial class CountryRepositoryTests
{
    private readonly CountryRepository sut = null!;

    private readonly Mock<ICountryContext> contextMock;

    private readonly IEnumerable<CountryInfo> testCountries = Enumerable.Empty<CountryInfo>();

    public CountryRepositoryTests()
    {
        var factory = new LoggerFactory();
        testCountries = JsonSerializer.Deserialize<IEnumerable<CountryInfo>>(TestData,
                                                                             new JsonSerializerOptions(JsonSerializerDefaults.Web))
            ?? Enumerable.Empty<CountryInfo>();
        contextMock = new Mock<ICountryContext>();
        contextMock.Setup(x => x.Countries).Returns(() => testCountries.AsQueryable());
        sut = new CountryRepository(factory.CreateLogger<CountryRepository>(), contextMock.Object);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetAll_ShouldHave_Count_4()
    {
        var all = sut.GetAll();

        all.Should().NotBeNullOrEmpty();
        all.Should().HaveCount(4);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByNamePart_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByName("united", false);

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByNamePart_Should_Have_All_Entries()
    {
        var countries = sut.GetCountriesByName(string.Empty, false);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(4);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByFullName_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByName("Germany", true);

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByFullNativeName_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByName("Deutschland", true);

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByFullNativeName_Should_Have_Least_No_Entry()
    {
        var countries = sut.GetCountriesByName(string.Empty, true);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(0);
    }

    [Theory]
    [Trait("Category", "UnitTest")]
    [InlineData("de", 1)]
    [InlineData("deu", 1)]
    [InlineData("d", 0)]
    [InlineData("deut", 0)]
    public void GetByAlphaCode_Should_Have_count_Entries(string alphaCode, int count)
    {
        var countries = sut.GetCountriesByAlphaCode(alphaCode);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(count);
    }

    [Theory]
    [Trait("Category", "UnitTest")]
    [InlineData("de;USA;cn", 3)]
    [InlineData("deu", 1)]
    [InlineData("d;us;ar", 2)]
    [InlineData("deut", 0)]
    public void GetByAlphaCodes_Should_Have_count_Entries(string alphaCode, int count)
    {
        var splitCodes = alphaCode.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var countries = sut.GetCountriesByAlphaCodes(splitCodes);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(count);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByRegion_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByRegion("Europe");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetBySubRegion_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesBySubRegion("Central Europe");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCurrencyName_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCurrency("Euro");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCurrencyCode_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCurrency("EUR");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCallingCode_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCallingCode("1");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCapital_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCapital("Berlin");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByRegionalBloc_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByRegionalBloc("EU");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByTopLevelDomain_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByTopLevelDomain("de");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByTopLevelDomainWithPoint_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByTopLevelDomain(".de");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCioc_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCioc("GER");
        countries.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [Trait("Category", "UnitTest")]
    [InlineData("en")]
    [InlineData("deu")]
    [InlineData("English")]
    [InlineData("Deutsch")]
    public void GetByLanguage_Should_Have_least_One_Entry(string lang)
    {
        var countries = sut.GetCountriesByLanguage(lang);
        countries.Should().NotBeNullOrEmpty();
    }
}
