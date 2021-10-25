//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Data\CountryRepository.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.Extensions.Options;
using RestCountries.API.Models;

namespace RestCountries.API.Data;

public class CountryRepository
{
    private readonly ILogger<CountryRepository> logger;

    private readonly IEnumerable<CountryInfo> countries;

    internal CountryRepository(ILogger<CountryRepository> logger, string fileName)
    {
        this.logger = logger;
        countries = JsonSerializer.Deserialize<List<CountryInfo>>(File.OpenRead(fileName),
                                                                  new JsonSerializerOptions(JsonSerializerDefaults.Web))
            ?? new();
    }

    public CountryRepository(ILogger<CountryRepository> logger, IOptions<CountryRepositoryOptions> options)
    {
        this.logger = logger;
        var countryRepositoryOptions = options.Value;
        var fileName = Path.Combine(countryRepositoryOptions.Directory, countryRepositoryOptions.FileName);

        countries = JsonSerializer.Deserialize<List<CountryInfo>>(File.OpenRead(fileName),
                                                                  new JsonSerializerOptions(JsonSerializerDefaults.Web))
            ?? new();
    }

    public IEnumerable<CountryInfo> GetAll()
    {
        return countries;
    }

    public IEnumerable<CountryInfo> GetCountriesByAlphaCode(string alphaCode)
    {
        return countries.Where(c => (c.Alpha2Code.ToLower() == alphaCode.ToLower()) || (c.Alpha3Code.ToLower() == alphaCode.ToLower()));
    }

    public IEnumerable<CountryInfo> GetCountriesByAlphaCodes(string[] alphaCodes)
    {
        alphaCodes = alphaCodes.Select(s => s.ToLower()).ToArray();
        return countries.Where(c => alphaCodes.Contains(c.Alpha2Code.ToLower()) || alphaCodes.Contains(c.Alpha3Code.ToLower()));
    }

    public IEnumerable<CountryInfo> GetCountriesByName(string name, bool? fullText)
    {
        name = name.ToLower();

        if (fullText ?? false)
        {
            return countries.Where(c => (c.Name.ToLower() == name) || (c.NativeName.ToLower() == name));
        }
        else
        {
            return countries.Where(c => c.Name.ToLower().Contains(name) || c.NativeName.ToLower().Contains(name));
        }
    }

    public IEnumerable<CountryInfo> GetCountriesByRegion(string region)
    {
        return countries.Where(c => c.Region.ToLower() == region.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByCurrency(string currency)
    {
        return countries.Where(c =>
                                   c.Currencies
                                    .Any(cur => (cur.Name.ToLower() == currency.ToLower()) || (cur.Code.ToLower() == currency.ToLower())));
    }

    public IEnumerable<CountryInfo> GetCountriesByCallingCode(string callingcode)
    {
        return countries.Where(c => c.CallingCodes.Any(c => c == callingcode));
    }

    public IEnumerable<CountryInfo> GetCountriesByCapital(string capital)
    {
        return countries.Where(c => c.Capital.ToLower() == capital.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByRegionalBloc(string bloc)
    {
        return countries
             .Where(c => c.RegionalBlocs.Any(b => (b.Name.ToLower() == bloc.ToLower()) || (b.Acronym.ToLower() == bloc.ToLower())));
    }

    public IEnumerable<CountryInfo> GetCountriesBySubRegion(string subregion)
    {
        return countries.Where(c => c.SubRegion.ToLower() == subregion.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByTopLevelDomain(string topleveldomain)
    {
        return countries.Where(c => c.TopLevelDomain.Any(t => t.ToLower().Contains(topleveldomain.ToLower())));
    }

    public IEnumerable<CountryInfo> GetCountriesByCioc(string cioc)
    {
        return countries.Where(c => c.Cioc.ToLower() == cioc.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByLanguage(string lang)
    {
        lang = lang.ToLower();
        return countries
             .Where(c =>
                        c.Languages
                         .Any(l =>
                                  (l.Iso639_1.ToLower() == lang)
                                  || (l.Iso639_2.ToLower() == lang)
                                  || (l.Name.ToLower() == lang)
                                  || (l.NativeName.ToLower() == lang)));
    }
}
