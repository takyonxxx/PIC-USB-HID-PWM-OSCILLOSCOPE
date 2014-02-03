namespace ScopeTest {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if ( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.startButton = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.samplesTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.scopeControl = new Instruments.ScopeControl();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_data = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.text_pwmfreq = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkT1 = new System.Windows.Forms.CheckBox();
            this.checkT4 = new System.Windows.Forms.CheckBox();
            this.checkT16 = new System.Windows.Forms.CheckBox();
            this.text_duty = new System.Windows.Forms.TextBox();
            this.text_pwm = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.startButton.Location = new System.Drawing.Point(3, 7);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(133, 28);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 50;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.BackColor = System.Drawing.SystemColors.Desktop;
            this.trackBar1.LargeChange = 3;
            this.trackBar1.Location = new System.Drawing.Point(400, 89);
            this.trackBar1.Maximum = 21;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(207, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 15;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // samplesTextbox
            // 
            this.samplesTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.samplesTextbox.Location = new System.Drawing.Point(576, 49);
            this.samplesTextbox.Name = "samplesTextbox";
            this.samplesTextbox.Size = new System.Drawing.Size(31, 20);
            this.samplesTextbox.TabIndex = 7;
            this.samplesTextbox.Tag = "";
            this.samplesTextbox.Text = "10";
            this.samplesTextbox.TextChanged += new System.EventHandler(this.samples_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(538, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "50ms";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(405, 22);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(455, 22);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 12;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(505, 22);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(555, 21);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 14;
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // scopeControl
            // 
            this.scopeControl.AllowDrop = true;
            this.scopeControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scopeControl.AutoScroll = true;
            this.scopeControl.AutoSize = true;
            this.scopeControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scopeControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scopeControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scopeControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.scopeControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.scopeControl.ForeColor = System.Drawing.Color.White;
            this.scopeControl.GridColor = System.Drawing.Color.DimGray;
            this.scopeControl.GridSpacing = 0F;
            this.scopeControl.Location = new System.Drawing.Point(11, 0);
            this.scopeControl.Margin = new System.Windows.Forms.Padding(1);
            this.scopeControl.Name = "scopeControl";
            this.scopeControl.Size = new System.Drawing.Size(623, 277);
            this.scopeControl.TabIndex = 0;
            this.scopeControl.UnitsY = 10F;
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.BackColor = System.Drawing.SystemColors.ControlText;
            this.trackBar2.LargeChange = 3;
            this.trackBar2.Location = new System.Drawing.Point(574, 58);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 164);
            this.trackBar2.TabIndex = 15;
            this.trackBar2.Value = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBar3
            // 
            this.trackBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar3.BackColor = System.Drawing.SystemColors.ControlText;
            this.trackBar3.LargeChange = 3;
            this.trackBar3.Location = new System.Drawing.Point(504, 58);
            this.trackBar3.Maximum = 12;
            this.trackBar3.Minimum = 1;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar3.Size = new System.Drawing.Size(45, 164);
            this.trackBar3.TabIndex = 16;
            this.trackBar3.Value = 10;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // lbl_status
            // 
            this.lbl_status.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_status.Location = new System.Drawing.Point(142, 13);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(252, 18);
            this.lbl_status.TabIndex = 20;
            this.lbl_status.Text = "Disconnected";
            // 
            // lbl_data
            // 
            this.lbl_data.BackColor = System.Drawing.Color.Black;
            this.lbl_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_data.ForeColor = System.Drawing.Color.White;
            this.lbl_data.Location = new System.Drawing.Point(504, 24);
            this.lbl_data.Name = "lbl_data";
            this.lbl_data.Size = new System.Drawing.Size(115, 31);
            this.lbl_data.TabIndex = 21;
            this.lbl_data.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(426, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Ch1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(476, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Ch2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(526, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Ch3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(576, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Ch4";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.text_pwmfreq);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.text_duty);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_status);
            this.panel1.Controls.Add(this.samplesTextbox);
            this.panel1.Controls.Add(this.text_pwm);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.checkBox4);
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.startButton);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(12, 281);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 151);
            this.panel1.TabIndex = 22;
            // 
            // text_pwmfreq
            // 
            this.text_pwmfreq.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.text_pwmfreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.text_pwmfreq.Location = new System.Drawing.Point(402, 49);
            this.text_pwmfreq.Name = "text_pwmfreq";
            this.text_pwmfreq.Size = new System.Drawing.Size(168, 21);
            this.text_pwmfreq.TabIndex = 35;
            this.text_pwmfreq.Text = "Pwm Freq: 0 Khz";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkT1);
            this.groupBox1.Controls.Add(this.checkT4);
            this.groupBox1.Controls.Add(this.checkT16);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(260, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 85);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time Div";
            // 
            // checkT1
            // 
            this.checkT1.AutoSize = true;
            this.checkT1.Location = new System.Drawing.Point(7, 62);
            this.checkT1.Name = "checkT1";
            this.checkT1.Size = new System.Drawing.Size(95, 17);
            this.checkT1.TabIndex = 2;
            this.checkT1.Text = "T2_DIV_BY_1";
            this.checkT1.UseVisualStyleBackColor = true;
            this.checkT1.CheckedChanged += new System.EventHandler(this.checkT1_CheckedChanged);
            // 
            // checkT4
            // 
            this.checkT4.AutoSize = true;
            this.checkT4.Location = new System.Drawing.Point(7, 40);
            this.checkT4.Name = "checkT4";
            this.checkT4.Size = new System.Drawing.Size(95, 17);
            this.checkT4.TabIndex = 1;
            this.checkT4.Text = "T2_DIV_BY_4";
            this.checkT4.UseVisualStyleBackColor = true;
            this.checkT4.CheckedChanged += new System.EventHandler(this.checkT4_CheckedChanged);
            // 
            // checkT16
            // 
            this.checkT16.AutoSize = true;
            this.checkT16.Location = new System.Drawing.Point(7, 20);
            this.checkT16.Name = "checkT16";
            this.checkT16.Size = new System.Drawing.Size(101, 17);
            this.checkT16.TabIndex = 0;
            this.checkT16.Text = "T2_DIV_BY_16";
            this.checkT16.UseVisualStyleBackColor = true;
            this.checkT16.CheckedChanged += new System.EventHandler(this.checkT16_CheckedChanged);
            // 
            // text_duty
            // 
            this.text_duty.Location = new System.Drawing.Point(106, 111);
            this.text_duty.Multiline = true;
            this.text_duty.Name = "text_duty";
            this.text_duty.Size = new System.Drawing.Size(45, 23);
            this.text_duty.TabIndex = 33;
            this.text_duty.Text = "64";
            // 
            // text_pwm
            // 
            this.text_pwm.Location = new System.Drawing.Point(106, 73);
            this.text_pwm.Multiline = true;
            this.text_pwm.Name = "text_pwm";
            this.text_pwm.Size = new System.Drawing.Size(45, 23);
            this.text_pwm.TabIndex = 32;
            this.text_pwm.Text = "128";
            // 
            // button6
            // 
            this.button6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button6.Location = new System.Drawing.Point(157, 111);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(97, 23);
            this.button6.TabIndex = 31;
            this.button6.Text = "DUTY (-)";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button5
            // 
            this.button5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button5.Location = new System.Drawing.Point(3, 111);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(97, 23);
            this.button5.TabIndex = 30;
            this.button5.Text = "DUTY (+)";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button4
            // 
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(157, 73);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 23);
            this.button4.TabIndex = 29;
            this.button4.Text = "PWM (-)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button3
            // 
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(3, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "PWM (+)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(3, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 28);
            this.button2.TabIndex = 27;
            this.button2.Text = "Start Pwm";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 444);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.lbl_data);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.scopeControl);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Instruments.ScopeControl scopeControl;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.TextBox samplesTextbox;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label lbl_data;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox text_duty;
        private System.Windows.Forms.TextBox text_pwm;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkT1;
        private System.Windows.Forms.CheckBox checkT4;
        private System.Windows.Forms.CheckBox checkT16;
        private System.Windows.Forms.Label text_pwmfreq;
	}
}

