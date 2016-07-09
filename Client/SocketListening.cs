using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class SocketListening
    {
        public void Listen(int port)
        {
            
            Console.WriteLine("TCP Listner had been started...");

            IPAddress ipAddr = IPAddress.Any;
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            // tcp/ip socket creation
            Socket Listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
     //       Listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //  binding socket to the local endpoint and listening for connections
            try
            {
                Listener.Bind(ipEndPoint);
                Listener.Listen(10); // max 10 connections

            
                while (true)
                {

                    // programm pauses, waiting for entry connection
                    Socket sHandler = Listener.Accept();

                    byte[] bytes = new byte[1024];
                    int bytesRec = sHandler.Receive(bytes);
                  
                    string data = Encoding.Default.GetString(bytes);
                    int i = data.IndexOf("!");
                    data = data.Substring(0, i + 1);
                    Console.WriteLine("Answer from the server: {0}", data);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}