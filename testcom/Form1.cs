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
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace testcom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void WriteToLCD(string s)
        {
            if (!serialPort1.IsOpen)
                serialPort1.Open();

            serialPort1.WriteLine(s);
            serialPort1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("HH:mm:ss");
            WriteToLCD(textBox1.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //button1_Click(null,null);
            button2_Click(null, null);
        }

        private string getContent(string url)
        {
            HttpWebRequest request =
            (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.Accept = "application/json";
            request.UserAgent = "Mozilla/5.0 ....";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            response.Close();
            return output.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
        public class BtcUsd
        {
            public double high { get; set; }
            public int low { get; set; }
            public double avg { get; set; }
            public double vol { get; set; }
            public double vol_cur { get; set; }
            public double last { get; set; }
            public double buy { get; set; }
            public double sell { get; set; }
            public int updated { get; set; }
        }

        public class RootObject
        {
            public BtcUsd btc_usd { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = getContent("https://yobit.net/api/3/ticker/btc_usd");
            textBox2.Text = s;

            RootObject o = JsonConvert.DeserializeObject<RootObject>(s);
            string last = o.btc_usd.last.ToString();
            
            WriteToLCD("BTC_USD:" + last);
        }
    }
}
