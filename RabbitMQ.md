1. install RabbitMQ via docker
```
docker run -d --hostname sw-rabbit-host --name sw-rabbit -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```
2. Enter Managment Admin
   http://localhost:8080
   username/passsword: guest/guest



Terms
A producer is a user application that sends messages.
A queue is a buffer that stores messages.
A consumer is a user application that receives messages.
message broker: it accepts and forwards messages. 
Exchange: the producer can only send messages to an exchange. An exchange is a very simple thing. On one side it receives messages from producers and the other side it pushes them to queues. (direct, topic, headers and fanout)


Best practice:
1. Manual message acknowledgments are turned on by default. In previous examples we explicitly turned them off by setting the autoAck ("automatic acknowledgement mode") parameter to true. It's time to remove this flag and manually send a proper acknowledgment from the worker, once we're done with a task.

Make autoAck: false

```
channel.BasicConsume(queue: "task_queue", autoAck: false, consumer: consumer);'
```

2. Message durability
Two things are required to make sure that messages aren't lost: we need to mark both the queue and messages as durable.
We need to mark Producer and Consumer.
```
channel.QueueDeclare(queue: "hello",
```

Now we need to mark our messages as persistent - by setting IBasicProperties.SetPersistent to true
var properties = channel.CreateBasicProperties();
properties.Persistent = true;
The persistence guarantees aren't strong, but it's more than enough for our simple task queue. 
If you need a stronger guarantee then you can use [publisher confirms](https://www.rabbitmq.com/confirms.html).

3. Fair dispatch
You might have noticed that the dispatching still doesn't work exactly as we want. For example in a situation with two workers, when all odd messages are heavy and even messages are light, one worker will be constantly busy and the other one will do hardly any work. Well, RabbitMQ doesn't know anything about that and will still dispatch messages evenly.
In order to change this behavior we can use the BasicQos method with the prefetchCount = 1 setting.
This tells RabbitMQ not to give more than one message to a worker at a time. 
```
channel.BasicQos(0, 1, false);
```


Resources
[RabbitMQ Second tutorial](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)
