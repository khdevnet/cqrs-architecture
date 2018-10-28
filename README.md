# Star Wars shop - Checkout component

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
* SW.Store.Checkout.sln
* SW.Checkout.Message.Handler.sln
* SW.Store.Checkout.Client.sln

4. Database replication
* Stop SW.Checkout.Message.Handler
* Update destination database connection strings in SW.Store.Checkout.StorageReplication\Replica ConnectionStringsProviders
* Run SW.Store.Checkout.StorageReplication.sln
* Restore database to specific datetime
```
dotnet SW.Store.Checkout.StorageReplication.dll "2018-10-27 16:46:54.762238+00"
```

## Resources
* [Domain information](https://github.com/khdevnet/sw-checkout/wiki/Domain-information)
* [Architecture Vision](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision)
* [Frameworks & Architecture Styles](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision#frameworks--architecture-styles)
* [RabbitMQ](https://github.com/khdevnet/sw-checkout/blob/master/RabbitMQ.md)
* [Distributed Systems](https://github.com/khdevnet/sw-checkout/blob/master/DistributedSystemsDocs)
