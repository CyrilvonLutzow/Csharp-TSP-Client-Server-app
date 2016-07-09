using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using PointDLL;

namespace Server
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This is the server part of the application.");
            Console.WriteLine("===========================================");
            
            var thread = new Thread(
    () =>
    {
        Console.WriteLine("TCP Listner had been started. Server is waiting for connection...");

        IPAddress ipAddr = IPAddress.Any;
        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 13000);

        // tcp/ip socket creation
        Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
       

        //  binding socket to the local endpoint and listening for connections
        try
        {
            sListener.Bind(ipEndPoint);
            sListener.Listen(10); // max 10 connections


            while (true)
            {

                // programm pauses, waiting for entry connection
                Socket handler = sListener.Accept(); // этот сокет находится в состоянии подключения, нужно сначала вернуть его в прослушку, а потом посылать пакет в обратку
                var RemoteAdr = handler.RemoteEndPoint as IPEndPoint;

                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);

                Thread threa2d = new Thread(variable => Deserialize_Send_Show(bytes,RemoteAdr));
                threa2d.Start();

              

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Main();

        }
    });
            thread.Start();

        }

        private static void Deserialize_Send_Show(byte[] incomeBytes, IPEndPoint RemoteAdr)
        {
            var objec = Deserialization.Deserialize(incomeBytes);
            Point3D p = objec as Point3D;
            if (p != null)
                Console.WriteLine("Results from the client: x:{0}, y:{1}, z:{2}", p.X, p.Y, p.Z);
            var sending = new SocketSending();
            sending.Send(RemoteAdr.Address.ToString(), 12000);
        }

    }
    
    class SocketSending
    {
        public void Send(string adress, int port)
        {
            Console.WriteLine("Sending procedure had been started");

            // Entering IP and port of the server
            IPAddress[] hostAddresses = Dns.GetHostAddresses(adress);
            IPAddress ipAddr = hostAddresses[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket Ssender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            // connecting client socket with server socket
            Ssender.Connect(ipEndPoint);

            Console.WriteLine("Socket connecting with {0} ", Ssender.RemoteEndPoint.ToString());

            // Sending data using socket
            byte[] sendBytes = Encoding.Default.GetBytes("Thanks!");
            int bytesSent = Ssender.Send(sendBytes);
            Console.WriteLine("Answer had been sended!");

            // Closing socket
            Ssender.Shutdown(SocketShutdown.Both);
            Ssender.Close();
        }

    }



   public static class Deserialization
    {
       public static object Deserialize(byte[] bytes)
       {
           BinaryFormatter formatter = new BinaryFormatter();
           MemoryStream ms = new MemoryStream(bytes);
           return (object)formatter.Deserialize(ms);

       }
    }
   
}
