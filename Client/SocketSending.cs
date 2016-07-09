using System;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class SocketSending
    {
        public void Send(string adress, int port, byte[] sendBytes)
        {
            Console.WriteLine("Sending procedure had been started");

            // Entering IP and port of the server
            IPAddress[] hostAddresses = Dns.GetHostAddresses(adress);
            IPAddress ipAddr = hostAddresses[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // connecting client socket with server socket
            sender.Connect(ipEndPoint);

            Console.WriteLine("Socket connecting with {0} ", sender.RemoteEndPoint.ToString());

            // Sending data using socket
            int bytesSent = sender.Send(sendBytes);
            Console.WriteLine("Instance of the Point3d class had been sended!");

            // Closing socket
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

    }
}