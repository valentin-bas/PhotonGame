using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Messages
{
    public sealed class ItemDisposedMessage
    {

        private readonly Item source;

        public ItemDisposedMessage(Item source)
        {
            this.source = source;
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
