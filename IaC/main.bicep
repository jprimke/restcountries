param location string = resourceGroup().location

var appname = 'restcountries'

var tags = {
  'Creator': 'Joerg Primke'
  'Country': 'Germany'
  'Company': 'AXA Partners'
  'Department': 'IT'
  'Project': 'restcountries'
}

resource appinsight 'Microsoft.Insights/components@2020-02-02' = {
  name: appname
  location: location
  kind: 'web'
  tags: tags
  properties: {
    Application_Type: 'web'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

module cosmos 'Module/cosmosdb.bicep' = {
  name: 'cosmos'
  params: {
    location: location
    name: appname
    tags: tags
  }
}

var cosmosConnectionString = cosmos.outputs.connectionstring
var cosmosDatabaseName = cosmos.outputs.databaseName
var cosmosContainerName = cosmos.outputs.containerName

resource asp 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: 'ASP-restcountries-b6e8'
  location: location
  kind: 'linux'
  sku: {
    name: 'P1v2'
    tier: 'PremiumV2'
  }
  tags: tags
}

resource app 'Microsoft.Web/sites@2021-02-01' = {
  name: appname
  location: location
  tags: tags
  kind: 'app,linux'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: asp.id
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|6.0'
      minimumElasticInstanceCount: 1
      minTlsVersion: '1.2'
      appSettings: [
        {
          name: 'ResourceDirectory'
          value: 'Resources'
        }
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appinsight.properties.InstrumentationKey
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'DiagnosticServices_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: 'InstrumentationKey=${appinsight.properties.InstrumentationKey};IngestionEndpoint=https://germanywestcentral-1.in.applicationinsights.azure.com/'
        }
        {
          name: 'XDT_MicrosoftApplicationInsights_Mode'
          value: 'recommended'
        }
        {
          name: 'CosmosDb:ConnectionString'
          value: cosmosConnectionString
        }
        {
          name: 'CosmosDb:DatabaseName'
          value: cosmosDatabaseName
        }
        {
          name: 'CosmosDb:ContainerName'
          value: cosmosContainerName
        }
      ]    
      netFrameworkVersion: 'v6.0'
      ftpsState: 'Disabled'
    }
    httpsOnly: true    
  }

  resource slot 'slots@2021-02-01' = {
    name: 'staging'    
    location: location
    properties: {
      enabled: true
    }
    tags: tags
  }
}
