# 学习笔记——关于高并发处理Concurrency
> 这里阐述的并发是一个比较宏观的概念，指的是“同时做多件事”。

> 引用文章地址：https://www.cnblogs.com/atree/p/Concurrency_Async.html
## 异步编程
异步编程就是使用future模式（又称promise）或者回调机制来实现（Non-blocking on waiting）。如果使用回调或事件来实现（容易callback hell），不仅编写这样的代码不直观，很快就容易把代码搞得一团糟。不过在.NET 4.5 及以上框架中引入的async/await关键字（在.NET 4.0中通过添加Microsoft.Bcl.Async包也可以使用），让编写异步代码变得容易和优雅。通过使用async/await关键字，可以像写同步代码那样编写异步代码，所有的回调和事件处理都交给编译器和运行时帮你处理了，简单好用。使用异步编程有两个好处：不阻塞主线程（比如UI线程），提高服务端应用的吞吐量。所以微软推荐ASP.NET中默认使用异步来处理请求。

**Keyword:** *async/await*

## 并行编程
并行编程的出现实际上是随着CPU有多核而兴起的，目的是充分利用多核CPU的计算能力。并行编程由于会提高CPU的利用率，更适合客户端的一些应用，对于服务端的应用可能会造成负面影响（因为服务器本身就具有并行处理的特点，比如IIS会并行的处理多个请求）。我自己使用并行编程最多的场景是之前分析环境数据不确定度的时候，使用并行的方式计算蒙特卡洛模拟（计算上千次之后拟合），当然后来我使用泰勒级数展开来计算不确定度，没有这么多的计算量就无需并行了。当然在计算多方案结果比较的情况下，还是继续使用了并发计算。在.NET中，并行的支持主要靠.NET 4.0引入的任务并行库和并行LINQ。通过这些库可以实现数据并行处理（处理方式相同，输入数据不同，比如我上面提到的应用场景）或者任务并行处理（处理方式不同，且数据隔离）。通过使用并行处理库，你不用关心Task的创建和管理（当然更不用说底层的线程了），只需要关注处理任务本身就行了。

**Keyword:** *Parallel*

## 响应式编程
响应式编程最近成为了一个Buzzword，其实微软6年前就开始给.NET提供一个Reactive Extensions了。一开始要理解响应式编程有点困难，但是一旦理解了，你就会对它的强大功能爱不释手。简单来说，响应式编程把事件流看作数据流，不过数据流是从IEnumable中拉取的，而事件流是从IObservable推送给你的。为什么响应式编程可以实现并发呢？这是因为Rx做到线程不可知，每次事件触发，后续的处理会从线程池中任意取出一个线程来处理。且可以对事件设置窗口期和限流。举个例子，你可以用Rx来让搜索文本框进行延迟处理（而不用类似我很早的时候用个定时器来延迟了）。
> Rx.net微软官方文档 https://docs.microsoft.com/zh-cn/previous-versions/dotnet/reactive-extensions/hh242975(v%3dvs.103)

> Rx官网 http://reactivex.io/

**Keyword:** *ReactiveExtentions/System.Reactive/响应式/可观察集合*

## 数据流编程
数据流（DataFlow）编程可能大家就更陌生了，不过还是有些常用场景可以使用数据流来解决。数据流其实是在任务并行库（TPL）上衍生出来的一套处理数据的扩展（也结合了异步的特性），TPL也是处理并行编程中任务并行和数据并行的基础库。望文生义，TPL DataFlow就是对数据进行一连串处理，首先为这样的处理定义一套网格（mesh），网格中可以定义分叉（fork）、连接（join）、循环（loop）。数据流入这样的处理网格就能够并行的被处理。你可以认为网格是一种升级版的管道，实际上很多时候就是被当作管道来使用。使用场景可以是“分析文本文件中词频”，也可以是“处理生产者/消费者问题”。
> TPLDataFlow官方文档 https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library
**Keyword:** *TPL DataFlow/数据流*

## Actor模型
Scala有Akka，其实微软研究院也推出了Orleans来支持了Actor模型的实现，当然也有Akka.NET可用。Orleans设计的目标是为了方便程序员开发需要大规模扩展的云服务, 可用于实现DDD+EventSourcing/CQRS系统。
> Akka.net http://getakka.net/articles/intro/what-are-actors.html

**Keyword:** *Orleans/Akka.net*

