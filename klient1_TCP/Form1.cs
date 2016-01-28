using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace klient1_TCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
           
        }

        // wyświetlanie danych w innym wątku
        delegate void SetTextCallback(string text);
        private void SetText2(string text2)
        {
            if (this.textBox2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText2);
                this.Invoke(d, new object[] { text2 });
            }
            else
            {
                this.textBox2.Text += text2;
            }
        }

        int zmienna;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                SetText2("próbuję wysłać dane");
                textBox2.Text = "Próba połączenia z serwerem";
               TcpClient client = new TcpClient("192.168.178.105", 1200);    //siec lan
                //TcpClient client = new TcpClient("127.0.0.1", 1200);    //localhost
                client.SendTimeout = 100;
                NetworkStream n = client.GetStream();
                textBox2.Text += " Połączono";
                zmienna++;
                string ch = zmienna.ToString();
               //string ch = textBox1.Text;
                byte[] message = Encoding.Unicode.GetBytes(ch);
                n.Write(message, 0, message.Length);
                textBox2.Text = "Wysłano";
                // client.Close();
            }
            catch (SocketException se)
            {
                textBox2.Text = "Nie udało się wysłać, za długi czas oczekiwania" + se.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // WAZNE: Tworzenie watka1 przypisanie go do watek1()
            Thread tid_watek1 = new Thread(new ThreadStart(watek1));
            // WAZNE: Tworzenie watka2 i przypisanie go do watek2()
            Thread tid_watek2 = new Thread(new ThreadStart(watek2));

            // Uruchomienie obu watkow
            tid_watek1.Start();
            tid_watek2.Start();
        }

        // Obsluga watku1
        private  void watek1()    //wywalony static
        {
            SetText2("watek1" + Environment.NewLine);
        }

        // Obsluga watku2
        private static void watek2()
        {
                while (true) ;   
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            textBox1.Text = "X=" + Cursor.Position.X.ToString();
            textBox1.Text += " Y=" + Cursor.Position.Y.ToString();

            textBox3.Text = Dns.GetHostName();
            textBox4.Text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            textBox5.Text = DateTime.Now.ToString("HH:mm:ss tt");      
        }

    }
}
