﻿using System;
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


 #region wyświetlanie danych w innym wątku
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
                this.textBox2.Text = text2;
            }
        }

        private void SetText1(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText1);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

        private void SetText3(string text)
        {
            if (this.textBox3.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText3);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox3.Text = text;
            }
        }

        private void SetText4(string text)
        {
            if (this.textBox4.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText4);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox4.Text = text;
            }
        }

        private void SetText5(string text)
        {
            if (this.textBox5.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText5);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox5.Text = text;
            }
        }

        private void SetText6(string text)
        {
            if (this.textBox6.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText6);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox6.Text = text;
            }
        }

#endregion

        int zmienna;
        private void timer1_Tick(object sender, EventArgs e)
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
            try
            {
                SetText2("próbuję wysłać dane");
                //TcpClient client = new TcpClient("192.168.178.105", 1200);    //siec lan
                TcpClient client = new TcpClient("127.0.0.1", 1200);    //localhost
                client.SendTimeout = 100;
                NetworkStream n = client.GetStream();
                SetText2(" Połączono");
                zmienna++;
                //string ch = zmienna.ToString();
                //wysłanie ramki danych [x y host ip time]
                string ch = textBox1.Text + Environment.NewLine
                   + textBox6.Text + Environment.NewLine
                   + textBox3.Text + Environment.NewLine
                   + textBox4.Text + Environment.NewLine
                   + textBox5.Text + Environment.NewLine;
                byte[] message = Encoding.Unicode.GetBytes(ch);
                n.Write(message, 0, message.Length);
                SetText2("Wysłano");
                // client.Close();
            }
            catch (SocketException se)
            {
                SetText2("Nie udało się wysłać, za długi czas oczekiwania" + se.Message);
            }
            //SetText2("watek1" + Environment.NewLine);
        }

        // Obsluga watku2
        private void watek2()
        {
            SetText1(Cursor.Position.X.ToString() );
            SetText6( Cursor.Position.Y.ToString() );

            SetText3( Dns.GetHostName() );
            SetText4( Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString() );
            SetText5( DateTime.Now.ToString("HH:mm:ss tt") );       
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
