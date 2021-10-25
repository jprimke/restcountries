using FluentAssertions;
using Microsoft.Extensions.Logging;
using RestCountries.API.Data;
using Xunit;

namespace RestCountries.API.Tests
{
    public class CountryRepositoryTests
    {
        private readonly CountryRepository sut = null!;

        public CountryRepositoryTests()
        {
            var factory = new LoggerFactory();
            var logger = factory.CreateLogger<CountryRepository>();
            sut = new CountryRepository(logger, @"resources\allcountries.json");
        }

        [Fact]
        public void GetAll_ShouldHave_Count_250()
        {
            var all = sut.GetAll();

            all.Should().NotBeNullOrEmpty();
            all.Should().HaveCount(250);
        }


        [Fact]
        public void GetByNamePart_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByName("united", false);

            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByNamePart_Should_Have_All_Entries()
        {
            var countries = sut.GetCountriesByName("", false);

            countries.Should().NotBeNull();
            countries.Should().HaveCount(250);
        }

        [Fact]
        public void GetByFullName_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByName("Germany", true);

            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByFullNativeName_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByName("Deutschland", true);

            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByFullNativeName_Should_Have_Least_No_Entry()
        {
            var countries = sut.GetCountriesByName("", true);

            countries.Should().NotBeNull();
            countries.Should().HaveCount(0);
        }

        [Theory]
        [InlineData("de", 1)]
        [InlineData("deu", 1)]
        [InlineData("d", 0)]
        [InlineData("deut", 0)]
        public void GetByAlphaCode_Should_Have_count_Entries(string alphaCode, int count)
        {
            var countries = sut.GetCountriesByAlphaCode(alphaCode);

            countries.Should().NotBeNull();
            countries.Should().HaveCount(count);
        }

        [Theory]
        [InlineData("de;USA;nl", 3)]
        [InlineData("deu", 1)]
        [InlineData("d;us;pt", 2)]
        [InlineData("deut", 0)]
        public void GetByAlphaCodes_Should_Have_count_Entries(string alphaCode, int count)
        {
            var splitCodes = alphaCode.Split(';', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries);
            var countries = sut.GetCountriesByAlphaCodes(splitCodes);

            countries.Should().NotBeNull();
            countries.Should().HaveCount(count);
        }

        [Fact]
        public void GetByRegion_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByRegion("Europe");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetBySubRegion_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesBySubRegion("Central Europe");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByCurrencyName_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByCurrency("Euro");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByCurrencyCode_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByCurrency("EUR");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByCallingCode_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByCallingCode("44");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByCapital_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByCapital("Berlin");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByRegionalBloc_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByRegionalBloc("EU");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByTopLevelDomain_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByTopLevelDomain("de");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByTopLevelDomainWithPoint_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByTopLevelDomain(".de");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByCioc_Should_Have_Least_One_Entry()
        {
            var countries = sut.GetCountriesByCioc("GER");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByLanguageIso639_1_Should_Have_least_One_Entry()
        {
            var countries = sut.GetCountriesByLanguage("en");
            countries.Should().NotBeNullOrEmpty(); 
        }

        [Fact]
        public void GetByLanguageIso639_2_Should_Have_least_One_Entry()
        {
            var countries = sut.GetCountriesByLanguage("deu");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByLanguageName_Should_Have_least_One_Entry()
        {
            var countries = sut.GetCountriesByLanguage("English");
            countries.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetByLanguageNative_Should_Have_least_One_Entry()
        {
            var countries = sut.GetCountriesByLanguage("Deutsch");
            countries.Should().NotBeNullOrEmpty();
        }
    }
}
