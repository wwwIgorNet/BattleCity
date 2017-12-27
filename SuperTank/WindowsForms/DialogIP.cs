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
        private string textBoxIPSecondComputerText = "";

        public DialogIP()
        {
            InitializeComponent();

            radioButtonNewGame.CheckedChanged += RadioButtonNewGame_CheckedChanged;

            // Получение имени компьютера.
            String host = System.Net.Dns.GetHostName();
            // Получение ip-адреса.
#pragma warning disable CS0618 // Type or member is obsolete
            MyIP = Dns.GetHostByName(host).AddressList[0];
#pragma warning restore CS0618 // Type or member is obsolete
                              // Показ адреса в label'е.
            textBoxYourIP.Text = MyIP.ToString();

            textBoxIPSecondComputer.Text = MyIP.ToString();

            this.ActiveControl = textBoxIPSecondComputer;

            radioButtonNewGame.Checked = true;
        }

        private void RadioButtonNewGame_CheckedChanged(object sender, EventArgs e)
        { 
            if(radioButtonNewGame.Checked)
            {
                textBoxIPSecondComputer.Enabled = false;
                textBoxIPSecondComputerText = textBoxIPSecondComputer.Text;
                textBoxIPSecondComputer.Text = "";
            }
            else
            {
                textBoxIPSecondComputer.Enabled = true;
                textBoxIPSecondComputer.Text = textBoxIPSecondComputerText;
            }
        }

        public IPAddress IPSecondComputer { get; private set; }
        public IPAddress MyIP { get; private set; }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            PressOk();
        }

        private void PressOk()
        {
            if (radioButtonJoinGame.Checked)
            {
                IPAddress ip;
                if (IPAddress.TryParse(textBoxIPSecondComputer.Text, out ip))
                {
                    IPSecondComputer = ip;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    textBoxIPSecondComputer.ForeColor = Color.Red;
                }
            }
            else
                this.DialogResult = DialogResult.OK;
        }

        public bool NewGame { get { return radioButtonNewGame.Checked; } }
        public bool JoinGame { get { return radioButtonJoinGame.Checked; } }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            PressCancel();
        }

        private void PressCancel()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBoxIPSecond_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(textBoxIPSecondComputer.ForeColor == Color.Red)
            {
                textBoxIPSecondComputer.ForeColor = Color.Black;
            }
        }
    }
}
