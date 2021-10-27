using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RestCountries.Data;
using RestCountries.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace RestCountries.Tests;

public partial class CountryRepositoryTests
{
    private readonly CountryRepository sut = null!;

    private Mock<ICountryContext> contextMock;
    private IEnumerable<CountryInfo> testCountries = Enumerable.Empty<CountryInfo>();


    public CountryRepositoryTests()
    {
        var factory = new LoggerFactory();
        testCountries = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CountryInfo>>(testData, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? Enumerable.Empty<CountryInfo>();
        contextMock = new Mock<ICountryContext>();
        contextMock.Setup(x => x.Countries).Returns(() => testCountries.AsQueryable());
        sut = new CountryRepository(factory.CreateLogger<CountryRepository>(), contextMock.Object);
    }

    [Fact]
    public void GetAll_ShouldHave_Count_4()
    {       
        var all = sut.GetAll();

        all.Should().NotBeNullOrEmpty();
        all.Should().HaveCount(4);
    }


    [Fact]
    public void GetByNamePart_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByName("united", false);

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByNamePart_Should_Have_All_Entries()
    {
        var countries = sut.GetCountriesByName("", false);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(4);
    }

    [Fact]
    public void GetByFullName_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByName("Germany", true);

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByFullNativeName_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByName("Deutschland", true);

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByFullNativeName_Should_Have_Least_No_Entry()
    {
        var countries = sut.GetCountriesByName("", true);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(0);
    }

    [Theory]
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
    [InlineData("de;USA;cn", 3)]
    [InlineData("deu", 1)]
    [InlineData("d;us;ar", 2)]
    [InlineData("deut", 0)]
    public void GetByAlphaCodes_Should_Have_count_Entries(string alphaCode, int count)
    {
        var splitCodes = alphaCode.Split(';', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries);
        var countries = sut.GetCountriesByAlphaCodes(splitCodes);

        countries.Should().NotBeNull();
        countries.Should().HaveCount(count);
    }

    [Fact]
    public void GetByRegion_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByRegion("Europe");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetBySubRegion_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesBySubRegion("Central Europe");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByCurrencyName_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCurrency("Euro");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByCurrencyCode_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCurrency("EUR");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByCallingCode_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCallingCode("1");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByCapital_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCapital("Berlin");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByRegionalBloc_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByRegionalBloc("EU");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByTopLevelDomain_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByTopLevelDomain("de");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByTopLevelDomainWithPoint_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByTopLevelDomain(".de");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByCioc_Should_Have_Least_One_Entry()
    {
        var countries = sut.GetCountriesByCioc("GER");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByLanguageIso639_1_Should_Have_least_One_Entry()
    {
        var countries = sut.GetCountriesByLanguage("en");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByLanguageIso639_2_Should_Have_least_One_Entry()
    {
        var countries = sut.GetCountriesByLanguage("deu");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByLanguageName_Should_Have_least_One_Entry()
    {
        var countries = sut.GetCountriesByLanguage("English");
        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetByLanguageNative_Should_Have_least_One_Entry()
    {
        var countries = sut.GetCountriesByLanguage("Deutsch");
        countries.Should().NotBeNullOrEmpty();
    }
}
