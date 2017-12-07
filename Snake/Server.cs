using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace Snake
{
    class Server
    {
         Socket sender;
         Socket client;
        Game game;

        public Server(ref Game game)
        {
            sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            sender.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80));
            sender.Listen(10);
            this.game = game;
            


        }
        public void waitConnection()
        {
            
                Task.Run(() =>
                {
                while (true)
                {
                    Thread.Sleep(100);
                    client = sender.Accept();
                        if (client != null)
                        {
                            sendBytes();
                            break;
                        }
                    }
                });
                
            
        }
        public void sendBytes ()
            {
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(200);
                    try
                   {
                        string json = JsonConvert.SerializeObject(game.Direction, Formatting.Indented);

                        byte[] data = Encoding.Default.GetBytes(json);

                        client.Send(data);
                   }
                  catch (SocketException e)
                    {
                        Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
                        return (e.ErrorCode);
                    }
                }
            });
            
        }
        public Direction receiveBytes()
        {
            byte[] data = new byte[255];

            int bytes = client.Receive(data, 0, data.Length, 0);
            Array.Resize(ref data, bytes);
            string json = data.ToString();
            Direction direction = JsonConvert.DeserializeObject<Direction>(json);
            return direction;
        }
    }
}
