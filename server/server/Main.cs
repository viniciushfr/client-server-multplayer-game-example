using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aguardando Conexoes");
            GameServer server = new GameServer(5555);
            /*
            List<Player> lp = new List<Player>();
            Player p = new Player();
            p.Name = "vinicius";
            p.X = 300;
            p.Y = 200;
            lp.Add(p);

            byte [] bp = JavaScriptSerializer.Serialize<List<Player>>(lp);
           

          

            List<Player> dlp = JavaScriptSerializer.Deserialize<List<Player>>(bp);
            Console.WriteLine(dlp.First().Name);
            Console.WriteLine(dlp.First().X);
            Console.WriteLine(dlp.First().Y);

            Console.WriteLine("Press key to continue");
            Console.Read();
            */
        }
    }
}