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
        var searchValue = "united";

        var countries = sut.GetCountriesByName(searchValue, false);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
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
        var searchValue = "Germany";

        var countries = sut.GetCountriesByName(searchValue, true);

        countries.Should().NotBeNullOrEmpty();
        countries.Should().HaveCount(1);
        countries.First().Name.ToLower().Should().Be(searchValue.ToLower());
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByFullNativeName_Should_Have_Least_One_Entry()
    {
        var searchValue = "Deutschland";

        var countries = sut.GetCountriesByName(searchValue, true);

        countries.Should().NotBeNullOrEmpty();
        countries.Should().HaveCount(1);
        countries.First().NativeName.ToLower().Should().Be(searchValue.ToLower());
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

        if (count > 0)
        {
            Alpha2CodeOrAlpha3CodeIsEqualToAlphaCode(countries.First(), alphaCode).Should().BeTrue();
        }
    }

    private bool Alpha2CodeOrAlpha3CodeIsEqualToAlphaCode(CountryInfo country, string alphaCode)
    {
        var alpha2Code = country.Alpha2Code;
        var alpha3Code = country.Alpha3Code;

        return alpha2Code.Equals(alphaCode, StringComparison.OrdinalIgnoreCase)
                    || alpha3Code.Equals(alphaCode, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [Trait("Category", "UnitTest")]
    [InlineData("de;USA;cn", 3)]
    [InlineData("deu", 1)]
    [InlineData("d;us;ar", 2)]
    [InlineData("deut", 0)]
    public void GetByAlphaCodes_Should_Have_count_Entries(string alphaCodes, int count)
    {
        var splitCodes = alphaCodes.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var countries = sut.GetCountriesByAlphaCodes(splitCodes);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(count);

        if (count > 0)
        {
            countries.All(c =>
                              alphaCodes.Contains(c.Alpha2Code, StringComparison.OrdinalIgnoreCase)
                              || alphaCodes.Contains(c.Alpha3Code, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
        }
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByRegion_Should_Have_Least_One_Entry()
    {
        var searchValue = "Europe";

        var countries = sut.GetCountriesByRegion(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.Region.Equals(searchValue, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetBySubRegion_Should_Have_Least_One_Entry()
    {
        var searchValue = "Central Europe";

        var countries = sut.GetCountriesBySubRegion(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.SubRegion.Equals(searchValue, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCurrencyName_Should_Have_Least_One_Entry()
    {
        var searchValue = "Euro";

        var countries = sut.GetCountriesByCurrency(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.Currencies.Any(c => c.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase))).Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCurrencyCode_Should_Have_Least_One_Entry()
    {
        var searchValue = "EUR";

        var countries = sut.GetCountriesByCurrency(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.Currencies.Any(c => c.Code.Equals(searchValue, StringComparison.OrdinalIgnoreCase))).Should().BeTrue();
    }

    [Theory]
    [Trait("Category", "UnitTest")]
    [InlineData("1")]
    [InlineData("49")]
    [InlineData("86")]
    public void GetByCallingCode_Should_Have_Least_One_Entry(string searchValue)
    {
        var countries = sut.GetCountriesByCallingCode(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.CallingCodes.Contains(searchValue)).Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCapital_Should_Have_Least_One_Entry()
    {
        var searchValue = "Berlin";

        var countries = sut.GetCountriesByCapital(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.Capital.Equals(searchValue, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByRegionalBloc_Should_Have_Least_One_Entry()
    {
        var searchValue = "EU";

        var countries = sut.GetCountriesByRegionalBloc(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c =>
                          c.RegionalBlocs
                           .Any(b =>
                                    b.Acronym.Equals(searchValue, StringComparison.OrdinalIgnoreCase)
                                    || b.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase)))
                 .Should()
                 .BeTrue();
    }

    [Theory]
    [Trait("Category", "UnitTest")]
    [InlineData("de")]
    [InlineData(".de")]
    public void GetByTopLevelDomain_Should_Have_Least_One_Entry(string searchValue)
    {
        var countries = sut.GetCountriesByTopLevelDomain(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.TopLevelDomain.Any(t => t.EndsWith(searchValue, StringComparison.OrdinalIgnoreCase))).Should().BeTrue();
    }


    [Fact]
    [Trait("Category", "UnitTest")]
    public void GetByCioc_Should_Have_Least_One_Entry()
    {
        var searchValue = "GER";

        var countries = sut.GetCountriesByCioc(searchValue);

        countries.Should().NotBeNullOrEmpty();
        countries.All(c => c.Cioc.Equals(searchValue, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
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
        countries.All(c =>
                          c.Languages
                           .Any(l =>
                                    l.Iso639_1.Equals(lang, StringComparison.OrdinalIgnoreCase)
                                    || l.Iso639_2.Equals(lang, StringComparison.OrdinalIgnoreCase)
                                    || l.Name.Equals(lang, StringComparison.OrdinalIgnoreCase)
                                    || l.NativeName.Equals(lang, StringComparison.OrdinalIgnoreCase)))
                 .Should()
                 .BeTrue();
    }
}
