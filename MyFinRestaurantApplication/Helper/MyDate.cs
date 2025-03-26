using System;

namespace ManagerApplication.Helper
{
    static class MyDate
    {
        public static string DateFormat()
        {
            return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }
    }
}
