using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server
{
    class SocketHelper
    {
        TcpClient mscClient;
        string mstrMessage;
        string mstrResponse;
        byte[] bytesSent;
        NetworkMessage networkMessage;
        String userId;
        public void processMsg(TcpClient client, NetworkStream stream, byte[] bytesReceived)
        {
            // Handle the message received and 
            // send a response back to the client.
            mstrMessage = Encoding.ASCII.GetString(bytesReceived, 0, bytesReceived.Length);
            mscClient = client;

            try {
                networkMessage = JsonConvert.DeserializeObject<NetworkMessage>(mstrMessage);
            } catch(Exception e)
            {

            }

            //Login
            if(networkMessage.Type == NetworkMessage.Types.Login)
            {
                if(Login(networkMessage.UserId, networkMessage.UserPassword))
                {
                    userId = networkMessage.UserId;
                    mstrResponse = "User logged in";
                } else
                {
                    mstrResponse = "Unable to login";
                }
            }

            //Submit
            if(networkMessage.Type == NetworkMessage.Types.Submit && userId != null)
            {
                if(Submit(userId, networkMessage.DllFile))
                {
                    mstrResponse = "File submitted";
                } else
                {
                    mstrResponse = "Unable to submit";
                }
            }

            bytesSent = Encoding.ASCII.GetBytes(mstrResponse);
            stream.Write(bytesSent, 0, bytesSent.Length);
        }
        bool Login(string userId, string userPassword)
        {
            //Check login data with database and return true/false
            return true;
        }
        bool Submit(string userId, byte[] dllFile)
        {
            /*
            Add submission data to database
            Place dll file in correct folder
            Return true false
            */
            return true;
        }
    }
}
