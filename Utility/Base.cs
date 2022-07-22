using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invitation.Utility
{
   public class PocoBase
    {
        protected string Message { get; set; }
        protected string ErrorCode { get; set; }
    }
    public struct DebugMode
    {
        public static bool IsAbsolute = false;
    }
}
