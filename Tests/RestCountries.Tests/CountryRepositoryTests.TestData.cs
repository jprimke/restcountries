namespace RestCountries.Tests;

public partial class CountryRepositoryTests
{
     private const string TestData = @"
      [
  {
    ""name"": ""Argentina"",
    ""topLevelDomain"": [
      "".ar""
    ],
    ""alpha2Code"": ""AR"",
    ""alpha3Code"": ""ARG"",
    ""callingCodes"": [
      ""54""
    ],
    ""capital"": ""Buenos Aires"",
    ""altSpellings"": [
      ""AR"",
      ""Argentine Republic"",
      ""República Argentina""
    ],
    ""subRegion"": ""South America"",
    ""region"": ""Americas"",
    ""population"": 45376763,
    ""latlng"": [
      -34,
      -64
    ],
    ""demonym"": ""Argentinean"",
    ""area"": 2780400,
    ""timezones"": [
      ""UTC-03:00""
    ],
    ""borders"": [
      ""BOL"",
      ""BRA"",
      ""CHL"",
      ""PRY"",
      ""URY""
    ],
    ""nativeName"": ""Argentina"",
    ""numericCode"": ""032"",
    ""currencies"": [
      {
        ""code"": ""ARS"",
        ""name"": ""Argentine peso"",
        ""symbol"": ""$""
      }
    ],
    ""languages"": [
      {
        ""iso639_1"": ""es"",
        ""iso639_2"": ""spa"",
        ""name"": ""Spanish"",
        ""nativeName"": ""Español""
      },
      {
    ""iso639_1"": ""gn"",
        ""iso639_2"": ""grn"",
        ""name"": ""Guaraní"",
        ""nativeName"": ""Avañe'ẽ""
      }
    ],
    ""translations"": {
    ""br"": ""Argentina"",
      ""pt"": ""Argentina"",
      ""nl"": ""Argentinië"",
      ""hr"": ""Argentina"",
      ""fa"": ""آرژانتین"",
      ""de"": ""Argentinien"",
      ""es"": ""Argentina"",
      ""fr"": ""Argentine"",
      ""ja"": ""アルゼンチン"",
      ""it"": ""Argentina"",
      ""hu"": ""Argentína""
    },
    ""flags"": {
    ""svg"": ""https://upload.wikimedia.org/wikipedia/commons/1/1a/Flag_of_Argentina.svg"",
      ""png"": ""https://upload.wikimedia.org/wikipedia/commons/thumb/1/1a/Flag_of_Argentina.svg/320px-Flag_of_Argentina.svg.png""
    },
    ""flag"": ""https://upload.wikimedia.org/wikipedia/commons/1/1a/Flag_of_Argentina.svg"",
    ""cioc"": ""ARG"",
    ""regionalBlocs"": [
      {
        ""acronym"": ""USAN"",
        ""name"": ""Union of South American Nations""
      }
    ],
    ""independent"": true
  },
  {
    ""name"": ""China"",
    ""topLevelDomain"": [
      "".cn""
    ],
    ""alpha2Code"": ""CN"",
    ""alpha3Code"": ""CHN"",
    ""callingCodes"": [
      ""86""
    ],
    ""capital"": ""Beijing"",
    ""altSpellings"": [
      ""CN"",
      ""Zhōngguó"",
      ""Zhongguo"",
      ""Zhonghua"",
      ""People's Republic of China"",
      ""中华人民共和国"",
      ""Zhōnghuá Rénmín Gònghéguó""
    ],
    ""subRegion"": ""Eastern Asia"",
    ""region"": ""Asia"",
    ""population"": 1402112000,
    ""latlng"": [
      35,
      105
    ],
    ""demonym"": ""Chinese"",
    ""area"": 9640011,
    ""timezones"": [
      ""UTC+08:00""
    ],
    ""borders"": [
      ""AFG"",
      ""BTN"",
      ""MMR"",
      ""HKG"",
      ""IND"",
      ""KAZ"",
      ""PRK"",
      ""KGZ"",
      ""LAO"",
      ""MAC"",
      ""MNG"",
      ""PAK"",
      ""RUS"",
      ""TJK"",
      ""VNM"",
      ""NPL""
    ],
    ""nativeName"": ""中国"",
    ""numericCode"": ""156"",
    ""currencies"": [
      {
        ""code"": ""CNY"",
        ""name"": ""Chinese yuan"",
        ""symbol"": ""¥""
      }
    ],
    ""languages"": [
      {
        ""iso639_1"": ""zh"",
        ""iso639_2"": ""zho"",
        ""name"": ""Chinese"",
        ""nativeName"": ""中文 (Zhōngwén)""
      }
    ],
    ""translations"": {
        ""br"": ""China"",
      ""pt"": ""China"",
      ""nl"": ""China"",
      ""hr"": ""Kina"",
      ""fa"": ""چین"",
      ""de"": ""China"",
      ""es"": ""China"",
      ""fr"": ""Chine"",
      ""ja"": ""中国"",
      ""it"": ""Cina"",
      ""hu"": ""Kína""
    },
    ""flags"": {
        ""svg"": ""https://upload.wikimedia.org/wikipedia/commons/f/fa/Flag_of_the_People%27s_Republic_of_China.svg"",
      ""png"": ""https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Flag_of_the_People%27s_Republic_of_China.svg/320px-Flag_of_the_People%27s_Republic_of_China.svg.png""
    },
    ""flag"": ""https://upload.wikimedia.org/wikipedia/commons/f/fa/Flag_of_the_People%27s_Republic_of_China.svg"",
    ""cioc"": ""CHN"",
    ""regionalBlocs"": [],
    ""independent"": true
  },
  {
    ""name"": ""Germany"",
    ""topLevelDomain"": [
      "".de""
    ],
    ""alpha2Code"": ""DE"",
    ""alpha3Code"": ""DEU"",
    ""callingCodes"": [
      ""49""
    ],
    ""capital"": ""Berlin"",
    ""altSpellings"": [
      ""DE"",
      ""Federal Republic of Germany"",
      ""Bundesrepublik Deutschland""
    ],
    ""subRegion"": ""Central Europe"",
    ""region"": ""Europe"",
    ""population"": 83240525,
    ""latlng"": [
      51,
      9
    ],
    ""demonym"": ""German"",
    ""area"": 357114,
    ""timezones"": [
      ""UTC+01:00""
    ],
    ""borders"": [
      ""AUT"",
      ""BEL"",
      ""CZE"",
      ""DNK"",
      ""FRA"",
      ""LUX"",
      ""NLD"",
      ""POL"",
      ""CHE""
    ],
    ""nativeName"": ""Deutschland"",
    ""numericCode"": ""276"",
    ""currencies"": [
      {
        ""code"": ""EUR"",
        ""name"": ""Euro"",
        ""symbol"": ""€""
      }
    ],
    ""languages"": [
      {
        ""iso639_1"": ""de"",
        ""iso639_2"": ""deu"",
        ""name"": ""German"",
        ""nativeName"": ""Deutsch""
      }
    ],
    ""translations"": {
        ""br"": ""Alemanha"",
      ""pt"": ""Alemanha"",
      ""nl"": ""Duitsland"",
      ""hr"": ""Njemačka"",
      ""fa"": ""آلمان"",
      ""de"": ""Deutschland"",
      ""es"": ""Alemania"",
      ""fr"": ""Allemagne"",
      ""ja"": ""ドイツ"",
      ""it"": ""Germania"",
      ""hu"": ""Grúzia""
    },
    ""flags"": {
        ""svg"": ""https://upload.wikimedia.org/wikipedia/commons/b/ba/Flag_of_Germany.svg"",
      ""png"": ""https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Flag_of_Germany.svg/320px-Flag_of_Germany.svg.png""
    },
    ""flag"": ""https://upload.wikimedia.org/wikipedia/commons/b/ba/Flag_of_Germany.svg"",
    ""cioc"": ""GER"",
    ""regionalBlocs"": [
      {
        ""acronym"": ""EU"",
        ""name"": ""European Union""
      }
    ],
    ""independent"": true
  },
  {
    ""name"": ""United States of America"",
    ""topLevelDomain"": [
      "".us""
    ],
    ""alpha2Code"": ""US"",
    ""alpha3Code"": ""USA"",
    ""callingCodes"": [
      ""1""
    ],
    ""capital"": ""Washington, D.C."",
    ""altSpellings"": [
      ""US"",
      ""USA"",
      ""United States of America""
    ],
    ""subRegion"": ""Northern America"",
    ""region"": ""Americas"",
    ""population"": 329484123,
    ""latlng"": [
      38,
      -97
    ],
    ""demonym"": ""American"",
    ""area"": 9629091,
    ""timezones"": [
      ""UTC-12:00"",
      ""UTC-11:00"",
      ""UTC-10:00"",
      ""UTC-09:00"",
      ""UTC-08:00"",
      ""UTC-07:00"",
      ""UTC-06:00"",
      ""UTC-05:00"",
      ""UTC-04:00"",
      ""UTC+10:00"",
      ""UTC+12:00""
    ],
    ""borders"": [
      ""CAN"",
      ""MEX""
    ],
    ""nativeName"": ""United States"",
    ""numericCode"": ""840"",
    ""currencies"": [
      {
        ""code"": ""USD"",
        ""name"": ""United States dollar"",
        ""symbol"": ""$""
      }
    ],
    ""languages"": [
      {
        ""iso639_1"": ""en"",
        ""iso639_2"": ""eng"",
        ""name"": ""English"",
        ""nativeName"": ""English""
      }
    ],
    ""translations"": {
        ""br"": ""Estados Unidos"",
      ""pt"": ""Estados Unidos"",
      ""nl"": ""Verenigde Staten"",
      ""hr"": ""Sjedinjene Američke Države"",
      ""fa"": ""ایالات متحده آمریکا"",
      ""de"": ""Vereinigte Staaten von Amerika"",
      ""es"": ""Estados Unidos"",
      ""fr"": ""États-Unis"",
      ""ja"": ""アメリカ合衆国"",
      ""it"": ""Stati Uniti D'America"",
      ""hu"": ""Amerikai Egyesült Államok""
    },
    ""flags"": {
        ""svg"": ""https://upload.wikimedia.org/wikipedia/commons/a/a4/Flag_of_the_United_States.svg"",
      ""png"": ""https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Flag_of_the_United_States.svg/320px-Flag_of_the_United_States.svg.png""
    },
    ""flag"": ""https://upload.wikimedia.org/wikipedia/commons/a/a4/Flag_of_the_United_States.svg"",
    ""cioc"": ""USA"",
    ""regionalBlocs"": [
      {
        ""acronym"": ""NAFTA"",
        ""name"": ""North American Free Trade Agreement""
      }
    ],
    ""independent"": true
  }
]
    ";
}
