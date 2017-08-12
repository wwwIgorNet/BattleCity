using SuperTank.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace SuperTank.WindowsForms
{
    public partial class GameForm : Form
    {
        private SceneView sceneView;
        public GameForm(SceneView sceneView)
        {
            InitializeComponent();

            GraphicsOption();
            this.BackColor = ConfigurationView.BackColor;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);

            this.SuspendLayout();
            this.sceneView = sceneView;
            Controls.Add(sceneView);
            this.ResumeLayout(false);

            this.Focus();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Left)
                Keyboard.Left = true;
            else if (e.KeyCode == Keys.Right)
                Keyboard.Right = true;
            else if (e.KeyCode == Keys.Up)
                Keyboard.Up = true;
            else if (e.KeyCode == Keys.Down)
                Keyboard.Down = true;
            else if (e.KeyCode == Keys.Space)
                Keyboard.Space = true;
            else if (e.KeyCode == Keys.Enter)
                Keyboard.Enter = true;
            else if (e.KeyCode == Keys.Escape)
                Keyboard.Escape = true;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Left)
                Keyboard.Left = false;
            else if (e.KeyCode == Keys.Right)
                Keyboard.Right = false;
            else if (e.KeyCode == Keys.Up)
                Keyboard.Up = false;
            else if (e.KeyCode == Keys.Down)
                Keyboard.Down = false;
            else if (e.KeyCode == Keys.Space)
                Keyboard.Space = false;
            else if (e.KeyCode == Keys.Enter)
                Keyboard.Enter = false;
            else if (e.KeyCode == Keys.Escape)
                Keyboard.Escape = false;
        }

        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
