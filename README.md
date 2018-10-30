# Star Wars shop - Checkout

**Checkout** as a standalone (micro)service, which contains web api and process checkout requests.
It used for customer checkout purposes, clients can create order and chek it status.

## Tech Stack
* [Asp.Net Core 2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1)
* [EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/)
* [Marten](http://jasperfx.github.io/marten/)
* [PostgreSQL](https://www.postgresql.org/)
* [Docker](https://www.docker.com/)

## Solution Structure

```
.
├── docs                                           # Contains documentation files
├── SW.Checkout.Client                       # It is client project, it simulate real clients, send http requests to API
├── SW.Checkout.Domain                       # Contains business logic
├── SW.Checkout.Infrastructure.EventStore    # Contains Marten EventStore contracts implementations
├── SW.Checkout.Infrastructure.RabbitMQ      # Contains RabbitMQ Message Bus contracts implementations
├── SW.Checkout.Infrastructure.ReadStorage   # Contains Read Storage contracts implementations
├── SW.Checkout.Message.Handler              # Contains Message processing console application implementations
├── SW.Checkout.Read                         # Contains Read contracts
├── SW.Checkout.StorageReplication           # Contains Database replication console application
├── SW.Checkout.WebApi                       # Contains Api contracts and enpoinds implementation
├── SW.Checkout.Core                                  # Contains common functionality of the system, contracts
```

## Start Project
1. Configure docker RabbitMQ
```
$ docker run -d --hostname sw-rabbit-host --name sw-rabbit -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```
2. Configure PostgreSQL
```
$ docker run --name event-store-postgres -e POSTGRES_PASSWORD=123456 -d -p 5432:5432 postgres
```
3. Run Application
* SW.Checkout.sln
* SW.Checkout.Message.Handler.sln
* SW.Checkout.Client.sln

4. Database replication
* Stop SW.Checkout.Message.Handler
* Update destination database connection strings in SW.Checkout.StorageReplication\Replica ConnectionStringsProviders
* Run SW.Checkout.StorageReplication.sln

5. Database replication to specific Datetime

```
$ dotnet SW.Checkout.StorageReplication.dll "2018-10-27 16:46:54.762238+00"
```

## Documentation
* [Domain information](https://github.com/khdevnet/sw-checkout/wiki/Domain-information)
* [Architecture Vision](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision)
* [Frameworks & Architecture Styles](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision#frameworks--architecture-styles)

## Resources
* [RabbitMQ](https://github.com/khdevnet/distributed-systems/blob/master/rabbitmq-practices.md)
* [Distributed Systems](https://github.com/khdevnet/distributed-systems/blob/master/distributed-systems.md)

## Navigation
* [Home](https://github.com/khdevnet/sw)
* [Goals](https://github.com/khdevnet/sw/blob/master/README.md#goals)
* [Features](https://github.com/khdevnet/sw#features)
* [Development](https://github.com/khdevnet/sw/blob/master/README.md#development)
* [User Documentation](https://github.com/khdevnet/sw/blob/master/README.md#user-documentation)

