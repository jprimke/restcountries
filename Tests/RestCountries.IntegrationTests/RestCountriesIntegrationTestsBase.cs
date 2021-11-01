//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\Tests\RestCountries.Tests\RestCountriesIntegrationTestsBase.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using RestCountries.Data.Models;
using Xunit;

namespace RestCountries.IntegrationTests;

public abstract class RestCountriesIntegrationTestsBase
{
    protected readonly HttpClient client;

    public RestCountriesIntegrationTestsBase(bool useFile)
    {
        var application = new PlaygroundApplication(useFile);
        client = application.CreateClient();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAll_ShouldHave_Count_250()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/all");

        countries.Should().NotBeNullOrEmpty();
        countries.Should().HaveCount(250);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByNamePart_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/name/united");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByNamePart_Should_Have_All_Entries()
    {
        var response = await client.GetAsync("/countries/name");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByFullName_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/name/Germany?fullText=true");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByFullNativeName_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/name/Deutschland?fullText=true");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByFullNativeName_Should_Have_Least_No_Entry()
    {
        var response = await client.GetAsync("/countries/name?fullText=true");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [Trait("Category", "Integration")]
    [InlineData("de", 1)]
    [InlineData("deu", 1)]
    public async Task GetByAlphaCode_Should_Have_count_Entries(string alphaCode, int count)
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>($"/countries/alpha/{alphaCode}");

        countries.Should().NotBeNull();
        countries.Should().HaveCount(count);
    }

    [Theory]
    [Trait("Category", "Integration")]
    [InlineData("d")]
    [InlineData("deut")]
    public async Task GetByAlphaCode_Should_Have_ResponseStatusCode_404(string alphaCode)
    {
        var response = await client.GetAsync($"/countries/alpha/{alphaCode}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [Trait("Category", "Integration")]
    [InlineData("de;USA;cn", 3)]
    [InlineData("deu", 1)]
    [InlineData("d;us;ar", 2)]
    [InlineData("deut", 0)]
    public async Task GetByAlphaCodes_Should_Have_count_Entries(string alphaCodes, int count)
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>($"/countries/alpha?codes={alphaCodes}");

        countries.Should().NotBeNull();
        countries.Should().HaveCount(count);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByRegion_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/region/Europe");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetBySubRegion_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/subregion/Central Europe");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByCurrencyName_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/currency/Euro");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByCurrencyCode_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/currency/EUR");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByCallingCode_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/callingcode/1");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByCapital_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/capital/Berlin");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByRegionalBloc_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/regionalBloc/EU");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByTopLevelDomain_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/topleveldomain/de");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByTopLevelDomainWithPoint_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/topleveldomain/.de");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetByCioc_Should_Have_Least_One_Entry()
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>("/countries/cioc/GER");

        countries.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [Trait("Category", "Integration")]
    [InlineData("en")]
    [InlineData("deu")]
    [InlineData("English")]
    [InlineData("Deutsch")]
    public async Task GetByLanguage_Should_Have_least_One_Entry(string lang)
    {
        var countries = await client.GetFromJsonAsync<IEnumerable<CountryInfo>>($"/countries/lang/{lang}");

        countries.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetHealth_Should_Get_ResponseCode_200_and_Healthy()
    {
        var response = await client.GetAsync("/health");

        response.EnsureSuccessStatusCode();

        var expected = await response.Content.ReadAsStringAsync();

        expected.Should().Be("Healthy");
    }
}
