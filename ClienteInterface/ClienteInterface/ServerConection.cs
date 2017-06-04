using server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

class ServerConection
{
    private TcpClient _client;

    private StreamReader _sReader;
    private StreamWriter _sWriter;

    private Boolean _isConnected;

    public ServerConection(String ipAddress, int portNum)
    {
        Client = new TcpClient();
        Client.Connect(ipAddress, portNum);
        SReader = new StreamReader(Client.GetStream(), Encoding.ASCII);
        SWriter = new StreamWriter(Client.GetStream(), Encoding.ASCII);
        //HandleCommunication();
    }

    public TcpClient Client { get => _client; set => _client = value; }
    public StreamReader SReader { get => _sReader; set => _sReader = value; }
    public StreamWriter SWriter { get => _sWriter; set => _sWriter = value; }
    public bool IsConnected { get => _isConnected; set => _isConnected = value; }

    public void HandleCommunication()
    {
        SReader = new StreamReader(Client.GetStream(), Encoding.ASCII);
        SWriter = new StreamWriter(Client.GetStream(), Encoding.ASCII);

        IsConnected = true;

        String playerName = null;
        Console.Write("nome do jogador:");
        playerName = Console.ReadLine();
        SWriter.WriteLine(playerName);
        SWriter.Flush();

        string sr = "";
        String sData = null;
        while (IsConnected)
        {
            Console.Write("send: ");
            sData = Console.ReadLine();

            SWriter.WriteLine(sData);
            SWriter.Flush();

            printArena(SReader.ReadLine());

        }
    }




  

    public void printArena(string strReceived)
    {
        string[] x = strReceived.Split('*');
        foreach (string i in x)
        {
            Console.WriteLine(i);
        }
    }
}