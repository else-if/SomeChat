using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace SomeChat
{
    public partial class Form1 : Form
    {
        WebSocket websocket = new WebSocket("ws://54.93.68.108:3000/");

        private void websocket_Opened(object sender, EventArgs e)
        {
            string text = "Opened";
            if (textBox2.InvokeRequired) textBox2.Invoke(new Action<string>((s) => textBox2.Text = s), text);
            else textBox2.Text = text;
        }

        private void websocket_Error(object sender, ErrorEventArgs e)
        {
            string text = "Error";
            if (textBox2.InvokeRequired) textBox2.Invoke(new Action<string>((s) => textBox2.Text = s), text);
            else textBox2.Text = text;
        }

        private void websocket_Closed(object sender, EventArgs e)
        {
            string text = "Closed";
            if (textBox2.InvokeRequired) textBox2.Invoke(new Action<string>((s) => textBox2.Text = s), text);
            else textBox2.Text = text;
        }

        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (textBox2.InvokeRequired) textBox2.Invoke(new Action<string>((s) => textBox2.Text = s), e.Message);
            else textBox2.Text = e.Message;            
        }

        public Form1()
        {
            InitializeComponent();
            
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            websocket.Open();
    
        }

        
        private void SendButton_Click(object sender, EventArgs e)
        {
            websocket.Send(textBox1.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            websocket.Close();
        }
    }
}
