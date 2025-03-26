
using ManagerApplication.Forms;


namespace ManagerApplication.Helper
{
    public static class Dialog
    {
        public static void Info(string text)
        {
            Alert alert_Form = new Alert();
            alert_Form.ShowAlert(text, EnumAlertType.Success);
        }
        public static void Error(string text)
        {
            Alert alert_Form = new Alert();
            alert_Form.ShowAlert(text, EnumAlertType.Error);
        }
    }
}
