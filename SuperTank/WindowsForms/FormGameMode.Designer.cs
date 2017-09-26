namespace SuperTank.WindowsForms
{
    partial class FormGameMode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGameMode));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonJoinGame = new System.Windows.Forms.RadioButton();
            this.radioButtonNewGame = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonJoinGame);
            this.groupBox1.Controls.Add(this.radioButtonNewGame);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonJoinGame
            // 
            this.radioButtonJoinGame.AutoSize = true;
            this.radioButtonJoinGame.Location = new System.Drawing.Point(17, 43);
            this.radioButtonJoinGame.Name = "radioButtonJoinGame";
            this.radioButtonJoinGame.Size = new System.Drawing.Size(99, 17);
            this.radioButtonJoinGame.TabIndex = 3;
            this.radioButtonJoinGame.TabStop = true;
            this.radioButtonJoinGame.Text = "Will join a game";
            this.radioButtonJoinGame.UseVisualStyleBackColor = true;
            // 
            // radioButtonNewGame
            // 
            this.radioButtonNewGame.AutoSize = true;
            this.radioButtonNewGame.Checked = true;
            this.radioButtonNewGame.Location = new System.Drawing.Point(17, 19);
            this.radioButtonNewGame.Name = "radioButtonNewGame";
            this.radioButtonNewGame.Size = new System.Drawing.Size(76, 17);
            this.radioButtonNewGame.TabIndex = 2;
            this.radioButtonNewGame.TabStop = true;
            this.radioButtonNewGame.Text = "New game";
            this.radioButtonNewGame.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(104, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormGameMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 123);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game mode";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonJoinGame;
        private System.Windows.Forms.RadioButton radioButtonNewGame;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}