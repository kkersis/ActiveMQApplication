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

namespace ActiveMQSender
{
    public partial class Sender : Form
    {
        private const string URL = "tcp://localhost:61616";

        public Sender()
        {
            var receiverForm = new ActiveMQReceiver.Receiver();
            receiverForm.Show(); 
            InitializeComponent();
            this.Text = "Sender";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IConnectionFactory connFactory = new ConnectionFactory(URL);
            using IConnection conn = connFactory.CreateConnection();
            using ISession session = conn.CreateSession();
            //IObjectMessage
            ITextMessage textMessage = session.CreateTextMessage(textBox1.Text);
            textMessage.NMSMessageId = "1";
            conn.Start();
            IDestination dest = SessionUtil.GetDestination(session, "pavyzdine.eile");
            IMessageProducer messageProducer = session.CreateProducer(dest);

            messageProducer.Send(textMessage);
        }
    }
}
