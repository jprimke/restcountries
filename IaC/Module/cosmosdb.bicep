param location string
param tags object
param name string

var databaseName = 'CountryDB'
var containerName = 'Countries'

resource cosmosDb 'Microsoft.DocumentDB/databaseAccounts@2021-06-15' = {
  name: name
  location: location
  kind: 'GlobalDocumentDB'
  properties: {
    databaseAccountOfferType: 'Standard'
    locations: [
      {
        isZoneRedundant: false
        failoverPriority: 0
        locationName: 'West Europe'
      }
    ]
    backupPolicy: {
       type: 'Periodic'
       periodicModeProperties: {
         backupIntervalInMinutes: 240
         backupRetentionIntervalInHours: 8
       }
    }
    enableFreeTier: false
  }
  tags: tags

  resource countryDb 'sqlDatabases@2021-06-15' = {
    name: databaseName
    properties: {
      resource: {
        id: databaseName
      }
    }

    resource countries 'containers@2021-06-15' = {
      name: containerName
      properties: {
        resource: {
          id: containerName
          partitionKey: {
            paths: [
              '/name'
            ]
            kind: 'Hash'
          }
        }
      }
    }
  }
}

output connectionstring string = last(cosmosDb.listConnectionStrings().connectionStrings).connectionString
output databaseName string = databaseName
output containerName string = containerName
