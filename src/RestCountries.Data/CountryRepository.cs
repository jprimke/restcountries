//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.Data\CountryRepository.cs" company="AXA Partners">
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
        return context.Countries
                      .Where(c =>
                                 c.Alpha2Code.Equals(alphaCode, StringComparison.OrdinalIgnoreCase)
                                 || c.Alpha3Code.Equals(alphaCode, StringComparison.OrdinalIgnoreCase))
                      .ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByAlphaCodes(string[] alphaCodes)
    {
        return context.Countries
                      .Where(c =>
                                 alphaCodes.Any(a =>
                                                    a.Equals(c.Alpha2Code, StringComparison.OrdinalIgnoreCase)
                                                    || a.Equals(c.Alpha3Code, StringComparison.OrdinalIgnoreCase)))
                      .ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByName(string name, bool? fullText)
    {
        if (fullText ?? false)
        {
            return context.Countries
                          .Where(c =>
                                     c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                                     || c.NativeName.Equals(name, StringComparison.OrdinalIgnoreCase))
                          .ToList();
        }
        else
        {
            return context.Countries
                          .Where(c =>
                                     c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                                     || c.NativeName.Contains(name, StringComparison.OrdinalIgnoreCase))
                          .ToList();
        }
    }

    public IEnumerable<CountryInfo> GetCountriesByRegion(string region)
    {
        return context.Countries.Where(c => c.Region.Equals(region, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCurrency(string currency)
    {
        return context.Countries
                      .Where(c =>
                                 c.Currencies
                                  .Any(cur =>
                                           cur.Name.Equals(currency, StringComparison.OrdinalIgnoreCase)
                                           || string.Equals(cur.Code, currency, StringComparison.OrdinalIgnoreCase)))
                      .ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCallingCode(string callingcode)
    {
        return context.Countries.Where(c => c.CallingCodes.Any(c => c == callingcode)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCapital(string capital)
    {
        return context.Countries.Where(c => c.Capital.Equals(capital, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByRegionalBloc(string bloc)
    {
        return context.Countries
                      .Where(c =>
                                 c.RegionalBlocs
                                  .Any(b =>
                                           b.Name.Equals(bloc, StringComparison.OrdinalIgnoreCase)
                                           || string.Equals(b.Acronym, bloc, StringComparison.OrdinalIgnoreCase)))
                      .ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesBySubRegion(string subregion)
    {
        return context.Countries.Where(c => c.SubRegion.Equals(subregion, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByTopLevelDomain(string topleveldomain)
    {
        return context.Countries
                      .Where(c => c.TopLevelDomain.Any(t => t.Contains(topleveldomain, StringComparison.OrdinalIgnoreCase)))
                      .ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByCioc(string cioc)
    {
        return context.Countries.Where(c => c.Cioc.Equals(cioc, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public IEnumerable<CountryInfo> GetCountriesByLanguage(string lang)
    {
        return context.Countries
                      .Where(c =>
                                 c.Languages
                                  .Any(l =>
                                           l.Iso639_1.Equals(lang, StringComparison.OrdinalIgnoreCase)
                                           || l.Iso639_2.Equals(lang, StringComparison.OrdinalIgnoreCase)
                                           || l.Name.Equals(lang, StringComparison.OrdinalIgnoreCase)
                                           || l.NativeName.Equals(lang, StringComparison.OrdinalIgnoreCase)))
                      .ToList();
    }
}
