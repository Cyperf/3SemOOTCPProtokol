using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _3SemOOTCPProtokol
{
    public class Server
    {

        public  void TextServerStart()
        {
            Console.WriteLine("TCP Server:");

            TcpListener listener = new TcpListener(IPAddress.Any, 21);

            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(socket));
            }

            void HandleClient(TcpClient socket)
            {
                NetworkStream ns = socket.GetStream();

                StreamReader reader = new StreamReader(ns);
                StreamWriter writer = new StreamWriter(ns);

                bool keepListening = true;

                while (keepListening)
                {
                    string message = reader.ReadLine();

                    Console.WriteLine("Client send: " + message);

                    writer.WriteLine(message);
                    writer.Flush();

                    switch (message.ToLower())
                    {
                        case "stop":
                            writer.WriteLine("Goodbye");
                            writer.Flush();
                            keepListening = false;
                            break;

                        case "random":
                            writer.WriteLine(HandleNumbers("random"));
                            writer.Flush();
                            break;

                        case "add":
                            writer.WriteLine(HandleNumbers("add"));
                            writer.Flush();
                            break;

                        case "subtract":
                            writer.WriteLine(HandleNumbers("subtract"));
                            writer.Flush();
                            break;

                        default:
                            writer.WriteLine("Invalid command. The available commands are: Add, Subtract, Random or Stop");
                            writer.Flush();
                            break;
                    }
                }

                int HandleNumbers(string type)
                {
                    int number1 = 0;
                    int number2 = 0;


                    Random randomNumber = new Random();

                    writer.WriteLine("Input numbers");
                    writer.Flush();
                    string numbers = reader.ReadLine();
                    var splitNumbers = numbers.Split(' ');

                    if (splitNumbers.Length == 2)
                    {
                        int.TryParse(splitNumbers[0], out number1);
                        int.TryParse(splitNumbers[1], out number2);
                    }
                    else
                    {
                        writer.WriteLine("Invalid input");
                        writer.Flush();
                    }

                    switch (type)
                    {

                        case "random":
                            if (number1 < number2)
                            {
                                return randomNumber.Next(number1, number2) + 1;
                            }
                            return randomNumber.Next(number2, number1) + 1;

                        case "add":

                            return number1 + number2;

                        case "subtract":

                            return number1 - number2;

                    }
                    return 0;
                }
            }
        }
    }
}
