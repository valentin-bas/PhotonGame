using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// The <see cref="MethodReturnValue"/> combines an error code with a debug message. 
    /// </summary>
    public struct MethodReturnValue
    {
        internal const string DebugMessageOk = "Ok";

        internal const short ErrorCodeOk = 0;

        private static readonly MethodReturnValue success = new MethodReturnValue { Error = ErrorCodeOk, Debug = DebugMessageOk };

        public static MethodReturnValue Ok
        {
            get
            {
                return success;
            }
        }

        public string Debug { get; set; }

        public short Error { get; set; }

        public static implicit operator bool(MethodReturnValue value)
        {
            return value.Error == ErrorCodeOk;
        }

        public static implicit operator MethodReturnValue(short value)
        {
            return Fail(value, value.ToString());
        }

        public static implicit operator int(MethodReturnValue value)
        {
            return value.Error;
        }

        public static MethodReturnValue Fail(short errorCode, string debug)
        {
            return new MethodReturnValue { Error = errorCode, Debug = debug };
        }
    }
}
