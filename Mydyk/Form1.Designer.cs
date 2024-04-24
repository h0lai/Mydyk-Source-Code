using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

namespace Mydyk
{
    partial class Form1
    {
        private System.Windows.Forms.Label labelMydyk;
        private System.Windows.Forms.Label labelRecoil;
        private System.Windows.Forms.Label labelSetProfileName;
        private System.Windows.Forms.TextBox textBoxSetProfileName;
        private System.Windows.Forms.Label labelHorizontalParameter;
        private System.Windows.Forms.TextBox textBoxHorizontalParameter;
        private System.Windows.Forms.Label labelVerticalParameter;
        private System.Windows.Forms.TextBox textBoxVerticalParameter;
        private System.Windows.Forms.Label labelGameProcess;
        private System.Windows.Forms.TextBox textBoxGameProcess;
        private System.Windows.Forms.Label labelOnOffKey;
        private System.Windows.Forms.TextBox textBoxOnOffKey;
        private System.Windows.Forms.Label labelRecoilStatus;
        private System.Windows.Forms.Label labelUDP;

        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.labelMydyk = new System.Windows.Forms.Label();
            this.labelRecoil = new System.Windows.Forms.Label();
            this.labelSetProfileName = new System.Windows.Forms.Label();
            this.textBoxSetProfileName = new System.Windows.Forms.TextBox();
            this.labelHorizontalParameter = new System.Windows.Forms.Label();
            this.textBoxHorizontalParameter = new System.Windows.Forms.TextBox();
            this.labelVerticalParameter = new System.Windows.Forms.Label();
            this.textBoxVerticalParameter = new System.Windows.Forms.TextBox();
            this.labelGameProcess = new System.Windows.Forms.Label();
            this.textBoxGameProcess = new System.Windows.Forms.TextBox();
            this.labelOnOffKey = new System.Windows.Forms.Label();
            this.textBoxOnOffKey = new System.Windows.Forms.TextBox();
            this.labelRecoilStatus = new System.Windows.Forms.Label();
            this.labelUDP = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUDPKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMydyk
            // 
            this.labelMydyk.AutoSize = true;
            this.labelMydyk.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMydyk.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelMydyk.Location = new System.Drawing.Point(291, 9);
            this.labelMydyk.Name = "labelMydyk";
            this.labelMydyk.Size = new System.Drawing.Size(119, 39);
            this.labelMydyk.TabIndex = 0;
            this.labelMydyk.Text = "Mydyk";
            // 
            // labelRecoil
            // 
            this.labelRecoil.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecoil.Location = new System.Drawing.Point(49, 9);
            this.labelRecoil.Name = "labelRecoil";
            this.labelRecoil.Size = new System.Drawing.Size(74, 27);
            this.labelRecoil.TabIndex = 0;
            this.labelRecoil.Text = "Recoil";
            // 
            // labelSetProfileName
            // 
            this.labelSetProfileName.AutoSize = true;
            this.labelSetProfileName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelSetProfileName.Location = new System.Drawing.Point(41, 55);
            this.labelSetProfileName.Name = "labelSetProfileName";
            this.labelSetProfileName.Size = new System.Drawing.Size(86, 13);
            this.labelSetProfileName.TabIndex = 0;
            this.labelSetProfileName.Text = "Set Profile Name";
            // 
            // textBoxSetProfileName
            // 
            this.textBoxSetProfileName.Location = new System.Drawing.Point(15, 82);
            this.textBoxSetProfileName.Name = "textBoxSetProfileName";
            this.textBoxSetProfileName.Size = new System.Drawing.Size(139, 20);
            this.textBoxSetProfileName.TabIndex = 1;
            this.textBoxSetProfileName.TextChanged += new System.EventHandler(this.textBoxSetProfileName_TextChanged);
            // 
            // labelHorizontalParameter
            // 
            this.labelHorizontalParameter.AutoSize = true;
            this.labelHorizontalParameter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelHorizontalParameter.Location = new System.Drawing.Point(32, 123);
            this.labelHorizontalParameter.Name = "labelHorizontalParameter";
            this.labelHorizontalParameter.Size = new System.Drawing.Size(105, 13);
            this.labelHorizontalParameter.TabIndex = 0;
            this.labelHorizontalParameter.Text = "Horizontal Parameter";
            // 
            // textBoxHorizontalParameter
            // 
            this.textBoxHorizontalParameter.Location = new System.Drawing.Point(15, 139);
            this.textBoxHorizontalParameter.Name = "textBoxHorizontalParameter";
            this.textBoxHorizontalParameter.Size = new System.Drawing.Size(139, 20);
            this.textBoxHorizontalParameter.TabIndex = 2;
            this.textBoxHorizontalParameter.TextChanged += new System.EventHandler(this.textBoxSetProfileName_TextChanged);
            // 
            // labelVerticalParameter
            // 
            this.labelVerticalParameter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelVerticalParameter.Location = new System.Drawing.Point(32, 178);
            this.labelVerticalParameter.Name = "labelVerticalParameter";
            this.labelVerticalParameter.Size = new System.Drawing.Size(100, 23);
            this.labelVerticalParameter.TabIndex = 0;
            this.labelVerticalParameter.Text = "Vertical Parameter";
            // 
            // textBoxVerticalParameter
            // 
            this.textBoxVerticalParameter.Location = new System.Drawing.Point(15, 204);
            this.textBoxVerticalParameter.Name = "textBoxVerticalParameter";
            this.textBoxVerticalParameter.Size = new System.Drawing.Size(139, 20);
            this.textBoxVerticalParameter.TabIndex = 3;
            this.textBoxVerticalParameter.TextChanged += new System.EventHandler(this.textBoxSetProfileName_TextChanged);
            // 
            // labelGameProcess
            // 
            this.labelGameProcess.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelGameProcess.Location = new System.Drawing.Point(41, 240);
            this.labelGameProcess.Name = "labelGameProcess";
            this.labelGameProcess.Size = new System.Drawing.Size(82, 20);
            this.labelGameProcess.TabIndex = 0;
            this.labelGameProcess.Text = "Game Process";
            // 
            // textBoxGameProcess
            // 
            this.textBoxGameProcess.Location = new System.Drawing.Point(15, 263);
            this.textBoxGameProcess.Name = "textBoxGameProcess";
            this.textBoxGameProcess.Size = new System.Drawing.Size(136, 20);
            this.textBoxGameProcess.TabIndex = 4;
            this.textBoxGameProcess.TextChanged += new System.EventHandler(this.TextBoxGameProcess_TextChanged);
            // 
            // labelOnOffKey
            // 
            this.labelOnOffKey.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelOnOffKey.Location = new System.Drawing.Point(41, 297);
            this.labelOnOffKey.Name = "labelOnOffKey";
            this.labelOnOffKey.Size = new System.Drawing.Size(70, 14);
            this.labelOnOffKey.TabIndex = 0;
            this.labelOnOffKey.Text = "On / Off Key";
            // 
            // textBoxOnOffKey
            // 
            this.textBoxOnOffKey.Location = new System.Drawing.Point(12, 314);
            this.textBoxOnOffKey.Name = "textBoxOnOffKey";
            this.textBoxOnOffKey.Size = new System.Drawing.Size(139, 20);
            this.textBoxOnOffKey.TabIndex = 5;
            // 
            // labelRecoilStatus
            // 
            this.labelRecoilStatus.AutoSize = true;
            this.labelRecoilStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecoilStatus.Location = new System.Drawing.Point(40, 344);
            this.labelRecoilStatus.Name = "labelRecoilStatus";
            this.labelRecoilStatus.Size = new System.Drawing.Size(83, 24);
            this.labelRecoilStatus.TabIndex = 0;
            this.labelRecoilStatus.Text = "Disabled";
            // 
            // labelUDP
            // 
            this.labelUDP.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUDP.Location = new System.Drawing.Point(293, 76);
            this.labelUDP.Name = "labelUDP";
            this.labelUDP.Size = new System.Drawing.Size(126, 26);
            this.labelUDP.TabIndex = 0;
            this.labelUDP.Text = "UDP Flood";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(533, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(162, 332);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(305, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "not lagging";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(242, 288);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(208, 45);
            this.trackBar1.TabIndex = 13;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(577, 348);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 17);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "donations";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(540, 107);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 15;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(524, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(605, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(562, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "Profiles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(295, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Adjust Lag";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(387, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 17);
            this.label4.TabIndex = 20;
            // 
            // textBoxUDPKey
            // 
            this.textBoxUDPKey.Location = new System.Drawing.Point(298, 165);
            this.textBoxUDPKey.Name = "textBoxUDPKey";
            this.textBoxUDPKey.Size = new System.Drawing.Size(100, 20);
            this.textBoxUDPKey.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Set UDP Flood Key";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox1.Location = new System.Drawing.Point(302, 344);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(92, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "Show Overlay";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(714, 421);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxUDPKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.labelUDP);
            this.Controls.Add(this.labelMydyk);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelRecoil);
            this.Controls.Add(this.labelSetProfileName);
            this.Controls.Add(this.textBoxSetProfileName);
            this.Controls.Add(this.labelHorizontalParameter);
            this.Controls.Add(this.textBoxHorizontalParameter);
            this.Controls.Add(this.labelVerticalParameter);
            this.Controls.Add(this.textBoxVerticalParameter);
            this.Controls.Add(this.labelGameProcess);
            this.Controls.Add(this.textBoxGameProcess);
            this.Controls.Add(this.labelOnOffKey);
            this.Controls.Add(this.textBoxOnOffKey);
            this.Controls.Add(this.labelRecoilStatus);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(730, 460);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(730, 460);
            this.Name = "Form1";
            this.Text = "Mydyk";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PictureBox pictureBox2;
        private Label label1;
        private TrackBar trackBar1;
        private LinkLabel linkLabel1;
        private ComboBox comboBox2;
        private Button button2;
        private Button button3;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBoxUDPKey;
        private Label label5;
        private CheckBox checkBox1;
    }
}