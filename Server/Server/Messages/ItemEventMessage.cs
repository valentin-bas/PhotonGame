using ExitGames.Diagnostics.Counter;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Messages
{
    public class ItemEventMessage
    {
        public static readonly CountsPerSecondCounter CounterEventReceive = new CountsPerSecondCounter("ItemEventMessage.Receive");

        public static readonly CountsPerSecondCounter CounterEventSend = new CountsPerSecondCounter("ItemEventMessage.Send");

        private readonly IEventData eventData;

        private readonly SendParameters sendParameters;

        private readonly Item source;

        public ItemEventMessage(Item source, IEventData eventData, SendParameters sendParameters)
        {
            this.source = source;
            this.eventData = eventData;
            this.sendParameters = sendParameters;
        }

        public IEventData EventData
        {
            get
            {
                return this.eventData;
            }
        }

        public SendParameters SendParameters
        {
            get
            {
                return this.sendParameters;
            }
        }

        public Item Source
        {
            get
            {
                return this.source;
            }
        }

    }
}
