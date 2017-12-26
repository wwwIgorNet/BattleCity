namespace SuperTank.WindowsForms
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
            this.textBoxIPSecondComputer = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
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
            this.label2.Location = new System.Drawing.Point(20, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Your IP: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 158);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP second computer:";
            // 
            // textBoxIPSecondComputer
            // 
            this.textBoxIPSecondComputer.Location = new System.Drawing.Point(171, 154);
            this.textBoxIPSecondComputer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIPSecondComputer.Name = "textBoxIPSecondComputer";
            this.textBoxIPSecondComputer.Size = new System.Drawing.Size(144, 22);
            this.textBoxIPSecondComputer.TabIndex = 2;
            this.textBoxIPSecondComputer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIPSecond_KeyPress);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(21, 199);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(216, 199);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxYourIP
            // 
            this.textBoxYourIP.Location = new System.Drawing.Point(171, 121);
            this.textBoxYourIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxYourIP.Name = "textBoxYourIP";
            this.textBoxYourIP.ReadOnly = true;
            this.textBoxYourIP.Size = new System.Drawing.Size(144, 22);
            this.textBoxYourIP.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonJoinGame);
            this.groupBox1.Controls.Add(this.radioButtonNewGame);
            this.groupBox1.Location = new System.Drawing.Point(21, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(295, 87);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonJoinGame
            // 
            this.radioButtonJoinGame.AutoSize = true;
            this.radioButtonJoinGame.Location = new System.Drawing.Point(23, 53);
            this.radioButtonJoinGame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonJoinGame.Name = "radioButtonJoinGame";
            this.radioButtonJoinGame.Size = new System.Drawing.Size(110, 21);
            this.radioButtonJoinGame.TabIndex = 3;
            this.radioButtonJoinGame.Text = "Join to game";
            this.radioButtonJoinGame.UseVisualStyleBackColor = true;
            // 
            // radioButtonNewGame
            // 
            this.radioButtonNewGame.AutoSize = true;
            this.radioButtonNewGame.Location = new System.Drawing.Point(23, 23);
            this.radioButtonNewGame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButtonNewGame.Name = "radioButtonNewGame";
            this.radioButtonNewGame.Size = new System.Drawing.Size(95, 21);
            this.radioButtonNewGame.TabIndex = 2;
            this.radioButtonNewGame.Text = "New game";
            this.radioButtonNewGame.UseVisualStyleBackColor = true;
            // 
            // DialogIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 246);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxYourIP);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxIPSecondComputer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.TextBox textBoxIPSecondComputer;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxYourIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonJoinGame;
        private System.Windows.Forms.RadioButton radioButtonNewGame;
    }
}