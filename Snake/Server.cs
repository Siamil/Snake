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
        Direction direction;

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
                        receiveBytes();
                        break;
                    }
                }
            });


        }
        public async void sendBytes()
        {
            while (true)
            {
                await Task.Run(() =>
             {

                 Thread.Sleep(50);
                 try
                 {
                     string[] json = new string[4];
                     json[0] = JsonConvert.SerializeObject(game.Snake.GetBlock(0).Posx, Formatting.Indented);
                     json[1] = JsonConvert.SerializeObject(game.Snake.GetBlock(0).Posy, Formatting.Indented);
                     json[2] = JsonConvert.SerializeObject(game.Food.Posx, Formatting.Indented);
                     json[3] = JsonConvert.SerializeObject(game.Food.Posy, Formatting.Indented);
                     byte[] data = new byte[json.Length];
                     for (int i = 0; i < json.Length; i++)
                     {
                         data[i] = unchecked((byte)Convert.ToSByte(json[i]));
                     }
                     
                     client.Send(data);
                 }
                 catch (SocketException e)
                 {
                     Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);

                 }


             });
            }

        }
        public async void receiveBytes()
        {
            while (true)
            {
                await Task.Run(() =>
                {

                    Thread.Sleep(50);
                    try
                    {
                        byte[] data = new byte[255];
                        int bytes = client.Receive(data, 0, data.Length, 0);
                        Array.Resize(ref data, bytes);
                        sbyte[] sdata = new sbyte[2];
                        sdata[0] = (SByte)data[0];
                        sdata[1] = (SByte)data[1];
                        string[] strings = data.Select(byteValue => byteValue.ToString()).ToArray();
                        strings[0] = sdata[0].ToString();
                        strings[1] = sdata[1].ToString();
                        
                        game.MultiSnake.GetBlock(0).Posx = JsonConvert.DeserializeObject<int>(strings[0]);
                        game.MultiSnake.GetBlock(0).Posy = JsonConvert.DeserializeObject<int>(strings[1]);
                    }

                    catch
                    { }


                });

            }

        }
    }
}
