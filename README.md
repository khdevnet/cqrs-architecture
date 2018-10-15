# Star Wars store checkout domain area

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
* [Domain information](https://github.com/khdevnet/sw-checkout/wiki/Domain-information)
* [Architecture Vision](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision)
* [Frameworks & Architecture Styles](https://github.com/khdevnet/sw-checkout/wiki/Architecture-Vision#frameworks--architecture-styles)
* [RabbitMQ](https://github.com/khdevnet/sw-checkout/blob/master/RabbitMQ.md)
* [Distributed Systems](https://github.com/khdevnet/sw-checkout/blob/master/DistributedSystemsDocs)
