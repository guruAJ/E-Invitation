using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Invitation.Utility
{
    public class EnumHelper
    {
        public enum Response
        {
            Success = 1,
            Default = 0,
            Error = -1,
            Mandatory = -2,
            SessionExpired = -3,
            InvalidCredentials = -4
        }
    }
}
