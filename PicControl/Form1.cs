using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Instruments;
using dotMath;
using USBManagement;
using System.Globalization;
namespace ScopeTest {
	public partial class Form1 : Form {

		List<TextBox> textBoxes = new List<TextBox>();
        UsbHidDevice my_hid = new UsbHidDevice();
        short VendorID = short.Parse("1111", NumberStyles.HexNumber);
        short ProductID = short.Parse("1111", NumberStyles.HexNumber);
        byte[] str_buffer = new byte[8];
        byte[] int_buffer = new byte[8];
        byte[] received_buffer = new byte[8];
		int samples = 10;
		int samplesGenerated = 0;
        int pwm = 128;
        int duty = 64;
        int timediv = 16;
        int xtalfreq = 20000000;
		double eqTime = 0.025;
		public Form1() {
			InitializeComponent();
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;
		}
        System.Diagnostics.Stopwatch uTimer = new System.Diagnostics.Stopwatch();
        void connect()
        {
            if (my_hid.OpenPipe(VendorID, ProductID))
            {
                SendData("C",0);               
                lbl_status.Text = "Connected to PIC";
                scopeControl.Start();
                samplesGenerated = 0;
                uTimer.Reset();
                uTimer.Start();
                updateTimer.Interval = (int)(eqTime * 1000);
                updateTimer.Start();
                button2.Enabled = true;
                startButton.Text = "Stop";
            }
            else
            {
                lbl_status.Text = "Disconnected";
            }
        }     
		private void startButton_Click(object sender, EventArgs e) {
			if ( startButton.Text == "Start" ) {              	
                connect();               
			} else {
				uTimer.Stop();
				updateTimer.Stop();
				scopeControl.Stop();
                SendData("S",0);               
                SendData("E",0);                       
                my_hid.ClosePipe();
                button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;
                lbl_status.Text = "Disconnected";
                button2.Text = "Start Pwm";
                startButton.Text = "Start";          
			}
		}
        int data=0;
        void GetData()
        {
            if (my_hid.IsOpen)
            {
                try
                {
                    my_hid.ReadPipe(ref received_buffer, 128, TransactionType.Interrupt);
                    data = received_buffer[2] * 256 + received_buffer[1];
                    updateReadText(Convert.ToString((float)data * 5 / 1023));
                }
                catch { }
            }
        }
        
        
        delegate void updateReadTextDelegate(string newText);
        private void updateReadText(string newText)
        {
            if (lbl_data.InvokeRequired)
            {
                updateReadTextDelegate del = new updateReadTextDelegate(updateReadText);
                lbl_data.Invoke(del, new object[] { newText.Trim() });
            }
            else
            {
                lbl_data.Text = newText.Trim();
            }
        }
		
		private void updateTimer_Tick(object sender, EventArgs e) {                        
                        try
                        {
                            SendData("A",0);
                            GetData();
                            int i, samplesToGen = (int)((uTimer.ElapsedMilliseconds * samples) / (eqTime * 1000));
                            for (i = samplesGenerated; i < samplesToGen; i++)
                            {

                                // begin the collection process
                                scopeControl.BeginAddPoint();
                                double tm = i * (eqTime / 10);
                                scopeControl.AddPoint(0, (float) data * 5 / 1023);                                
                                scopeControl.EndAddPoint();
                            }
                            samplesGenerated = i;
                            //textBox2.Text = String.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(lineArr[2]) * 5 / 1023)));
                            //textBox3.Text = String.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(lineArr[3]) * 5 / 1023)));
                            //textBox4.Text = String.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(lineArr[4]) * 5 / 1023)));                           
                        }
                        catch { }
		}

		private void Form1_Load(object sender, EventArgs e) {            
			Color [] colors = new Color[4];

			colors[0] = Color.Gold;
			colors[1] = Color.Red;
			colors[2] = Color.Yellow;
			colors[3] = Color.Green;

			for ( int i = 0; i < 4; i++ ) {

				Trace d = new Trace();
				d.TraceColor = colors[i];
				scopeControl.Traces.Add(d);               

			}
            scopeControl.Traces[1].Visible = false;
            scopeControl.Traces[2].Visible = false;
            scopeControl.Traces[3].Visible = false;
			trackBar1_Scroll(null, null);
            checkT16.Checked = true;
		}

