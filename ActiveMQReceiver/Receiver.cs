using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveMQReceiver
{
    public partial class Receiver : Form
    {
        private const string URL = "tcp://localhost:61616";
        IConnection conn;
        public Receiver()
        {
            InitializeComponent();
            this.Text = "Receiver";
        }

        private void Message_Listener(IMessage message)
        {
            ITextMessage textMessage = (ITextMessage)message;
            string receivedText = (string)textMessage.Text;
            MessageBox.Show(receivedText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn != null && conn.IsStarted)
            {
                conn.Close();
                button1.Text = "Start Receiver";
            }
            else
            {
                IConnectionFactory connFactory = new ConnectionFactory(URL);
                conn = connFactory.CreateConnection();
                ISession session = conn.CreateSession();
                conn.Start();
                IDestination dest = SessionUtil.GetDestination(session, "pavyzdine.eile");
                IMessageConsumer receiver = session.CreateConsumer(dest);
                receiver.Listener += new MessageListener(Message_Listener);
                button1.Text = "Stop Receiver";
            }
        }
    }
}
