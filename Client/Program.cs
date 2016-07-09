using System;
using System.Threading;
using PointDLL;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the client part of the application.");
            Console.WriteLine("===========================================");

            var listener = new SocketListening();
            var listenerThread = new Thread(variable => listener.Listen(12000));
            
            listenerThread.Start();
            
            StartInteractionWithTheServer();
            
        }

       private static void StartInteractionWithTheServer()
        {
            try
            {
                Thread.Sleep(1000);
                Console.WriteLine("===================================");
                Console.Write("Enter the IP adress of the server: ");
                string adress = Console.ReadLine();

                Console.WriteLine("Please enter the (int) variables as X-Y-Z coordinates of some 3d point: ");
                
                Console.Write("X: ");
                int X =  Convert.ToInt32(Console.In.ReadLine());

                Console.Write("Y: ");
                int Y = Convert.ToInt32(Console.In.ReadLine());

                Console.Write("Z: ");
                int Z = Convert.ToInt32(Console.In.ReadLine());

                var point = new Point3D(X,Y,Z);

                var bytes = Serialization.Serialize(point);

                var SocketSend = new SocketSending();
                SocketSend.Send(adress,13000,bytes);

                Thread.Sleep(1000);
                bool loop = false;
                do
                {
                    Console.WriteLine("Continue? Press Y or N: ");
                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.KeyChar)
                    {
                        case 'y':
                            StartInteractionWithTheServer();
                            loop = true;
                            break;
                        case 'n':
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Error!");
                            break;
                    }
                } 
                while (loop  == false);

            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
               StartInteractionWithTheServer();
            }
            
            }
    }
}