		private void textBox_Leave(object sender, EventArgs e) {

			EqCompiler equation;
			TextBox t = sender as TextBox;

			// validate the equation, and if its not valid, then
			// re-focus the textbox
			if ( t.Text.Trim().Equals("") ) {

				if ( t.Tag != null )
					lock ( t.Tag ) {
						t.Tag = null;
					}

				return;
			}

			try {

				equation = new EqCompiler(t.Text, true);
				equation.Compile();

				// set useful variables to something
				equation.SetVariable("e", Math.E);
				equation.SetVariable("pi", Math.PI);

				// just generically set these
				equation.SetVariable("t", 0);

				double testValue = equation.Calculate();

				// make sure theres no nonsense here
				foreach ( string var in equation.GetVariableList() )
					if ( var != "e" && var != "pi" && var != "t" )
						throw new ApplicationException(String.Format("Invalid variable \"{0}\" used in equation.", var));

				// i think this works
				if ( t.Tag == null )
					t.Tag = equation;
				else
					lock ( t.Tag ) {
						t.Tag = equation;
					}

			} catch ( Exception ex ) {
				MessageBox.Show(String.Format("Warning: Invalid equation! {0}", ex.Message), "Error");
			}
		}

		private void samples_TextChanged(object sender, EventArgs e) {
			try {
				samples = Convert.ToInt32(samplesTextbox.Text);
			} catch {
				samplesTextbox.Text = samples.ToString();
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {
			scopeControl.Traces[0].Visible = !scopeControl.Traces[0].Visible;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e) {
			scopeControl.Traces[1].Visible = !scopeControl.Traces[1].Visible;
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e) {
			scopeControl.Traces[2].Visible = !scopeControl.Traces[2].Visible;
		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e) {
			scopeControl.Traces[3].Visible = !scopeControl.Traces[3].Visible;
		}

		private void trackBar1_Scroll(object sender, EventArgs e) {

			// neat algorithm: use logarithms to establish a base value
			// (1us, 10us, 100us, 1ms .. ), then double it modulus amount 
			// of times to get the intermediate values (1x,2x,4x)

			scopeControl.MicroSecondsPerUnit =
				(int)Math.Pow(10, trackBar1.Value / 3) *
				(int)Math.Pow(2, trackBar1.Value % 3);

		}

		private void trackBar2_Scroll(object sender, EventArgs e) {
			scopeControl.Traces[0].ZeroPositionY = trackBar2.Value - 5;
		}

		private void trackBar3_Scroll(object sender, EventArgs e) {           
            scopeControl.Traces[0].MilliPerUnit =
                (int)Math.Pow(10, trackBar3.Value / 3) *
                (int)Math.Pow(2, trackBar3.Value % 3);
		}

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(button2.Text) == "Start Pwm")
            {
                text_pwm.Text = "128";
                text_duty.Text = "64";
                button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = true;    
                if (checkT16.Checked)             
                    SendData("P", 16);
                else if (checkT4.Checked)
                    SendData("P", 4);
                else if (checkT1.Checked)
                    SendData("P", 1);

                button2.Text = "Stop Pwm";
                lbl_status.Text = "PWM Started";
                text_pwmfreq.Text = "Pwm Freq: " + String.Format("{0:0.### Khz}", getFreq(pwm, timediv));
            }
            else
            {
                button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;
                SendData("S",0);
                button2.Text = "Start Pwm";
                lbl_status.Text = "PWM Stopped";
                text_pwmfreq.Text = "Pwm Freq: 0 Khz";
            }
        }
             
