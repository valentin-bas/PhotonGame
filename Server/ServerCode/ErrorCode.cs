using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCode
{
    public enum ErrorCode
    {
        Ok = 0,

        Fatal = 1,

        ParameterOutOfRange = 51,

        OperationNotSupported,

        InvalidOperationParameter,

        InvalidOperation,

        //ItemAccessDenied,
        //
        //InterestAreaNotFound,
        //
        //InterestAreaAlreadyExists,
        //
        //WorldAlreadyExists = 101,
        //
        //WorldNotFound,
        //
        //ItemAlreadyExists,
        //
        //ItemNotFound
    }
}
