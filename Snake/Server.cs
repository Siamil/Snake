using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;


namespace Snake
{
    class Server
    {
         Socket sender;
         Socket client;

        public Server()
        {
            sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            sender.Bind(new IPEndPoint(0, 80));
            sender.Listen(10);
             client = sender.Accept();


        }
        public void sendBytes (Game game)
            {
            string json = JsonConvert.SerializeObject(game.Direction, Formatting.Indented);

            byte[] data = Encoding.Default.GetBytes(json);

            client.Send(data);
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
