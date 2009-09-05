using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using XStreamer.Connection.Event;

namespace XStreamer.Connection
{
    public class Server
    {
        private TcpListener _listener;

        public EventHandler<OnClientConnectEventArgs> OnClientConnect;

        public ushort Port { get; set; }

        public Server()
        {
            // set default port
            Port = 1400;
        }

        public void Start()
        {
            _listener = new TcpListener(IPAddress.Any, Port);
            _listener.BeginAcceptTcpClient(OnConnect, null);
        }

        public static string Version
        {
            get
            {
                return "XBMSP-2.0 2.0,1.2,1.1 XStreamer Media Server\n";
            }
        }

        private void OnConnect(IAsyncResult result)
        {
            TcpClient client = _listener.EndAcceptTcpClient(result);

            // print a message
            byte[] message = Encoding.UTF8.GetBytes(Version);
            client.GetStream().Write(message, 0, message.Length);
            if (OnClientConnect != null)
                OnClientConnect(this, new OnClientConnectEventArgs(client));

            _listener.BeginAcceptTcpClient(OnConnect, null);
        }
    }
}