param location string = 'westeurope'
param webAppName string = 'ecommerce-api-${uniqueString(resourceGroup().id)}'

// 1. Definicja planu hostingu (serwera) - darmowa warstwa F1
resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'ecommerce-plan'
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
  kind: 'linux'
  properties: {
    reserved: true // Wymagane dla systemu Linux
  }
}

// 2. Definicja właściwej aplikacji Web API
resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNET|8.0' // Wskazujemy na środowisko .NET 8
    }
  }
}

output deployedWebAppUrL string = webApp.properties.defaultHostName
