using ManagerApplication.Properties;
using ManagerApplication.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagerApplication.Forms
{
    public partial class Alert : Form
    {
        public Alert()
        {
            InitializeComponent();
        }

        public enum State
        {
            Wait,
            Start,
            Close
        };

        private State state;
        //private MessageType type;

        private void btnCloseClick(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            state = State.Close;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Wait:
                    timer1.Interval = 3000;
                    state = State.Close;
                    break;
                case State.Start:
                    timer1.Interval = 1;
                    Opacity += 0.1;
                    if (x < Location.X)
                    {
                        Left--;
                    }
                    else
                    {
                        if (Opacity == 1)
                        {
                            state = State.Wait;
                        }
                    }
                    break;
                case State.Close:
                    timer1.Interval = 1;
                    Opacity -= 0.1;
                    Left -= 3;
                    if (Opacity == 0.0)
                    {
                        Close();
                    }
                    break;
            }
        }

        private int x, y;
        public void ShowAlert(string message, EnumAlertType type)
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            string formName;
            for (int i = 1; i < 10; i++)
            {
                formName = "alert" + i.ToString();
                Alert alertForm = (Alert)Application.OpenForms[formName];

                if (alertForm == null)
                {
                    Name = formName;
                    x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    Location = new Point(x, y);
                    break;
                }
            }

            x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            switch (type)
            {
                case EnumAlertType.Success:
                    picIcon.Image = Resources.SuccessFill;
                    BackColor = Color.DarkGreen;
                    break;
                case EnumAlertType.Info:
                    picIcon.Image = Resources.InfoFill;
                    BackColor = Color.DarkOrange;
                    break;
                case EnumAlertType.Error:
                    picIcon.Image = Resources.WarningFill;
                    BackColor = Color.DarkRed;
                    break;
            }
            lblDescription.Text = message;

            this.Show();
            state = State.Start;
            timer1.Interval = 1;
            timer1.Start();
        }
    }
}
