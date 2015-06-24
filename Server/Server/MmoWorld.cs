using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MmoWorld : IDisposable
    {
        public static readonly MmoWorld Instance = new MmoWorld();

        private readonly ItemCache itemCache;

        public MmoWorld()
        {
            this.itemCache = new ItemCache(Settings.MaxLockWaitTimeMilliseconds);
        }

        ~MmoWorld()
        {
            //this.Dispose(false);
        }

        public ItemCache ItemCache
        {
            get
            {
                return this.itemCache;
            }
        }

        #region IDisposable

        /// <summary>
        ///   Disposes all used <see cref = "Region">regions</see>.
        /// </summary>
        public void Dispose()
        {
            //this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
