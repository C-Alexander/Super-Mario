using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class NetworkMessage
    {
        Types type;
        string userId;
        string userPassword;
        byte[] dllFile;

        public enum Types
        {
            Login, Submit
        }
        internal Types Type
        {
            get
            {
                return type;
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }
        }

        public string UserPassword
        {
            get
            {
                return userPassword;
            }
        }

        public byte[] DllFile
        {
            get
            {
                return dllFile;
            }
        }
    }
}
