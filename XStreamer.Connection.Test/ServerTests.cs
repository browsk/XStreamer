using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Xunit;

namespace XStreamer.Connection.Test
{
    public class ServerTests
    {
        [Fact]
        void Test_Can_Connect_To_Server()
        {
            using (Server server = new Server())
            {
                server.Start();

                TcpClient connection = new TcpClient();

                try
                {
                    connection.Connect("localhost", 1400);
                }
                catch (SocketException e)
                {
                    Assert.True(false, "Failed to connect to socket. " + e.Message);
                }

                Assert.True(connection.Connected);
            }
        }

        [Fact]
        void Test_Client_Connection_Generates_Event()
        {
            using(Server server = new Server())
            {
                server.Start();

                TcpClient client = null;
                ManualResetEvent connectEvent = new ManualResetEvent(false);

                server.OnClientConnect += (sender, args) =>
                {
                    client = args.Client;
                    connectEvent.Set();
                };


                TcpClient connection = new TcpClient();

                try
                {
                    // blocking connect
                    connection.Connect("localhost", 1400);
                }
                catch (SocketException e)
                {
                    Assert.True(false, "Failed to connect to socket. " + e.Message);
                }

                Assert.True(connectEvent.WaitOne(200), "OnClientConnect event not generated");
                Assert.NotNull(client);
            }
        }

        [Fact]
        void Test_Can_Set_Listening_Port()
        {
            using (Server server = new Server{ Port = 1404 })
            {
                server.Start();

                TcpClient connection = new TcpClient();

                try
                {
                    connection.Connect("localhost", 1404);
                }
                catch (SocketException e)
                {
                    Assert.True(false, "Failed to connect to socket. " + e.Message);
                }

                Assert.True(connection.Connected);
            }
        }

        [Fact]
        public void Test_Server_Returns_Correct_Ident_String()
        {
            using (Server server = new Server())
            {
                server.Start();

                TcpClient connection = new TcpClient();

                try
                {
                    connection.Connect("localhost", 1400);
                }
                catch (SocketException e)
                {
                    Assert.True(false, "Failed to connect to socket. " + e.Message);
                }

                Assert.True(connection.Connected);

                // read the data from the socket
                byte[] data = new byte[512];

                NetworkStream stream = connection.GetStream();
                stream.ReadTimeout = 200;
                int readResult = stream.Read(data, 0, data.Length);

                Assert.True(readResult > 0, "Failed reading from client");

                // get the version string
                string version = Encoding.UTF8.GetString(data, 0, readResult);
                string expectedVersion = Server.Version;

                Assert.Equal(expectedVersion, version);
            }
            
        }
    }
}
