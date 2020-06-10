using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    public partial class TrafficLights : Form
    {

        private Timer timerSwitch = null;
        private Timer timerBlink = null;
        private int timeCounter = 0;
        private int hou = 0, min = 0, sec = 0;

        private PictureBox lightToBlink = null;
        private Color colorToCheck = Color.Gray;
        private Label labelTime = null;



        public TrafficLights()
        {
            InitializeComponent();
            InitializeTrafficLights();
            InitializeLabelTime();
            InitializeTimerSwitch();
            InitializeTimerBlink();
            InitializeBoxForm();  
        }

        private void InitializeLabelTime()
        {
            labelTime = new Label();
            labelTime.Font = new Font("Tahoma", 18, FontStyle.Bold);
            labelTime.Width = 150;
            labelTime.Height = 50;
            labelTime.Top = 20;
            labelTime.Left = 50;
            labelTime.Text = "00:00:00";
            this.Controls.Add(labelTime);
        }

        private void InitializeTimerSwitch()
        {
            timerSwitch = new Timer();
            timerSwitch.Interval = 1000;
            timerSwitch.Tick += new EventHandler(TimerSwitch_Tick);
            timerSwitch.Start();
        }

        private void InitializeTimerBlink()
        {
            timerBlink = new Timer();
            timerBlink.Interval = 200;
            timerBlink.Tick += new EventHandler(TimerBlink_Tick);
        }

        private void TimerBlink_Tick(object sender, EventArgs e)
        {
            if (lightToBlink.BackColor == Color.Gray)
            {
                lightToBlink.BackColor = colorToCheck;
            }
            else
            {
                lightToBlink.BackColor = Color.Gray;
            }
        }

        private void StartBlinking (PictureBox light, Color color)
        {
            timerBlink.Start();
            lightToBlink = light;
            colorToCheck = color;
        }

        private void StopBliniking()
        {
            timerBlink.Stop();
        }
        private void TimerSwitch_Tick(object sender, EventArgs e)
        {
            UpdateClock();
            SwitchLights();
            UpdateLabelTime();

           /* if (timeCounter == 0)
            {
                RedLight.BackColor = Color.Red; //red on
            }
            else if (timeCounter == 5)
            {
                RedLight.BackColor = Color.Gray; //red off
                YellowLight.BackColor = Color.Yellow; //yellow on
            }
            else if (timeCounter == 7)
            {
                YellowLight.BackColor = Color.Gray; //yellow off
                GreenLight.BackColor = Color.Green; //green on
            }
            else if (timeCounter == 12)
            {
                GreenLight.BackColor = Color.Gray; //green off
                YellowLight.BackColor = Color.Yellow; //yellow on
            }
            else if (timeCounter == 14)
            {
                YellowLight.BackColor = Color.Gray;
                RedLight.BackColor = Color.Red;
                timeCounter = -1;
            }
                    timeCounter++; */
        }



        private void UpdateClock()
        {
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 0;
            }
            if (min == 60)
            {
                hou++;
                min = 0;
            }
            if (hou == 100)
            {
                hou = 0;
            }
        }

        private void UpdateLabelTime()
        {
            labelTime.Text = $"{hou:00}:{min:00}:{sec:00}";
        }

        private void ResetClock()
        {
            sec = 0;
            min = 0;
            hou = 0;
        }

        private void SwitchLights()
        {
            switch (timeCounter)
            {
                case 0:
                    RedLight.BackColor = Color.Red;
                    break;
                case 3:
                    YellowLight.BackColor = Color.Yellow;
                   // RedLight.BackColor = Color.Gray;
                    break;
                case 5:
                    RedLight.BackColor = Color.Gray;
                    YellowLight.BackColor = Color.Gray;
                    GreenLight.BackColor = Color.Green;
                    break;
                case 6:
                    StartBlinking(GreenLight, Color.Green);
                    //lightToBlink = GreenLight;
                    // colotToCheck = Color.Green;
                   // timerBlink.Start();
                    break;
                case 8:
                    StopBliniking();
                   // timerBlink.Stop();
                    YellowLight.BackColor = Color.Yellow;
                    GreenLight.BackColor = Color.Gray;
                    break;
                case 10:
                    YellowLight.BackColor = Color.Gray;
                    RedLight.BackColor = Color.Red;
                    timeCounter = -1;
                    ResetClock();
                    break;
            }
            timeCounter++;
        }

        private void InitializeTrafficLights()
        {
            RedLight.BackColor = Color.Gray;
            YellowLight.BackColor = Color.Gray;
            GreenLight.BackColor = Color.Gray;
        }

        private void InitializeBoxForm()
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, RedLight.Width, RedLight.Height);
            RedLight.Region = new Region(path);

            System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
            path2.AddEllipse(0, 0, YellowLight.Width, YellowLight.Height);
            YellowLight.Region = new Region(path2);

            System.Drawing.Drawing2D.GraphicsPath path3 = new System.Drawing.Drawing2D.GraphicsPath();
            path3.AddEllipse(0, 0, GreenLight.Width, GreenLight.Height);
            GreenLight.Region = new Region(path3);
        }
    }
}
