//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Data\CountryRepository.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using RestCountries.API.Models;

namespace RestCountries.API.Data;

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
        return context.Countries;
    }

    public IEnumerable<CountryInfo> GetCountriesByAlphaCode(string alphaCode)
    {
        return context.Countries.Where(c => (c.Alpha2Code.ToLower() == alphaCode.ToLower()) || (c.Alpha3Code.ToLower() == alphaCode.ToLower()));
    }

    public IEnumerable<CountryInfo> GetCountriesByAlphaCodes(string[] alphaCodes)
    {
        alphaCodes = alphaCodes.Select(s => s.ToLower()).ToArray();
        return context.Countries.Where(c => alphaCodes.Contains(c.Alpha2Code.ToLower()) || alphaCodes.Contains(c.Alpha3Code.ToLower()));
    }

    public IEnumerable<CountryInfo> GetCountriesByName(string name, bool? fullText)
    {
        name = name.ToLower();

        if (fullText ?? false)
        {
            return context.Countries.Where(c => (c.Name.ToLower() == name) || (c.NativeName.ToLower() == name));
        }
        else
        {
            return context.Countries.Where(c => c.Name.ToLower().Contains(name) || c.NativeName.ToLower().Contains(name));
        }
    }

    public IEnumerable<CountryInfo> GetCountriesByRegion(string region)
    {
        return context.Countries.Where(c => c.Region.ToLower() == region.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByCurrency(string currency)
    {
        return context.Countries.Where(c =>
                                   c.Currencies
                                    .Any(cur => (cur.Name.ToLower() == currency.ToLower()) || (cur.Code.ToLower() == currency.ToLower())));
    }

    public IEnumerable<CountryInfo> GetCountriesByCallingCode(string callingcode)
    {
        return context.Countries.Where(c => c.CallingCodes.Any(c => c == callingcode));
    }

    public IEnumerable<CountryInfo> GetCountriesByCapital(string capital)
    {
        return context.Countries.Where(c => c.Capital.ToLower() == capital.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByRegionalBloc(string bloc)
    {
        return context.Countries
             .Where(c => c.RegionalBlocs.Any(b => (b.Name.ToLower() == bloc.ToLower()) || (b.Acronym.ToLower() == bloc.ToLower())));
    }

    public IEnumerable<CountryInfo> GetCountriesBySubRegion(string subregion)
    {
        return context.Countries.Where(c => c.SubRegion.ToLower() == subregion.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByTopLevelDomain(string topleveldomain)
    {
        return context.Countries.Where(c => c.TopLevelDomain.Any(t => t.ToLower().Contains(topleveldomain.ToLower())));
    }

    public IEnumerable<CountryInfo> GetCountriesByCioc(string cioc)
    {
        return context.Countries.Where(c => c.Cioc.ToLower() == cioc.ToLower());
    }

    public IEnumerable<CountryInfo> GetCountriesByLanguage(string lang)
    {
        lang = lang.ToLower();
        return context.Countries
             .Where(c =>
                        c.Languages
                         .Any(l =>
                                  (l.Iso639_1.ToLower() == lang)
                                  || (l.Iso639_2.ToLower() == lang)
                                  || (l.Name.ToLower() == lang)
                                  || (l.NativeName.ToLower() == lang)));
    }
}
