using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTank.WindowsForms
{
    public partial class FormGameMode : Form
    {
        public FormGameMode()
        {
            InitializeComponent();

            this.ActiveControl = button1;
        }

        public bool NewGame { get { return radioButtonNewGame.Checked; } }
        public bool JoinGame { get { return radioButtonJoinGame.Checked; } }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
