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
        Boolean serialon = false,adcon=false,pwmon=false;
		List<TextBox> textBoxes = new List<TextBox>();
        UsbHidDevice my_hid = new UsbHidDevice();
        short VendorID = short.Parse("1111", NumberStyles.HexNumber);
        short ProductID = short.Parse("1111", NumberStyles.HexNumber);
        byte[] str_buffer = new byte[8];
        byte[] int_buffer = new byte[8];
        byte[] received_buffer = new byte[64];
		int samples = 10;
		int samplesGenerated = 0;
        int pwm = 255;
        int duty = 128;
        int timediv = 16;
        int data1 = 0, data2 = 0, data3 = 0, data4 = 0, data5 = 0, data6 = 0, data7 = 0, data8 = 0, data9 = 0, data10 = 0, data11 = 0, data12 = 0;
        int xtalfreq = 48000000;        
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
                SendData("c",0);               
                lbl_status.Text = "Connected to PIC";               
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
		        if(pwmon)
                 SendData("s",0);               
                SendData("e",0);                       
                my_hid.ClosePipe();
                button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;
                lbl_status.Text = "Disconnected";
                button2.Text = "Start Pwm";
                startButton.Text = "Start";          
			}
		}
       
        int GetData()
        {
            if (my_hid.IsOpen)
            {
                try
                {
                    my_hid.ReadPipe(ref received_buffer, 128, TransactionType.Interrupt);
                    return received_buffer[2] * 256 + received_buffer[1];                   
                }
                catch { }
            }
            return 0;
        }
		private void updateTimer_Tick(object sender, EventArgs e) {                        
                        try
                        {
                            if (!serialon)
                            {
                                if (my_hid.IsOpen)
                                        setscope();
                            }else
                                SendData("r", 0);
                        }
                        catch { }
		}
        int i;
        String GetStrData()
        {          
            my_hid.ReadPipe(ref received_buffer, 128, TransactionType.Interrupt); 
            string hex = BitConverter.ToString(received_buffer);
            string[] hexValuesSplit = hex.Split('-');            
            char[] c = new char[128];
            i=0;
            foreach (String hexvalue in hexValuesSplit)
            { 
                try
                {
                    if (!IsNullOrDefault(hexvalue))
                    {
                        long num = long.Parse(hexvalue.Trim(), NumberStyles.AllowHexSpecifier);
                        if (!IsNullOrDefault(num))
                            c[i] = (char)num;
                        else
                            c[i] = (char)'X';
                    }
                }
                catch { }
                i++;
            }
            string[] adcvalue = new string(c).Trim().Split('X');
            return adcvalue[1].Trim();             
        }
        static bool IsNullOrDefault<T>(T value)
        {
            return object.Equals(value, default(T));
        }
        private void setscope()
       {
           int i, samplesToGen = (int)((uTimer.ElapsedMilliseconds * samples) / (eqTime * 1000));
           SendData("u", 0);
          
           string strdata = GetStrData();
           string[] adcdata = strdata.Split(';');
           lbl_chnl1.Text = strdata;
           if (checkBox1.Checked)
               data1 =Convert.ToInt32(adcdata[0]);
           if (checkBox2.Checked)
               data2 = Convert.ToInt32(adcdata[1]);
           if (checkBox3.Checked)
               data3 = Convert.ToInt32(adcdata[2]);
           if (checkBox4.Checked)
               data4 = Convert.ToInt32(adcdata[3]);
           if (checkBox5.Checked)
               data5 = Convert.ToInt32(adcdata[4]);
           if (checkBox6.Checked)
               data6 = Convert.ToInt32(adcdata[5]);
           if (checkBox7.Checked)
               data7 = Convert.ToInt32(adcdata[6]);
           if (checkBox8.Checked)
               data8 = Convert.ToInt32(adcdata[7]);
           if (checkBox9.Checked)
               data9 = Convert.ToInt32(adcdata[8]);
           if (checkBox10.Checked)
               data10 = Convert.ToInt32(adcdata[9]);
           if (checkBox11.Checked)
               data11 = Convert.ToInt32(adcdata[10]);
           if (checkBox12.Checked)
               data12 = Convert.ToInt32(adcdata[11]);
           lbl_chnl1.Text = String.Format("{0:0.00 V}", (float)data1 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data2 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data3 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data4 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data5 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data6 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data7 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data8 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data9 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data10 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data11 * 5 / 1023) + " " +
                           String.Format("{0:0.00 V}", (float)data12 * 5 / 1023);
           for (i = samplesGenerated; i < samplesToGen; i++)
           {
               // begin the collection process
               scopeControl.BeginAddPoint();
               double tm = i * (eqTime / 10);
               scopeControl.AddPoint(0, (float)data1 * 5 / 1023);
               scopeControl.AddPoint(1, (float)data2 * 5 / 1023);
               scopeControl.AddPoint(2, (float)data3 * 5 / 1023);
               scopeControl.AddPoint(3, (float)data4 * 5 / 1023);
               scopeControl.AddPoint(4, (float)data5 * 5 / 1023);
               scopeControl.AddPoint(5, (float)data6 * 5 / 1023);
               scopeControl.AddPoint(6, (float)data7 * 5 / 1023);
               scopeControl.AddPoint(7, (float)data8 * 5 / 1023);
               scopeControl.AddPoint(8, (float)data9 * 5 / 1023);
               scopeControl.AddPoint(9, (float)data10 * 5 / 1023);
               scopeControl.AddPoint(10, (float)data11 * 5 / 1023);
               scopeControl.AddPoint(11, (float)data12 * 5 / 1023);  
               scopeControl.EndAddPoint();
           }
           samplesGenerated = i;   
       }
		private void Form1_Load(object sender, EventArgs e) {            
			Color [] colors = new Color[13];
            colors[0] = Color.Gold;
			colors[1] = Color.White;
            colors[2] = Color.Blue;
            colors[3] = Color.Red;
            colors[4] = Color.Yellow;
            colors[5] = Color.Turquoise;
            colors[6] = Color.Thistle;
            colors[7] = Color.Pink;
            colors[8] = Color.Peru;
            colors[9] = Color.Coral;
            colors[10] = Color.Crimson;
            colors[11] = Color.DarkCyan;
            colors[12] = Color.Violet; 
			
			for ( int i = 0; i < 12; i++ ) {

				Trace d = new Trace();
				d.TraceColor = colors[i];
				scopeControl.Traces.Add(d);               

			}
            scopeControl.Traces[0].Visible = true;
            scopeControl.Traces[1].Visible = false;
            scopeControl.Traces[2].Visible = false;
            scopeControl.Traces[3].Visible = false;
            scopeControl.Traces[4].Visible = false;
            scopeControl.Traces[5].Visible = false;
            scopeControl.Traces[6].Visible = false;
            scopeControl.Traces[7].Visible = false;
            scopeControl.Traces[8].Visible = false;
            scopeControl.Traces[9].Visible = false;
            scopeControl.Traces[10].Visible = false;
            scopeControl.Traces[11].Visible = false;
           
			trackBar1_Scroll(null, null);
            trackBar2_Scroll(null, null);
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

			
		private void trackBar1_Scroll(object sender, EventArgs e) {

			// neat algorithm: use logarithms to establish a base value
			// (1us, 10us, 100us, 1ms .. ), then double it modulus amount 
			// of times to get the intermediate values (1x,2x,4x)

			scopeControl.MicroSecondsPerUnit =
				(int)Math.Pow(10, trackBar1.Value / 3) *
				(int)Math.Pow(2, trackBar1.Value % 3);

		}	

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(button2.Text) == "Start Pwm")
            {
                pwmon = true;
                button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = true;    
                if (checkT16.Checked)             
                    SendData("p", 16);
                else if (checkT4.Checked)
                    SendData("p", 4);
                else if (checkT1.Checked)
                    SendData("p", 1);

                button2.Text = "Stop Pwm";
                lbl_status.Text = "PWM Started";
                text_pwmfreq.Text = "Pwm Freq: " + String.Format("{0:0.000 Khz}", getFreq(pwm, timediv));
            }
            else
            {
                pwmon = false;
                button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;
                SendData("s",0);
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
            SendData("f", pwm);
            SendData("d", duty);        
            updatePwmText(Convert.ToString(pwm));
            updateDutyText(Convert.ToString(duty));
            text_pwmfreq.Text = "Pwm Freq: " + String.Format("{0:0.000 Khz}", getFreq(pwm, timediv));
        }

        private void button4_Click_1(object sender, EventArgs e)
        {            
            pwm = Convert.ToInt32(text_pwm.Text) - 1;
            if (pwm < 2)
                pwm = 2;
            duty = pwm / 2;
            SendData("f", pwm);
            SendData("d", duty);
            updatePwmText(Convert.ToString(pwm));
            updateDutyText(Convert.ToString(duty));
            text_pwmfreq.Text = "Pwm Freq: " + String.Format("{0:0.000 Khz}", getFreq(pwm, timediv));
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            duty = Convert.ToInt32(text_duty.Text) + 1;
            if (duty > 255)
                duty = 255;
            SendData("f", pwm);
            SendData("d", duty);
            updatePwmText(Convert.ToString(pwm));
            updateDutyText(Convert.ToString(duty));
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            duty = Convert.ToInt32(text_duty.Text) - 1;
            if (duty < 0)
                duty = 0;
            SendData("f", pwm);
            SendData("d", duty);
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
                SendData("p", 16);
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
                SendData("p", 4);
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
                SendData("p", 1);
                timediv = 1;
                text_pwmfreq.Text = String.Format("{0:0.### Khz}", getFreq(pwm, timediv));
            }
        }
        private double getFreq(int pwnfreq, int timediv)
        {
            return (double)(xtalfreq / (timediv * (pwnfreq + 1) * 4))/1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialon)
            {               
                serialon = true;
                uTimer.Reset();
                uTimer.Start();
                updateTimer.Interval = (int)(eqTime * 1000);
                updateTimer.Start();
                button1.Text = "Serial Off";
            }
            else
            {                
                serialon = false;
                uTimer.Stop();
                updateTimer.Stop();
                button1.Text = "Serial On";
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                scopeControl.Traces[i].MilliPerUnit =
                  (int)Math.Pow(10, trackBar2.Value / 3) *
                  (int)Math.Pow(2, trackBar2.Value % 3);
            }

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[0].Visible = !scopeControl.Traces[0].Visible;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[1].Visible = !scopeControl.Traces[1].Visible;
        }	
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[2].Visible = !scopeControl.Traces[2].Visible;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[3].Visible = !scopeControl.Traces[3].Visible;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[4].Visible = !scopeControl.Traces[4].Visible;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[5].Visible = !scopeControl.Traces[5].Visible;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[6].Visible = !scopeControl.Traces[6].Visible;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[7].Visible = !scopeControl.Traces[7].Visible;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[8].Visible = !scopeControl.Traces[8].Visible;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[9].Visible = !scopeControl.Traces[9].Visible;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[10].Visible = !scopeControl.Traces[10].Visible;
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            scopeControl.Traces[11].Visible = !scopeControl.Traces[11].Visible;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            checkBox1.Checked=false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            data1 = data2 =data3 =data4 = data5 = data6 =data7 = data8 = data9 = data10 = data11 = data12 = 0;
            lbl_chnl1.Text = "";
        }

      

        private void button9_Click(object sender, EventArgs e)
        {
            if (!adcon)
            {
                adcon = true;
                scopeControl.Start();
                samplesGenerated = 0;
                uTimer.Reset();
                uTimer.Start();
                updateTimer.Interval = (int)(eqTime * 1000);
                updateTimer.Start();
                button9.Text = "Adc Off";
            }
            else
            {
                adcon = false;
                uTimer.Stop();
                updateTimer.Stop();
                scopeControl.Stop();
                button9.Text = "Adc On";
            }
        }

	}
}