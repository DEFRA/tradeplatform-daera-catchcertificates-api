# Setup

To run this webapp, you will need a `./src/Defra.Trade.CatchCertificates.Api/appsettings.Development.json` file. The file will need the following settings:

```jsonc 
{
  "ConnectionStrings": {
    "sql_db": "<secret>",
    "sql_db_audit": "<secret>"
  },
  "APPINSIGHTS_INSTRUMENTATIONKEY": "000000000000000000000",
  "CommonSql": {
    "UseAzureAccessToken": false
  },
  "CommonAuditSql": {
    "UseAzureAccessToken": false
  },
  "SocSettings": {
    "EventHubName": "<secret> ",
    "EventHubConnectionString": "<secret>"
  },
  "ProtectiveMonitoringSettings": {
    "Application": "TradeApi",
    "Component": "TRADE.API",
    "Enabled": false,
    "LogToAppInsights": true,
    "LogToSoc": true
  }
}
```

Secrets reference can be found here: https://dev.azure.com/defragovuk/DEFRA-TRADE-APIS/_wiki/wikis/DEFRA-TRADE-APIS.wiki/26086
