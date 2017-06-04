using server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


class GameServer
{
    private TcpListener _server;
    private Boolean _isRunning;
    //private Arena arena;
    private Arena2 arena;

    public object JsonConvert { get; private set; }

    public GameServer(int port)
    {
        _server = new TcpListener(IPAddress.Any, port);
        _server.Start();

        _isRunning = true;
        //arena = new Arena();
        arena = new Arena2();
        LoopClients();
    }

    public void LoopClients()
    {
        while (_isRunning)
        {
            // wait for client connection
            TcpClient newClient = _server.AcceptTcpClient();

            // client found.
            // create a thread to handle communication
            Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
            t.Start(newClient);
        }
    }

    public void HandleClient(object obj)
    {
        // retrieve client from parameter passed to thread
        TcpClient client = (TcpClient)obj;

        // sets two streams
        StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
        StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
        // you could use the NetworkStream to read and write, 
        // but there is no forcing flush, even when requested

        Boolean bClientConnected = true;
        string sData = null;
        Player player = new Player();
        
        try{
            player.Name = sReader.ReadLine();
            Console.WriteLine("jogador " + player.Name + " foi conectado");
        }catch(Exception e)
        {
            //Console.WriteLine("jogador não informou seu nome");
            client.Close();
            return;
        }
        
        //player = sReader.ReadLine();
        arena.newPlayer(player);
        
        
        while (bClientConnected)
        {
            // reads from stream
            try
            {
                sData = sReader.ReadLine();

                if (sData.Split(' ')[0].Equals("cmd"))
                {
                    char cmd = sData.Split(' ')[1][0];
                    int r = arena.Players.Contains(player) ? arena.MovPlayer(player, cmd) : -1;
                    if (r == 0)
                    {
                        sWriter.WriteLine("got");
                        sWriter.Flush();
                    }
                    else
                    {
                        sWriter.WriteLine("invmov");
                        sWriter.Flush();
                    }
                }
                else
                if (sData.Split(' ')[0].Equals("update"))
                {
                    byte[] p;
                    lock (arena.PlayersLock)
                    {
                        List<Player> mensage = arena.Players;
                        p = JavaScriptSerializer.Serialize<List<Player>>(mensage);

                    }
                    client.GetStream().Write(p, 0, p.Length);
                    client.GetStream().Flush();
                  
                }
                else
                {
                    //sWriter.WriteLine("invcmd");
                    //sWriter.Flush();
                }
                // shows content on the console.
                //Console.WriteLine(player.Name + "  send:  " + sData);
            }catch(Exception e)
            {
                Console.WriteLine("jogador "+ player.Name +" desconectou");
                client.Close();
                arena.Players.Remove(player);
                
                return;
            }
            
            // to write something back.
             
        }
    }

 


}