//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Data\CountryRepository.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestCountries.Data.Models;

namespace RestCountries.Data;

public class CountryFileContext : ICountryContext
{
    private readonly ILogger<CountryFileContext> logger;

    private List<CountryInfo> countries = new();
    
    public IQueryable<CountryInfo> Countries => countries.AsQueryable();

    internal CountryFileContext(ILogger<CountryFileContext> logger, string fileName)
    {
        this.logger = logger;
        countries = JsonSerializer.Deserialize<List<CountryInfo>>(File.OpenRead(fileName),
                                                                  new JsonSerializerOptions(JsonSerializerDefaults.Web))
            ?? new();
    }

    public CountryFileContext(ILogger<CountryFileContext> logger, IOptions<CountryFileOptions> options)
    {
        this.logger = logger;
        var countryRepositoryOptions = options.Value;
        var fileName = Path.Combine(countryRepositoryOptions.Directory, countryRepositoryOptions.FileName);

        countries = JsonSerializer.Deserialize<List<CountryInfo>>(File.OpenRead(fileName),
                                                                  new JsonSerializerOptions(JsonSerializerDefaults.Web))
            ?? new();
    }
}
