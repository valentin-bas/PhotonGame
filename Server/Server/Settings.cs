using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Settings
    {
        /// <summary>
        /// Initializes static members of the <see cref="Settings"/> class.
        /// </summary>
        static Settings()
        {
            ItemAutoUnsubcribeDelay = 5000;

            // movement etc
            ItemEventChannel = 0;

            MaxLockWaitTimeMilliseconds = 1000;

            DiagnosticsEventChannel = 2;
            DiagnosticsEventReliability = Reliability.Reliable;
        }

        public static byte DiagnosticsEventChannel { get; set; }

        public static Reliability DiagnosticsEventReliability { get; set; }

        public static int ItemAutoUnsubcribeDelay { get; set; }

        public static byte ItemEventChannel { get; set; }

        public static int MaxLockWaitTimeMilliseconds { get; set; }

    }
}
