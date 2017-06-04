using server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteInterface
{
    public partial class Form1 : Form
    {


        private int x;
        private int y;

        private int numP;
        private ServerConection sConection;

        private List<Player> players;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            //sConection = new ServerConection("192.168.100.39", 5555);
            players = new List<Player>();
            //sConection.SWriter.WriteLine("vini");
            //x = 50;
            //y = 50;
            //numP = 1;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach(var p in new List<Player>(players)) {
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(255,p.ColorRed , p.ColorGreen, p.ColorBlue)),
                    p.X,
                    p.Y,
                    p.Raio*2,
                    p.Raio*2);
                e.Graphics.DrawString(p.Name, new Font("Arial", 12), Brushes.Black,p.X+p.Raio,p.Y+p.Raio*2);
            }
        }
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string reply="no reply";
            if (e.KeyCode == Keys.W)
            {
                sConection.SWriter.WriteLine("cmd w");
                sConection.SWriter.Flush();
                reply = sConection.SReader.ReadLine();
                y -= 10;
            }else if(e.KeyCode == Keys.S)
            {
                sConection.SWriter.WriteLine("cmd s");
                sConection.SWriter.Flush();
                reply = sConection.SReader.ReadLine();
                y += 10;
            }else if(e.KeyCode == Keys.D)
            {
                sConection.SWriter.WriteLine("cmd d");
                sConection.SWriter.Flush();
                reply = sConection.SReader.ReadLine();
                x += 10;
            }else if(e.KeyCode == Keys.A)
            {
                sConection.SWriter.WriteLine("cmd a");
                sConection.SWriter.Flush();
                reply = sConection.SReader.ReadLine();
                x -=10;
            }
            Console.Write(reply);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sConection!=null) { 
                string sData = "update";
                sConection.SWriter.WriteLine(sData);
                sConection.SWriter.Flush();

                byte[] bytes = new byte[sConection.Client.ReceiveBufferSize];
                sConection.Client.GetStream().Read(bytes, 0, (int)sConection.Client.ReceiveBufferSize);

                players = JavaScriptSerializer.Deserialize<List<Player>>(bytes);

                Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = "192.168.100.38";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            //textBox2.Text = "192.168.100.38";
            Console.WriteLine(textBox2.Text);
            sConection = new ServerConection(textBox2.Text, 5555);
            sConection.SWriter.WriteLine(name);
            textBox1.Dispose();
            button1.Dispose();
            label1.Dispose();
            textBox2.Dispose();
            label2.Dispose();
            

        }

        
    }
}
