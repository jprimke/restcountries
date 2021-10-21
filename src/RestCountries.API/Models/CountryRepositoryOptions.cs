//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.API\Models\CountryRepositoryOptions.cs" company="AXA Partners">
//     Author: Jörg H Primke
//     Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace RestCountries.API.Models;

    public class CountryRepositoryOptions
    {
        public string Directory { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;
    }
