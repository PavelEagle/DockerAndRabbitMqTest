using System;
using System.Threading.Tasks;
using MassTransit;

namespace EventBus.Extensions
{
    public static class BusExtensions
    {
        public static async Task SendAsync<T>(this IBus bus, T message, string host, string queue)
        {
            var uri = new Uri($"{host}/{queue}");
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(message);
        }
    }
}