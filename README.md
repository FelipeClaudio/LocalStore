# LocalStore

### Project created to study DDD concepts and EFcore

### Domain
TODO

### How to Run:

* It is necessary to create and user with username and password: localstoreuser. 
* The migration must be executed for each context. The startup project must be set to "LocalStore.Infrastructure":
  * dotnet ef database update --project .\LocalStore.Infrastructure\LocalStore.Infrastructure.csproj --context LocalStore.Infrastructure.Database.Products.ProductContext
  * dotnet ef database update --project .\LocalStore.Infrastructure\LocalStore.Infrastructure.csproj --context LocalStore.Infrastructure.Database.Orders.OrderContext
* When the "TestApplication" project runs, the database is fullfiled with mocked data.
