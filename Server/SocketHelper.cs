using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerNetworkMessage;

namespace Server
{
    static class SocketHelper
    {
        static Dictionary<int, TcpClient> users = new Dictionary<int, TcpClient>();

        static TcpClient mscClient;
        static string mstrMessage;
        static string mstrResponse;
        static byte[] bytesSent;
        static NetworkMessage networkMessage;
        static public void processMsg(TcpClient client, NetworkStream stream, byte[] bytesReceived)
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
                string session = Database.Login(networkMessage);
                if (session != null)
                {
                    users.Add(networkMessage.UserId, client);
                    NetworkMessage response = new NetworkMessage(session);
                    mstrResponse = JsonConvert.SerializeObject(response);
                } else
                {
                    mstrResponse = "Unable to login";
                }
            }

            //Submit
            if(networkMessage.Type == NetworkMessage.Types.Submit && users.ContainsKey(networkMessage.UserId))
            {
                if(Submit())
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
        static bool Submit()
        {
            /*
            Add submission data to database
            Place dll file in correct folder
            Return true false
            */
            int fileID = Database.Submit(networkMessage);
            if (fileID != 0)
            {
                /*
                Decode dll file from networkMessage,
                output it to server directory named %fileId%.dll then return true if above went succesful
                */
                return true;
            }
            return false;
        }
    }
}
