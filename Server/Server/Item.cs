using Photon.SocketServer.Concurrency;
using Server.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Item : IDisposable
    {
#if MissingSubscribeDebug
        private static readonly ExitGames.Logging.ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
#endif

        private readonly MessageChannel<ItemDisposedMessage> disposeChannel;

        private readonly MessageChannel<ItemEventMessage> eventChannel;

        private readonly string id;

        private readonly MessageChannel<ItemPositionMessage> positionUpdateChannel;

        private readonly Hashtable properties;

        private readonly byte type;

        private readonly MmoWorld world;

        private bool disposed;

        public Item(Vector position, Hashtable properties, string id, byte type, MmoWorld world)
        {
            this.Position = position;
            this.eventChannel = new MessageChannel<ItemEventMessage>(ItemEventMessage.CounterEventSend);
            this.disposeChannel = new MessageChannel<ItemDisposedMessage>(MessageCounters.CounterSend);
            this.positionUpdateChannel = new MessageChannel<ItemPositionMessage>(MessageCounters.CounterSend);
            this.properties = properties ?? new Hashtable();
            if (properties != null)
            {
                this.PropertiesRevision++;
            }

            this.id = id;
            this.world = world;
            this.type = type;
        }

        ~Item()
        {
            this.Dispose(false);
        }

        public MessageChannel<ItemDisposedMessage> DisposeChannel
        {
            get
            {
                return this.disposeChannel;
            }
        }

        public bool Disposed
        {
            get
            {
                return this.disposed;
            }
        }

        public MessageChannel<ItemEventMessage> EventChannel
        {
            get
            {
                return this.eventChannel;
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }
        }

        public Vector Position { get; set; }

        /// </summary>
        public MessageChannel<ItemPositionMessage> PositionUpdateChannel
        {
            get
            {
                return this.positionUpdateChannel;
            }
        }

        public Hashtable Properties
        {
            get
            {
                return this.properties;
            }
        }

        public int PropertiesRevision { get; set; }

        public byte Type
        {
            get
            {
                return this.type;
            }
        }

        public MmoWorld World
        {
            get
            {
                return this.world;
            }
        }

        private IDisposable CurrentWorldRegionSubscription { get; set; }

        public void Destroy()
        {
            this.OnDestroy();
        }


        [Obsolete("Use Position_set and UpdateInterestManagement() instead")]
        public void Move(Vector position)
        {
            this.Position = position;
        }

        public void SetProperties(Hashtable propertiesSet, ArrayList propertiesUnset)
        {
            if (propertiesSet != null)
            {
                foreach (DictionaryEntry entry in propertiesSet)
                {
                    this.properties[entry.Key] = entry.Value;
                }
            }

            if (propertiesUnset != null)
            {
                foreach (object key in propertiesUnset)
                {
                    this.properties.Remove(key);
                }
            }

            this.PropertiesRevision++;
        }

        [Obsolete("Use Position_set and UpdateInterestManagement() instead")]
        public void Spawn(Vector position)
        {
            this.Position = position;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.disposeChannel.Publish(new ItemDisposedMessage(this));
                this.eventChannel.Dispose();
                this.disposeChannel.Dispose();
                this.positionUpdateChannel.Dispose();

                this.disposed = true;
            }
        }

        protected virtual ItemPositionMessage GetPositionUpdateMessage(Vector position)
        {
            return new ItemPositionMessage(this, position);
        }

        protected virtual void OnDestroy()
        {
        }

    }
}
