using _3SemOOTCPProtokol;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;


void ChooseUser() { 
Console.WriteLine("Server or User?");
string who = Console.ReadLine();

if (who.ToLower() == "server") 
{
        Server textServer = new Server();
        textServer.TextServerStart();
} 
else if (who.ToLower() == "user")
{

} else
{
    Console.WriteLine("Invalid input.");
        ChooseUser();
}
}

ChooseUser();