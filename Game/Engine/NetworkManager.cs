using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace Game.Engine
{
    static class NetworkManager
    {
        private static Socket _socket;
        public static void Connect(String host, int port)
        {
            if (_socket == null)
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            if (!_socket.Connected)
            {
                if (!_socket.)
            }
        }
    }
}
