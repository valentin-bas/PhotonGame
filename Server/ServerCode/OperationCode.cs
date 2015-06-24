using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCode
{
    public enum OperationCode : byte
    {
        Nil = 0,

        EnterWorld = 91,

        ExitWorld = 92,

        Move = 93,

        //RaiseGenericEvent = 94,
        //
        //SetProperties = 95,
        //
        //SpawnItem = 96,
        //
        //DestroyItem = 97,
        //
        //SubscribeItem = 98,
        //
        //UnsubscribeItem = 99,
        //
        //SetViewDistance = 100,
        //
        //AttachInterestArea = 101,
        //
        //DetachInterestArea = 102,
        //
        //AddInterestArea = 103,
        //
        //RemoveInterestArea = 104,
        //
        //GetProperties = 105,
        //
        //MoveInterestArea = 106,
        //
        //RadarSubscribe = 107,
        //
        //UnsubscribeCounter = 108,
        //
        //SubscribeCounter = 109
    }
}
