param location string = resourceGroup().location
param webAppName string = 'ecommerce-api-${uniqueString(resourceGroup().id)}'

// 1. Definicja planu hostingu (Darmowa warstwa F1 na Linuxie)
resource appServicePlan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: '${webAppName}-plan'
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
  kind: 'linux'
  properties: {
    reserved: true // Wymagane dla Linuxa
  }
}

// 2. Definicja samej aplikacji Web API
resource webApp 'Microsoft.Web/sites@2022-09-01' = {
  name: webAppName
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNET|8.0' // Wersja .NET Core
    }
  }
}

output generatedWebAppName string = webApp.name
