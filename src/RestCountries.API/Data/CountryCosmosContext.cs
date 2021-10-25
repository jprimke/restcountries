using Microsoft.EntityFrameworkCore;
using RestCountries.API.Models;

namespace RestCountries.API.Data
{

    public class CountryCosmosContext : DbContext, ICountryContext
    {
        public DbSet<CountryInfo> CountriesSet => Set<CountryInfo>();

        public IEnumerable<CountryInfo> Countries => CountriesSet;

        public CountryCosmosContext(DbContextOptions<CountryCosmosContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Countries");

            modelBuilder.Entity<CountryInfo>(e =>
                                             { 
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
