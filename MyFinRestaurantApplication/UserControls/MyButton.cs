using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace ManagerApplication.UserControls
{
    public partial class MyButton : UserControl
    {
        public MyButton()
        {
            InitializeComponent();
        }
        public Guna2Button button { get { return Button; } }
    }
}
