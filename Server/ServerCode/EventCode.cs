using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCode
{
    public enum EventCode : byte
    {
        ItemDestroyed = 1,

        ItemMoved,

        ItemPropertiesSet,

        ItemGeneric,

        WorldExited,

        ItemSubscribed,

        ItemUnsubscribed,

        ItemProperties,

        //RadarUpdate,
        //
        //CounterData
    }
}
