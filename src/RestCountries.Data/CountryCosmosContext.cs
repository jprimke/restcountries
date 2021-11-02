//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\restcountries\src\RestCountries.Data\CountryCosmosContext.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2021 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos.Infrastructure.Internal;
using RestCountries.Data.Dbos;
using RestCountries.Data.Models;

namespace RestCountries.Data
{
    public class CountryCosmosContext : DbContext, ICountryContext
    {
        private readonly string dbName = string.Empty;
        private string containerName = string.Empty;

        public DbSet<CountryInfoDbo>? CountriesSet { get; set; }   // Can't use because EF Core for CosmosDB canot handle dictionaries

        private List<CountryInfo> countries = new();

        public IQueryable<CountryInfo> Countries
        {
            get
            {
                if (!countries.Any())
                {
                    var client = Database.GetCosmosClient();
                    var database = client.GetDatabase(dbName);
                    var entityType = Model.FindEntityType(typeof(CountryInfoDbo).FullName!);
                    containerName = entityType?.GetContainer() ?? string.Empty;
                    var container = database.GetContainer(containerName);

                    countries = container.GetItemLinqQueryable<CountryInfo>(allowSynchronousQueryExecution: true).ToList();
                }

                return countries.AsQueryable();
            }
        }

        public CountryCosmosContext(DbContextOptions<CountryCosmosContext> options)
            : base(options)
        {
            var ext = options.FindExtension<CosmosOptionsExtension>();
            dbName = ext?.DatabaseName ?? string.Empty;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer(nameof(Countries));

            modelBuilder.Entity<CountryInfoDbo>(e =>
                                                  {
                                                      e.ToContainer(nameof(Countries));
                                                      e.HasKey(x => x.Id);
                                                      e.HasPartitionKey(x => x.Name);
                                                      e.HasNoDiscriminator();
                                                      e.OwnsMany(x => x.Currencies);
                                                      e.OwnsMany(x => x.Languages);
                                                      e.OwnsMany(x => x.RegionalBlocs);
                                                  });
            base.OnModelCreating(modelBuilder);
        }
    }
}
