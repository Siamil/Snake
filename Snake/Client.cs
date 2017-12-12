﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Web;
using Newtonsoft.Json;
using System.Threading;

namespace Snake
{
    class Client
    {
        Socket sender;
        Game game;
        Direction direction;
        Food food;

        public Client(ref Game game)
        {
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
            sender.Connect(endpoint);
            this.game = game;
            sendBytes();

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
                        string json = JsonConvert.SerializeObject(game.MultiSnake.Direction, Formatting.Indented);

                        byte[] data = Encoding.Default.GetBytes(json);

                        sender.Send(data);
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
                    int bytes = sender.Receive(data, 0, data.Length, 0);
                    Array.Resize(ref data, bytes);
                    sbyte[] sdata = new sbyte[2];
                    sdata[0] = (SByte)data[0];
                    sdata[1] = (SByte)data[1];
                    string [] strings = data.Select(byteValue => byteValue.ToString()).ToArray();
                    strings[0] = sdata[0].ToString();
                    strings[1] = sdata[1].ToString();
                    //  string json = Encoding.UTF8.GetString(data);
                    //  direction = JsonConvert.DeserializeObject<Direction>(strings[0]);
                    //food.Posx = JsonConvert.DeserializeObject<int>(strings[1]);
                    //  food.Posy = JsonConvert.DeserializeObject<int>(strings[2]);
                    game.Snake.Posx = JsonConvert.DeserializeObject<int>(strings[0]);
                    game.Snake.Posy = JsonConvert.DeserializeObject<int>(strings[1]);
                    game.Food.Posx = JsonConvert.DeserializeObject<int>(strings[2]);
                    game.Food.Posy = JsonConvert.DeserializeObject<int>(strings[3]);
                }

                catch
                { }


            });
                
            }

        }
    }
}
