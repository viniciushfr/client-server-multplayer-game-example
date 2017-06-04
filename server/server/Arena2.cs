using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    class Arena2
    {
        private List<Player> players;
        
        private int width;
        private int height;     
        private Timer timer;
        private object playersLock;

        public List<Player> Players { get => players; set => players = value; }
        public  object PlayersLock { get => playersLock; set => playersLock = value; }

        public Arena2()
        {
            playersLock = new object();
            width = 1300;
            height = 650;
            
            
            Players = new List<Player>();
            for(int i = 0; i <10; i++)
            {
                lock (PlayersLock)
                {
                    players.Add(criarComida());
                }
            }
            
            timer = new Timer((e) =>
            {
                //Console.WriteLine("teste");
                if (players.Count <= 100)
                {
                    lock (PlayersLock)
                    {
                        players.Add(criarComida());
                    }
                }
            }, null, 0, (int) TimeSpan.FromMilliseconds(2000).TotalMilliseconds);
            
        }

        public Player criarComida()
        {
            Player comida = new Player();
            comida.Name = "";
            comida.Raio = 5;
            comida.Velocidade = 0;
            Random rdm = new Random();
            comida.ColorRed = rdm.Next(0, 200);
            comida.ColorGreen = rdm.Next(0, 200);
            comida.ColorBlue = rdm.Next(0, 200);
            bool perto = false;
            do
            {
                perto = false;
                comida.X = rdm.Next(0, width - 70);
                comida.Y = rdm.Next(0, height - 70);
                //Console.WriteLine(comida.X + " - " + comida.Y);
                for (int i = 0; i < players.Count; i++)
                {
                    Player outro = players.ElementAt(i);
                    var distancia = Distancia(comida, outro);
                    if (comida!=outro && distancia <= comida.Raio + outro.Raio + 10)
                    {
                        //Console.WriteLine(distancia);
                        perto = true;
                        break;
                    }
                }
            } while (perto);
            //Console.WriteLine("Gerou comida");
            return comida;
        }

        public void newPlayer(Player newPlayer)
        {
            Random rdm = new Random();
            bool perto = false;
            newPlayer.ColorRed = rdm.Next(0, 255);
            newPlayer.ColorGreen = rdm.Next(0, 255);
            newPlayer.ColorBlue = rdm.Next(0, 255);
            do
            {
                perto = false;
                newPlayer.X = rdm.Next(0, width - 70);
                newPlayer.Y = rdm.Next(0, height - 70);
                newPlayer.Raio = 20;
                newPlayer.Velocidade = 20;
                for (int i = 0; i < players.Count; i++)
                {
                    Player outro = players.ElementAt(i);
                    if (outro != newPlayer && Distancia(newPlayer, outro) <= newPlayer.Raio + outro.Raio + 10)
                    {
                        perto = true;
                        break;
                    }
                }
            } while (perto);
            Console.WriteLine("Adicionou player "+ newPlayer.Name + " ao jogo");
            lock (PlayersLock)
            {
                Players.Add(newPlayer);
            }
        }

        public int MovPlayer(Player p, char cmd)
        {
            //Console.WriteLine("Moveu o player " + p.Name);
            if (cmd == 'w')
            {
                if (p.Y - p.Velocidade >= 0)
                {
                    p.Y -= p.Velocidade;
                    checarColisao(p);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else if (cmd == 's')
            {
                if (p.Y + p.Velocidade <= this.height)
                {
                    p.Y += p.Velocidade;
                    checarColisao(p);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else if (cmd == 'd')
            {
                if (p.X + p.Velocidade <= this.width)
                {
                    p.X += p.Velocidade;
                    checarColisao(p);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else if (cmd == 'a')
            {
                if (p.X - p.Velocidade >= 0)
                {
                    p.X -= p.Velocidade;
                    checarColisao(p);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        
        public bool checarColisao(Player player)
        {

            for(int i = 0; i < players.Count; i++)
            {
                Player outro = players.ElementAt(i);

                if (outro!=player && Distancia(player,outro) <= player.Raio + outro.Raio)
                {
                    //Console.WriteLine("Colidiu " + player.Name + " - " + outro.Name);
                    if (player.Raio > outro.Raio)
                    {
                        Console.WriteLine(player.Name + " matou " + outro.Name);
                        player.Raio += outro.Raio / 5;
                        player.Velocidade -= player.Velocidade == 1 ? 0 : (player.Raio - 10) / 20;
                        lock (PlayersLock)
                        {
                            Players.Remove(outro);
                        }
                        return true;
                    }else if (player.Raio < outro.Raio)
                    {
                        Console.WriteLine(outro.Name + " matou " + player.Name);
                        outro.Raio += player.Raio/5;
                        outro.Velocidade -= outro.Velocidade == 1 ? 0 : (outro.Raio - 10) / 20;
                        lock (PlayersLock)
                        {
                            Players.Remove(player);
                        }
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        private int Distancia(Player player,Player outro)
        {
            return (int)Math.Sqrt(Math.Pow(((outro.X + outro.Raio) - (player.X + player.Raio)), 2) + Math.Pow(((outro.Y + outro.Raio) - (player.Y + player.Raio)), 2));
        }
    }
}
