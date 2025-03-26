using System.Management;

namespace ManagerApplication.Helper
{
    public class MyHard
    {
        public static string GetHardwareId()
        {
            string serialNumber = "Unknown";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive");
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                serialNumber = wmi_HD["SerialNumber"].ToString();
                break;
            }
            return serialNumber;
        }
    }
}
