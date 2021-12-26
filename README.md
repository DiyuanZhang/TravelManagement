# TravelManagement
提供企业差旅管理服务

# Run The Code
## How to run database migration
```
Create a new database named TravelManagement-local
cd .\TravelManagement.Migration\
dotnet build
dotnet run "{DBConnectionString}"
```

## How to run application
```
dotnet restore
set value for the DBConnectionString & ServiceBusConnectionString & ServiceBusQueue & ApprovalSystemUrl & PaymentSystemUrl in appsettings.json
dotnet run --project .\TravelManagement\
```
visit https://localhost:5001/swagger/index.html to view all apis.


## How to run tests
```
cd .\TravelManagement.Test
dotnet restore
dotnet test
```