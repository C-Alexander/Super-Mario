using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerNetworkMessage
{
    class NetworkMessage
    {
        Types type;
        Users user;
        string session;
        int userId;
        string userPassword;
        Submit submit = new Submit();

        public NetworkMessage(string session)
        {
            this.session = session;
        }

        public enum Types
        {
            Login, Submit
        }
        public enum Users
        {
            Student, Teacher
        }
        internal Types Type
        {
            get
            {
                return type;
            }
        }
        internal Users User
        {
            get
            {
                return user;
            }
        }
        public string Session
        {
            get
            {
                return session;
            }
        }
        public int UserId
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
        public Submit Submit
        {
            get
            {
                return submit;
            }
        }
    }
    internal class Submit
    {
        byte[] dllFile;

        public byte[] DllFile
        {
            get
            {
                return dllFile;
            }
        }
    }
}
