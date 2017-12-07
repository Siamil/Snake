using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Web;
using Newtonsoft.Json;
namespace Snake
{
    class Client
    {
        Socket sender;
        
        public Client()
        {
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endpoint = new IPEndPoint(0, 80);
            sender.Connect(endpoint);

        }
        public void sendMessage(Game game)
        {
            string json = JsonConvert.SerializeObject(game.MultiDirection, Formatting.Indented);
            
            byte[] data = Encoding.Default.GetBytes(json);

            sender.Send(data);
        }
        public Direction  receiveBytes()
        {
            byte[] data = new byte[255];
            
            int bytes = sender.Receive(data, 0, data.Length, 0);
            Array.Resize(ref data, bytes);
            string json = data.ToString();
            Direction direction = JsonConvert.DeserializeObject<Direction>(json);
            return direction;
        }
    }
}
