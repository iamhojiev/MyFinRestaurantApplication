using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class AddKitchen : Form
    {

        private Kitchen _kitchen = null;
        public AddKitchen(Kitchen kitchen = null)
        {
            InitializeComponent();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Items.Add(printer);
            }

            if (kitchen != null)
            {
                _kitchen = kitchen;
                txtTitle.Text = "Редактирование данных";
                txtName.Text = _kitchen.kitchen_name;
                cmbPrinter.SelectedItem = _kitchen.kitchen_printer;
            }
            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                if (cmbPrinter.SelectedItem != null)
                {
                    try
                    {
                        var printerName = cmbPrinter.SelectedItem.ToString();
                        if (_kitchen != null)
                        {
                            _kitchen.kitchen_name = txtName.Text;
                            _kitchen.kitchen_printer = printerName;
                            if (await new Kitchen().OnUpdateAsync(_kitchen))
                            {
                                Dialog.Info("Данные успешно обновлены!");
                                DialogResult = DialogResult.OK;
                            }
                        }
                        else
                        {
                            var newKitchen = new Kitchen()
                            {
                                kitchen_name = txtName.Text,
                                kitchen_printer = printerName,
                            };
                            if (await new Kitchen().OnInsertAsync(newKitchen))
                            {
                                Dialog.Info("Данные успешно добавлены!");
                                DialogResult = DialogResult.OK;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Dialog.Error(ex.Message.ToString());
                    }
                }
                else
                    Dialog.Error("Принтер не выбран");
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                btnSave.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

