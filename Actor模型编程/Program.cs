using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actor模型编程
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var system = ActorSystem.Create("MySystem");

            //var firstRef = system.ActorOf(Props.Create<PrintMyActorRefActor>(), "first-actor");
            //Console.WriteLine($"First: {firstRef}");
            //firstRef.Tell("printit", ActorRefs.NoSender);
            Console.WriteLine("-------------------------------------------------------");

            //var first = system.ActorOf(Props.Create<StartStopActor1>(), "first");
            //first.Tell("stop");

            Console.WriteLine("-------------------------------------------------------");
            //var supervisingActor = system.ActorOf(Props.Create<SupervisingActor>(), "supervising-actor");
            //supervisingActor.Tell("failChild");
            Console.WriteLine("-------------------------------------------------------");

            new test2_1().Device_actor_must_reply_with_empty_reading_if_no_temperature_is_known();
            Console.ReadLine();
        }
    }
    #region <1-1>
    public class PrintMyActorRefActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            switch (message as string)
            {
                case "printit":
                    IActorRef secondRef = Context.ActorOf(Props.Empty, "second-actor");
                    Console.WriteLine($"Second: {secondRef}");
                    break;
            }
        }
    }
    #endregion
    #region <1-2>
    public class StartStopActor1 : UntypedActor
    {
        protected override void PreStart()
        {
            Console.WriteLine("first started");
            Context.ActorOf(Props.Create<StartStopActor2>(), "second");
        }

        protected override void PostStop() => Console.WriteLine("first stopped");

        protected override void OnReceive(object message)
        {
            switch (message as string)
            {
                case "stop":
                    Context.Stop(Self);
                    break;
            }
        }
    }
    public class StartStopActor2 : UntypedActor
    {
        protected override void PreStart() => Console.WriteLine("second started");
        protected override void PostStop() => Console.WriteLine("second stopped");

        protected override void OnReceive(object message)
        {
        }
    }
    #endregion
    #region <1-3>
    public class SupervisingActor : UntypedActor
    {
        private IActorRef child = Context.ActorOf(Props.Create<SupervisedActor>(), "supervised-actor");
        protected override void OnReceive(object message)
        {
            switch (message as string)
            {
                case "failChild":
                    child.Tell("fail");
                    break;
            }
        }
    }
    public class SupervisedActor : UntypedActor
    {
        protected override void PreStart() => Console.WriteLine("supervised actor started");
        protected override void PostStop() => Console.WriteLine("supervised actor stopped");
        protected override void OnReceive(object message)
        {
            switch (message as string)
            {
                case "fail":
                    Console.WriteLine("supervised actor fails now");
                    throw new Exception("I failed!");
            }
        }
    }
    #endregion
    #region <1-4>
    public class IotApp
    {
        public static void Init()
        {
            using (var system = ActorSystem.Create("iot-system"))
            {
                // Create top level supervisor
                var supervisor = system.ActorOf(Props.Create<IotSupervisor>(), "iot-supervisor");
                // Exit the system after ENTER is pressed
                Console.ReadLine();
            }
        }
    }
    public class IotSupervisor : UntypedActor
    {
        public ILoggingAdapter Log { get; } = Context.GetLogger();
        protected override void PreStart() => Log.Info("IoT Application started");
        protected override void PostStop() => Log.Info("IoT Application stopped");
        // No need to handle any messages
        protected override void OnReceive(object message)
        {
        }
        public static Props Props() => Akka.Actor.Props.Create<IotSupervisor>();
    }
    #endregion
    #region <2-1>
    public class test2_1
    {
        public void Device_actor_must_reply_with_empty_reading_if_no_temperature_is_known()
        {
            var system = ActorSystem.Create("MySystem");
            var deviceActor = system.ActorOf(Device.Props("group", "device"));
            var showActor = system.ActorOf(Show.Props("group", "show"));

            deviceActor.Tell(new ReadTemperature(requestId: 42), showActor);
            //var response = probe.ExpectMsg<RespondTemperature>();
            //response.RequestId.Should().Be(42);
            //response.Value.Should().BeNull();
        }

    }
    public sealed class ReadTemperature
    {
        public ReadTemperature(long requestId)
        {
            RequestId = requestId;
        }

        public long RequestId { get; }
    }

    public sealed class RespondTemperature
    {
        public RespondTemperature(long requestId, double? value)
        {
            RequestId = requestId;
            Value = value;
        }

        public long RequestId { get; }
        public double? Value { get; }
    }
    public class Device : UntypedActor
    {
        private double? _lastTemperatureReading = null;

        public Device(string groupId, string deviceId)
        {
            GroupId = groupId;
            DeviceId = deviceId;
        }

        protected override void PreStart() => Log.Info($"Device actor {GroupId}-{DeviceId} started");
        protected override void PostStop() => Log.Info($"Device actor {GroupId}-{DeviceId} stopped");

        protected ILoggingAdapter Log { get; } = Context.GetLogger();
        protected string GroupId { get; }
        protected string DeviceId { get; }

        protected override void OnReceive(object message)
        {
            var msg=message as ReadTemperature;
            if(msg.RequestId>20)
                Sender.Tell(new RespondTemperature(msg.RequestId, 200));
        }

        public static Props Props(string groupId, string deviceId) =>
            Akka.Actor.Props.Create(() => new Device(groupId, deviceId));
    }
    public class Show : UntypedActor
    {
        private double? _lastTemperatureReading = null;

        public Show(string groupId, string deviceId)
        {
            GroupId = groupId;
            DeviceId = deviceId;
        }

        protected override void PreStart() => Log.Info($"Device actor {GroupId}-{DeviceId} started");
        protected override void PostStop() => Log.Info($"Device actor {GroupId}-{DeviceId} stopped");

        protected ILoggingAdapter Log { get; } = Context.GetLogger();
        protected string GroupId { get; }
        protected string DeviceId { get; }

        protected override void OnReceive(object message)
        {
            var msg = message as RespondTemperature;
            Console.WriteLine($"{msg.Value}");
        }

        public static Props Props(string groupId, string deviceId) =>
            Akka.Actor.Props.Create(() => new Show(groupId, deviceId));
    }
    #endregion
    #region <3-1>
    public sealed class RequestTrackDevice
    {
        public RequestTrackDevice(string groupId, string deviceId)
        {
            GroupId = groupId;
            DeviceId = deviceId;
        }

        public string GroupId { get; }
        public string DeviceId { get; }
    }

    public sealed class DeviceRegistered
    {
        public static DeviceRegistered Instance { get; } = new DeviceRegistered();
        private DeviceRegistered() { }
    }
    #endregion
}
