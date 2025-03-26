using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageEntryInfo : Form
    {
        private Entry myEntry;

        public PageEntryInfo(Entry entry)
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            txtInfo.Text = $"Приход №{entry.entry_id} от {entry.entry_date}, Сумма: {entry.entry_summa}";
            myEntry = entry;
            UpdateGrid();
        }

        private void UpdateAmountTxt()
        {
            txtAmount.Text = $"Осталось заплатить: {myEntry.GetEntryDebt}";
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var logs = await new EntryLog().OnSelectEntryTransactionsAsync(myEntry.entry_id);
            var users = await new User().OnAllUserAsync();

            foreach (var i in logs)
            {
                i.user = users.FirstOrDefault(u => u.user_id == i.transaction_user);
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
            UpdateAmountTxt();
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            if (myEntry.GetEntryDebt > 0)
            {
                PayEntry payEntry = new PayEntry(myEntry);
                if (payEntry.ShowDialog() == DialogResult.OK)
                {
                    UpdateGrid();
                    Dialog.Info("Операция прошла успешно!");
                }
            }
            else
            {
                Dialog.Error("Данное поступление не имеет долга!");
            }
        }

        private async void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count == 0)
            {
                Dialog.Error("Вы не выбрали платеж");
                return;
            }

            var entryLog = (EntryLog)dgvMain.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show(
                "Вы действительно хотите удалить платеж?", "Удаление",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await BalanceSystem.Instance.DeleteTransaction(entryLog);

                myEntry.entry_paid -= entryLog.transaction_amount;
                await new Entry().OnUpdateAsync(myEntry);

                var vendor = await new Vendor().OnSelectAsync(myEntry.entry_vendor);
                vendor.vendor_debt += entryLog.transaction_amount;
                await new Vendor().OnUpdateAsync(vendor);

                UpdateGrid();
                UpdateAmountTxt();

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                btnAddEntry.PerformClick();
                return true;
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                btnDeleteEntry.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}


