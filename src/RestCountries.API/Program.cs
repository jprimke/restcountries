//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Program.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RestCountries.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1.0", new() { Title = "RestCountries.API", Version = "v1.0" }); });

builder.Services
       .Configure<CountryFileOptions>(config =>
                                            {
                                                config.Directory = builder.Configuration.GetValue<string>("ResourceDirectory");
                                                config.FileName = "allCountries.json";
                                            });
builder.Services.AddSingleton<ICountryContext, CountryFileContext>();
builder.Services.AddSingleton<CountryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "RestCountries.API v1.0"));
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("countries/all", (CountryRepository repository) => Results.Ok(repository.GetAll()));

app.MapGet("countries/alpha/{alphaCode:alpha:length(2,3)}",
           (CountryRepository repository, string alphaCode) => Results.Ok(repository.GetCountriesByAlphaCode(alphaCode)));

app.MapGet("countries/alpha",
           (CountryRepository repository, [FromQuery] string codes) =>
           {
               var splitCodes = codes.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
               return Results.Ok(repository.GetCountriesByAlphaCodes(splitCodes));
           });

app.MapGet("countries/name/{name}",
           (CountryRepository repository, string name, [FromQuery] bool? fullText) =>
               Results.Ok(repository.GetCountriesByName(name, fullText)));

app.MapGet("countries/region/{region:alpha}",
           (CountryRepository repository, string region) => Results.Ok(repository.GetCountriesByRegion(region)));

app.MapGet("countries/currency/{currency:alpha}",
           (CountryRepository repository, string currency) => Results.Ok(repository.GetCountriesByCurrency(currency)));

app.MapGet("countries/lang/{lang:alpha:length(2)}",
           (CountryRepository repository, string lang) => Results.Ok(repository.GetCountriesByLanguage(lang)));

app.MapGet("countries/callingcode/{callingcode}",
           (CountryRepository repository, string callingcode) => Results.Ok(repository.GetCountriesByCallingCode(callingcode)));

app.MapGet("countries/subregion/{subregion}",
           (CountryRepository repository, string subregion) => Results.Ok(repository.GetCountriesBySubRegion(subregion)));

app.MapGet("countries/capital/{capital:alpha}",
           (CountryRepository repository, string capital) => Results.Ok(repository.GetCountriesByCapital(capital)));

app.MapGet("countries/regionalBloc/{bloc:alpha}",
           (CountryRepository repository, string bloc) => Results.Ok(repository.GetCountriesByRegionalBloc(bloc)));

app.MapGet("countries/topleveldomain/{topleveldomain}",
           (CountryRepository repository, string topleveldomain) => Results.Ok(repository.GetCountriesByTopLevelDomain(topleveldomain)));

app.MapGet("countries/cioc/{cioc:alpha}", (CountryRepository repository, string cioc) => Results.Ok(repository.GetCountriesByCioc(cioc)));

app.MapHealthChecks("/health");

app.Run();
