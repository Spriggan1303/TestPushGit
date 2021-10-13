using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSignalR
{
    [HubName("signalrServer")]
    public class SignalrServer:Hub
    {
       
        public static void SendMessages()
        {
          

        }
    }
}
