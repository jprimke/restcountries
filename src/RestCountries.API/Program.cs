//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Program.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestCountries.Data;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1.0", new() { Title = "RestCountries.API", Version = "v1.0" }); });

bool useJsonFile = configuration.GetValue<bool>("UseJsonFile");

if (useJsonFile)
{
    builder.Services
           .Configure<CountryFileOptions>(config =>
                                          {
                                              var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                                              config.Directory = Path.Combine(rootPath, configuration.GetValue<string>("ResourceDirectory"));
                                              config.FileName = "allCountries.json";
                                          });

    builder.Services.AddSingleton<ICountryContext, CountryFileContext>();
}
else
{
    builder.Services
           .AddScoped<DbContextOptions<CountryCosmosContext>>(sp =>
                                                              {
                                                                  var cs = configuration.GetValue<string>("CosmosDb:ConnectionString");
                                                                  var dn = configuration.GetValue<string>("CosmosDb:DatabaseName");
                                                                  var dbcob = new DbContextOptionsBuilder<CountryCosmosContext>();
                                                                  return dbcob.UseCosmos(cs, dn).Options;
                                                              });
    builder.Services.AddDbContext<ICountryContext, CountryCosmosContext>();
}

builder.Services.AddScoped<CountryRepository>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "RestCountries.API v1.0"));
}

app.UseCors(p =>
            {
                p.AllowAnyOrigin();
                p.WithMethods("GET");
                p.AllowAnyHeader();
            });

app.UseHsts();

app.UseHttpsRedirection();

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

app.MapGet("countries/lang/{lang:alpha}",
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

internal partial class Program
{
}
