using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invitation.Utility
{
    class Log
    {
       public static short Exception(Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

            Debug.WriteLine(trace.GetFrame(0).GetMethod().ReflectedType.FullName);
            Debug.WriteLine("Line: " + trace.GetFrame(0).GetFileLineNumber());
            Debug.WriteLine("Column: " + trace.GetFrame(0).GetFileColumnNumber());

            Console.WriteLine(ex.Message);
            Console.WriteLine(trace.GetFrame(0).GetMethod().ReflectedType.FullName);
            Console.WriteLine("Line: " + trace.GetFrame(0).GetFileLineNumber());
            Console.WriteLine("Column: " + trace.GetFrame(0).GetFileColumnNumber());

            return (short)EnumHelper.Response.Error;
        }
    }
}
