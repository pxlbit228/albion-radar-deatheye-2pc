using System;
using System.Threading.Tasks;
using Albion.Network;

namespace X975.Radar.Packets.Handlers
{
    public class DebugHandler : PacketHandler<object>
    {
        protected override Task OnHandleAsync(object packet)
        {
            // just for debugging, i will not remove this
            
            if (packet is ResponsePacket response)
            {
                if (response.Parameters.TryGetValue(253, out var code))
                {
                    Console.WriteLine("Response: " + code);
                }
                else
                {
                    ;
                }
            }
            else if (packet is RequestPacket request)
            {
                if (request.Parameters.TryGetValue(253, out var code))
                {
                    Console.WriteLine("Request: " + code);
                }
                else
                {
                    ;
                }
            }
            else if (packet is EventPacket @event)
            {
                if (@event.Parameters.TryGetValue(252, out var code))
                {
                    Console.WriteLine("Event: " + code);
                }
            }
            
            return Task.CompletedTask;
        }
    }
}