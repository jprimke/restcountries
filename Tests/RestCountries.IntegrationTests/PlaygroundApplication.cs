//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\Tests\RestCountries.Tests\PlaygroundApplication.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace RestCountries.IntegrationTests;

internal class PlaygroundApplication : WebApplicationFactory<Program>
{
    private readonly string environment;

    public PlaygroundApplication(bool useFile = true, string environment = "Development")
    {
        this.environment = environment;
        Environment.SetEnvironmentVariable("UseJsonFile", useFile.ToString().ToLower(), EnvironmentVariableTarget.Process);
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(environment);

        return base.CreateHost(builder);
    }
}
