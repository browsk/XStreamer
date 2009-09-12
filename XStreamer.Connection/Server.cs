using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using XStreamer.Connection.Event;

namespace XStreamer.Connection
{
    public class Server : IDisposable
    {
        private TcpListener _listener;

        private readonly IList<WeakReference> _connectedClients = new List<WeakReference>();

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
            _listener.Server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _listener.Start();
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

            // purge old clients
            var closedClients = new List<WeakReference>();
            foreach (var connectedClient in _connectedClients)
            {
                var weakClient = connectedClient.Target;
                if (weakClient == null)
                    closedClients.Add(connectedClient);
            }

            foreach (var closedClient in closedClients)
            {
                _connectedClients.Remove(closedClient);
            }

            // add new client
            _connectedClients.Add(new WeakReference(client));


            // print a message
            byte[] message = Encoding.UTF8.GetBytes(Version);
            client.GetStream().Write(message, 0, message.Length);
            if (OnClientConnect != null)
                OnClientConnect(this, new OnClientConnectEventArgs(client));

            _listener.BeginAcceptTcpClient(OnConnect, null);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _listener.Stop();

                foreach (var weakClient in _connectedClients)
                {
                    var client = (TcpClient) weakClient.Target;
                    if (client != null && client.Connected)
                    {
                        client.Close();
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}