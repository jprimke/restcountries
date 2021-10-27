//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Mosaic\Countries\Countries.Client\CountryInfo.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2020 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace RestCountries.Data.Models;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public class CountryInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("topLevelDomain")]
    public List<string> TopLevelDomain { get; set; } = new List<string>();

    [JsonPropertyName("alpha2Code")]
    public string Alpha2Code { get; set; } = string.Empty;

    [JsonPropertyName("alpha3Code")]
    public string Alpha3Code { get; set; } = string.Empty;

    [JsonPropertyName("callingCodes")]
    public List<string> CallingCodes { get; set; } = new List<string>();

    [JsonPropertyName("capital")]
    public string Capital { get; set; } = string.Empty;

    [JsonPropertyName("altSpellings")]
    public List<string> AltSpellings { get; set; } = new List<string>();

    [JsonPropertyName("subregion")]
    public string SubRegion { get; set; } = string.Empty;

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("population")]
    public int Population { get; set; }

    [JsonPropertyName("latlng")]
    public List<double> LatLng { get; set; } = new List<double>();

    [JsonPropertyName("demonym")]
    public string Demonym { get; set; } = string.Empty;

    [JsonPropertyName("area")]
    public double? Area { get; set; }

    [JsonPropertyName("timezones")]
    public List<string> TimeZones { get; set; } = new List<string>();

    [JsonPropertyName("borders")]
    public List<string> Borders { get; set; } = new List<string>();

    [JsonPropertyName("nativeName")]
    public string NativeName { get; set; } = string.Empty;

    [JsonPropertyName("numericCode")]
    public string NumericCode { get; set; } = string.Empty;

    [JsonPropertyName("currencies")]
    public List<CurrencyInfo> Currencies { get; set; } = new List<CurrencyInfo>();

    [JsonPropertyName("languages")]
    public List<LanguageInfo> Languages { get; set; } = new List<LanguageInfo>();

    [JsonPropertyName("translations")]
    public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("flags")]
    public Dictionary<string, string> Flags { get; set; } = new();

    [JsonPropertyName("flag")]
    public string Flag { get; set; } = string.Empty;

    [JsonPropertyName("cioc")]
    public string Cioc { get; set; } = string.Empty;

    [JsonPropertyName("regionalBlocs")]
    public List<BlocInfo> RegionalBlocs { get; set; } = new List<BlocInfo>();

    [JsonPropertyName("independent")]
    public bool Independent { get; set; }
}
