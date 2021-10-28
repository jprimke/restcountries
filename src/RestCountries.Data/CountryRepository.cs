//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Data\CountryRepository.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;
using RestCountries.Data.Models;

namespace RestCountries.Data;

public class CountryRepository
{
    private readonly ILogger<CountryRepository> logger;
    private readonly ICountryContext context;


    public CountryRepository(ILogger<CountryRepository> logger, ICountryContext context)
    {
        this.context = context;
        this.logger = logger;
    }

    public IEnumerable<CountryInfo> GetAll()
    {
        return context.Countries.ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByAlphaCode(string alphaCode)
    {
        return context.Countries.Where(c => string.Equals(c.Alpha2Code, alphaCode, StringComparison.OrdinalIgnoreCase) || string.Equals(c.Alpha3Code, alphaCode, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByAlphaCodes(string[] alphaCodes)
    {
        alphaCodes = alphaCodes.Select(s => s.ToLower()).ToArray();
        return context.Countries.Where(c => alphaCodes.Contains(c.Alpha2Code.ToLower()) || alphaCodes.Contains(c.Alpha3Code.ToLower())).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByName(string name, bool? fullText)
    {
        name = name.ToLower();

        if (fullText ?? false)
        {
            return context.Countries.Where(c => c.Name.ToLower() == name || c.NativeName.ToLower() == name).ToList();
        }
        else
        {
            return context.Countries.Where(c => c.Name.ToLower().Contains(name) || c.NativeName.ToLower().Contains(name)).ToList();
        }
    }

    public IEnumerable<CountryInfo> GetCountriesByRegion(string region)
    {
        return context.Countries.Where(c => string.Equals(c.Region, region, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCurrency(string currency)
    {
        return context.Countries.Where(c =>
                                   c.Currencies
                                    .Any(cur => string.Equals(cur.Name, currency, StringComparison.OrdinalIgnoreCase) || string.Equals(cur.Code, currency, StringComparison.OrdinalIgnoreCase))).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCallingCode(string callingcode)
    {
        return context.Countries.Where(c => c.CallingCodes.Any(c => c == callingcode)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCapital(string capital)
    {
        return context.Countries.Where(c => string.Equals(c.Capital, capital, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByRegionalBloc(string bloc)
    {
        return context.Countries
             .Where(c => c.RegionalBlocs.Any(b => string.Equals(b.Name, bloc, StringComparison.OrdinalIgnoreCase) || string.Equals(b.Acronym, bloc, StringComparison.OrdinalIgnoreCase))).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesBySubRegion(string subregion)
    {
        return context.Countries.Where(c => string.Equals(c.SubRegion, subregion, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByTopLevelDomain(string topleveldomain)
    {
        return context.Countries.Where(c => c.TopLevelDomain.Any(t => t.Contains(topleveldomain, StringComparison.OrdinalIgnoreCase))).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCioc(string cioc)
    {
        return context.Countries.Where(c => string.Equals(c.Cioc, cioc, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByLanguage(string lang)
    {
        lang = lang.ToLower();
        return context.Countries
             .Where(c =>
                        c.Languages
                         .Any(l =>
                                  l.Iso639_1.ToLower() == lang
                                  || l.Iso639_2.ToLower() == lang
                                  || l.Name.ToLower() == lang
                                  || l.NativeName.ToLower() == lang)).ToList();
    }
}
