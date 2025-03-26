using System;
using System.Linq;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using ManagerApplication.Properties;

namespace ManagerApplication
{
    public partial class FrmDumper : Form
    {
        private readonly int intervalMinutes;
        private readonly User dbUser = new User();

        public FrmDumper()
        {
            InitializeComponent();
            RateType myRateType = new RateType().OnLoad().FirstOrDefault(r => r.rate_id == Settings.Default.rate_type);
            intervalMinutes = myRateType.rate_interval_hour * 60;
            CheckLastDumpDate();
        }

        private void CheckLastDumpDate()
        {
            DateTime lastDumpDate = GetLastDumpDate();
            double minutesPassed = (DateTime.Now - lastDumpDate).TotalMinutes;

            if (minutesPassed > intervalMinutes || minutesPassed < 0)
            {
                DoDump();
            }
            else if (minutesPassed > 0)
            {
                SetTimerInterval(intervalMinutes - minutesPassed);
            }
        }

        private DateTime GetLastDumpDate()
        {
            if (DateTime.TryParse(Settings.Default.last_dump, out DateTime lastDumpDate))
            {
                return lastDumpDate;
            }

            RecordLastDumpDate();
            return DateTime.Now;
        }

        private async void DoDump()
        {
            if (!string.IsNullOrEmpty(Settings.Default.dump_path))
            {
                if (await dbUser.ExportDump(Settings.Default.dump_path))
                {
                    FileArchiver.CreateZipArchive(Settings.Default.dump_path, Settings.Default.zip_password, true);
                    RecordLastDumpDate();
                    SetTimerInterval(intervalMinutes);
                }
            }
        }

        private void RecordLastDumpDate()
        {
            Settings.Default.last_dump = DateTime.Now.ToString();
            Settings.Default.Save();
        }

        private void SetTimerInterval(double minutes)
        {
            timer1.Interval = (int)(minutes * 60000);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DoDump();
        }
    }
}
