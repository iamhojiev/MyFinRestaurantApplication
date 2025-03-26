using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerApplication.Forms
{
    public partial class ColumnsSettingsForm : Form
    {
        private DataGridView _dgv;
        private Dictionary<string, bool> _columnSettings;

        public bool IsSettingsSaved { get; private set; }

        public ColumnsSettingsForm(DataGridView dgv)
        {
            InitializeComponent();
            _dgv = dgv;
            LoadColumnSettings();
        }

        private void LoadColumnSettings()
        {
            checkedListBoxColumns.Items.Clear();
           _columnSettings = _dgv.Columns.Cast<DataGridViewColumn>()
                .ToDictionary(c => c.HeaderText, c => c.Visible);

            foreach (var column in _columnSettings)
            {
                checkedListBoxColumns.Items.Add(column.Key, column.Value);
            }
        }

        public Dictionary<string, bool> GetColumnSettings()
        {
            return _columnSettings;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxColumns.Items.Count; i++)
            {
                var columnName = checkedListBoxColumns.Items[i].ToString();
                _columnSettings[columnName] = checkedListBoxColumns.GetItemChecked(i);
            }

            IsSettingsSaved = true;
            Close();
        }
    }

}
