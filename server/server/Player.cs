using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Drawing;

namespace server
{
   
    public class Player
    {
        private string name;
        private int x;
        private int y;
        private int raio;
        private int velocidade;
        private int colorRed;
        private int colorGreen;
        private int colorBlue;
      
        public string Name { get => name; set => name = value; }
        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }
        public int Raio { get => raio; set => raio = value; }
        public int Velocidade { get => velocidade; set => velocidade = value; }
        public int ColorGreen { get => colorGreen; set => colorGreen = value; }
        public int ColorRed { get => colorRed; set => colorRed = value; }
        public int ColorBlue { get => colorBlue; set => colorBlue = value; }
    }
}
