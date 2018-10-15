# Star Wars store checkout domain area 
# Bussines information
  * [Checkout Flows](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision)
  * [Domain Entities](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision)

# Development
### Frameworks & Architecture Styles
  * [CQRS Architecture](https://martinfowler.com/bliki/CQRS.html)
  * [Onion Architecture](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/)
  * [Docker](https://www.docker.com/)
  
### Application Architecture
  * [High Level View](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision)

## Start Project
1. Configure docker RabbitMQ
```
docker run -d --hostname sw-rabbit-host --name sw-rabbit -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```
2. Run Solutions
* SW.Store.Checkout.sln
* SW.Checkout.OrderHandler.Application.sln
* SW.Store.Checkout.Client.sln

## Resources
* [RabbitMQ](https://github.com/khdevnet/sw-checkout/blob/master/RabbitMQ.md)
* [Distributed Systems](https://github.com/khdevnet/sw-checkout/blob/master/DistributedSystemsDocs)
