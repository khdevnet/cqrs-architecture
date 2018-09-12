# cqrs-architecture

## Distributed computing rules

### CAP Theorem

1. Consistency: Every read receives the most recent write or an error
2. Availability: Every request receives a (non-error) response – without guarantee that it contains the most recent write
3. Partition tolerance: The system continues to operate despite an arbitrary number of messages being dropped (or delayed) by the network between nodes

### The 8 fallacies of distributed computing are becoming irrelevant
1. The network is reliable
The first fallacy is an easy way to set yourself up for failure, as Murphy made sure there will always be things that go wrong with the network—whether it is power failure or a cut cable. However, Active Transactional Data Replication ensures that should a single server or an entire data center go offline, the information you need will still be available, as each data note is continuously synchronised without geographical constraints.

2. Latency is zero
Latency is how much time it takes for data to move from one place to another (versus bandwidth, which is how much data can be transferred during that time). In the past, latency has deteriorated quickly when you move to WAN or internet scenarios. As more enterprises look to migrate operations to the cloud or move to a hybrid cloud structure, network latency can lead to an inability to back up data and restore it at speed in any given situation, including disaster recovery. Active Transactional Data Replication technology can now ensure transactional data can be moved to the cloud at petabyte scale with no downtime and no data loss, making it possible for on-premise and cloud environments to operate as one.

3. Bandwidth is infinite
Bandwidth is the capacity of a network to transfer data. Even though network bandwidth capacity has been improving the amount of information we want to transfer, it is also increasing exponentially. But there are ways to increase bandwidth. Again data replication technology allows you to have multiple databases, meaning your system isn’t dependent on any one thing and you have more bandwidth at your disposal that can be controlled using network traffic shaping capabilities. This gives administrators the power to prioritize network traffic on the basis of source and target data centers and the ability to assign higher priority to specific files and directories during replication between data centers.

4. The network is secure
The only completely secure system is one that is not connected to any network. Nevertheless, security is increasing all the time, as companies can deploy software solutions to ensure only external servers are exposed through the firewall, thereby reducing an organization’s vulnerability to hackers. 

5. Topology doesn’t change
Topology doesn’t change as long as it stays in the test lab. When applications are deployed into an organization, network topology can quickly become out of control with laptops coming and going, wireless ad hoc networks and new mobile devices. In fact, one of the biggest benefits for organizations moving to the cloud is the ability to change topology at will. As data can now be replicated across different environments with guaranteed consistency, issues around topology are no longer as relevant as they used to be. 

6. There is one administrator
While there is never just one administrator, given your applications are likely interacting with systems outside your administrative control, the impact of this is no longer as critical as it once was. The fact is you care about administrators only when things go awry and you need to pinpoint a problem and solve it. Even then with the same data available across multiple locations, organizations can now carry on with their work with no risk of downtime.

7. Transport cost is zero
While nothing in life is free, the costs of setting and running a WAN network are considerably cheaper than they used to be. Cloud computing means you can purchase what you need per transaction, gigabyte or an hour. Active-active architecture means all servers and clusters are fully readable and writeable, always in sync and recover automatically from each other after planned or unplanned downtime. This means there are no passive read-only backup servers and clusters, so organizations can now use 100 percent of their hardware without wasting budget on idle servers.

8. The network is homogenous
This fallacy was added to the original seven by Gosling, the creator of Java, in 1997. With the advent of mobile, no network today is homogenous, but an increasing number of tools are being built that do let you try out things across different networks and environments. The key point here is to ensure you have no vendor lock-in.

### Durability Guarantees
1. Message is persisted
2. Message is not removed until processed
3. Duplicate messages are recognized
