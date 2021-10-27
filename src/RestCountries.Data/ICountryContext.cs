//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Data\CountryRepository.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using RestCountries.Data.Models;

namespace RestCountries.Data;

public interface ICountryContext
{
    public IQueryable<CountryInfo> Countries { get; }
}
