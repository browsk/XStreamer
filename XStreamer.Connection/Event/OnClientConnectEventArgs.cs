using System;
using System.Net.Sockets;

namespace XStreamer.Connection.Event
{
    public class OnClientConnectEventArgs : EventArgs
    {
        private readonly TcpClient _client;

        internal OnClientConnectEventArgs(TcpClient client)
        {
            _client = client;
        }

        public TcpClient Client
        {
            get { return _client; }
        }
    }
}