        delegate void updatePwmTextDelegate(string newText);
        private void updatePwmText(string newText)
        {
            if (text_pwm.InvokeRequired)
            {
                updatePwmTextDelegate del = new updatePwmTextDelegate(updatePwmText);
                text_pwm.Invoke(del, new object[] { newText.Trim() });
            }
            else
            {
                text_pwm.Text = newText.Trim();
            }
        }
        delegate void updateDutyTextDelegate(string newText);
        private void updateDutyText(string newText)
        {
            if (text_duty.InvokeRequired)
            {
                updateDutyTextDelegate del = new updateDutyTextDelegate(updateDutyText);
                text_duty.Invoke(del, new object[] { newText.Trim() });
            }
            else
            {
                text_duty.Text = newText.Trim();
            }
        }
        void SendData(String command, int value)
        {
            if (my_hid.IsOpen)
            {
                int_buffer = BitConverter.GetBytes(value);
                Encoding enc = Encoding.ASCII;
                byte[] myByteArray = enc.GetBytes(command);
                for (int i = 0; i <= command.ToString().Length - 1; i++)
                {
                    str_buffer[i] = myByteArray[i];
                }
                byte[] Out_data = new byte[3];
                Out_data[0] = str_buffer[0]; 
                Out_data[1] = int_buffer[0];               
                try
                {
                    my_hid.WritePipe(Out_data, TransactionType.Interrupt);
                }
                catch { }
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {           
            pwm=Convert.ToInt32(text_pwm.Text)+1;
            if (pwm > 255)
                pwm = 255;
            duty = pwm / 2;
            if(pwm == 255)duty=128;
            SendData("F", pwm);
            SendData("D", duty);        
            updatePwmText(Convert.ToString(pwm));
            updateDutyText(Convert.ToString(duty));
            text_pwmfreq.Text = "Pwm Freq: " + String.Format("{0:0.### Khz}", getFreq(pwm, timediv));
        }

        private void button4_Click_1(object sender, EventArgs e)
        {            
            pwm = Convert.ToInt32(text_pwm.Text) - 1;
            if (pwm < 2)
                pwm = 2;
            duty = pwm / 2;
            SendData("F", pwm);
            SendData("D", duty);
            updatePwmText(Convert.ToString(pwm));
            updateDutyText(Convert.ToString(duty));
            text_pwmfreq.Text = "Pwm Freq: " + String.Format("{0:0.### Khz}", getFreq(pwm, timediv));
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            duty = Convert.ToInt32(text_duty.Text) + 1;
            if (duty > 255)
                duty = 255;
            SendData("F", pwm);
            SendData("D", duty);
            updatePwmText(Convert.ToString(pwm));
            updateDutyText(Convert.ToString(duty));
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            duty = Convert.ToInt32(text_duty.Text) - 1;
            if (duty < 0)
                duty = 0;
            SendData("F", pwm);
            SendData("D", duty);
            updatePwmText(Convert.ToString(pwm));
            updateDutyText(Convert.ToString(duty));
        }
        private void checkT16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkT16.Checked)
            {
                checkT1.Checked = false;
                checkT4.Checked = false;
            }
            if (button2.Text == "Stop Pwm")
            {
                SendData("P", 16);
                timediv = 16;
                text_pwmfreq.Text = String.Format("{0:0.### Khz}", getFreq(pwm, timediv));
            }

        }

        private void checkT4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkT4.Checked)
            {
                checkT1.Checked = false;
                checkT16.Checked = false;
            }
            if (button2.Text == "Stop Pwm")
            {
                SendData("P", 4);
                timediv = 4;
                text_pwmfreq.Text = String.Format("{0:0.### Khz}", getFreq(pwm, timediv));
            }
        }

        private void checkT1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkT1.Checked)
            {
                checkT16.Checked = false;
                checkT4.Checked = false;
            }
            if (button2.Text == "Stop Pwm")
            {
                SendData("P", 1);
                timediv = 1;
                text_pwmfreq.Text = String.Format("{0:0.### Khz}", getFreq(pwm, timediv));
            }
        }
        private double getFreq(int pwnfreq, int timediv)
        {
            return (double)(xtalfreq / (timediv * (pwnfreq + 1) * 4))/1000;
        }
	}
}