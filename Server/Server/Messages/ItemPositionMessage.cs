using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Messages
{
    public class ItemPositionMessage
    {
        private readonly Vector position;

        private readonly Item source;

        public ItemPositionMessage(Item source, Vector position)
        {
            this.source = source;
            this.position = position;
        }

        public Vector Position
        {
            get
            {
                return this.position;
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
