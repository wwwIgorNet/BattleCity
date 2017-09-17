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
using SuperTank.Audio;

namespace SuperTank.WindowsForms
{
    public partial class GameForm : Form
    {
        private IKeyboard keyboard;
        private ScrenGame screnGame;
        private static IViewSound viewSound;

        public GameForm(SceneView sceneView, IViewSound sound)
        {
            InitializeComponent();

            GameForm.viewSound = sound;
            this.ClientSize = new Size(ConfigurationView.WindowClientWidth, ConfigurationView.WindowClientHeight);

            this.SuspendLayout();
            screnGame = new ScrenGame(sceneView);
            Controls.Add(screnGame);
            this.ResumeLayout(false);

            this.Focus();
        }

        public static IViewSound Sound { get { return viewSound; } }

        public IKeyboard Keyboard
        {
            get { return keyboard; }
            set { keyboard = value; }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Space:
                case Keys.Enter:
                case Keys.Escape:
                    keyboard.KeyDown(e.KeyCode);
                    break;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Space:
                case Keys.Enter:
                case Keys.Escape:
                    keyboard.KeyUp(e.KeyCode);
                    break;
            }
        }
    }
}
