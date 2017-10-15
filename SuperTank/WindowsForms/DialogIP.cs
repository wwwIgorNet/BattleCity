using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    /// <summary>
    /// The game mode selection dialog and input IP
    /// </summary>
    public partial class DialogIP : Form
    {
        public DialogIP()
        {
            InitializeComponent();

            // Получение имени компьютера.
            String host = System.Net.Dns.GetHostName();
            // Получение ip-адреса.
            MyIP = Dns.GetHostByName(host).AddressList[0];
            // Показ адреса в label'е.
            textBoxYourIP.Text = MyIP.ToString();

            textBoxIPGame.Text = MyIP.ToString(); // todo delite it

            this.ActiveControl = textBoxIPGame;
        }

        public IPAddress GameIP { get; private set; }
        public IPAddress MyIP { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ip = null;
            if(IPAddress.TryParse(textBoxIPGame.Text, out ip))
            {
                GameIP = ip;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                textBoxIPGame.ForeColor = Color.Red;
            }
        }
        public bool NewGame { get { return radioButtonNewGame.Checked; } }
        public bool JoinGame { get { return radioButtonJoinGame.Checked; } }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBoxIPGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textBoxIPGame.ForeColor == Color.Red)
            {
                textBoxIPGame.ForeColor = Color.Black;
            }
        }
    }
}
