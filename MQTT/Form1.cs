using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Application1
{
    public partial class Form1 : Form
    {

        MqttClient mqttClient;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                mqttClient = new MqttClient("194.87.237.67");
                mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                mqttClient.Subscribe(new string[] { "root/system/timestamp" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                mqttClient.Connect("root/system/timestamp");
            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
            //listBox1.Invoke((MethodInvoker)(() => listBox1.Items.Add(message)));
            label4.Invoke((MethodInvoker)delegate
                {
                    label4.Text = message;
                });
        }


        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;           
            Task.Run(() =>
            {
                if (mqttClient != null && mqttClient.IsConnected)
                {
                    mqttClient.Publish("root/system/timestamp", Encoding.UTF8.GetBytes(textBox1.Text + "  " + label3.Text));                   
                }
            });
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.PerformClick();
            label3.Text = DateTime.Now.ToString();
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
