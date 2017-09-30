﻿namespace SuperTank.WindowsForms
{
    partial class DialogIP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogIP));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIPGame = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxYourIP = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonJoinGame = new System.Windows.Forms.RadioButton();
            this.radioButtonNewGame = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Your IP: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter IP game: ";
            // 
            // textBoxIPGame
            // 
            this.textBoxIPGame.Location = new System.Drawing.Point(104, 125);
            this.textBoxIPGame.Name = "textBoxIPGame";
            this.textBoxIPGame.Size = new System.Drawing.Size(109, 20);
            this.textBoxIPGame.TabIndex = 2;
            this.textBoxIPGame.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIPGame_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxYourIP
            // 
            this.textBoxYourIP.Location = new System.Drawing.Point(104, 98);
            this.textBoxYourIP.Name = "textBoxYourIP";
            this.textBoxYourIP.ReadOnly = true;
            this.textBoxYourIP.Size = new System.Drawing.Size(109, 20);
            this.textBoxYourIP.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonJoinGame);
            this.groupBox1.Controls.Add(this.radioButtonNewGame);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 71);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonJoinGame
            // 
            this.radioButtonJoinGame.AutoSize = true;
            this.radioButtonJoinGame.Location = new System.Drawing.Point(17, 43);
            this.radioButtonJoinGame.Name = "radioButtonJoinGame";
            this.radioButtonJoinGame.Size = new System.Drawing.Size(85, 17);
            this.radioButtonJoinGame.TabIndex = 3;
            this.radioButtonJoinGame.TabStop = true;
            this.radioButtonJoinGame.Text = "Join to game";
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
            // DialogIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 200);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxYourIP);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxIPGame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogIP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IP";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIPGame;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxYourIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonJoinGame;
        private System.Windows.Forms.RadioButton radioButtonNewGame;
    }
}