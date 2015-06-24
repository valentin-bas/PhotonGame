using ExitGames.Diagnostics.Counter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Messages
{
    public static class MessageCounters
    {
        public static readonly CountsPerSecondCounter CounterReceive = new CountsPerSecondCounter("ItemMessage.Receive");

        public static readonly CountsPerSecondCounter CounterSend = new CountsPerSecondCounter("ItemMessage.Send");

    }
}
