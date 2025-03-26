using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using Guna.UI2.WinForms;
using System.Collections.Generic;
using ManagerApplication.UserControls;

namespace ManagerApplication
{
    public partial class CassaSelect : Form
    {
        public Cassa selectedCassa;
        private string currency;
        public CassaSelect()
        {
            InitializeComponent();
            UpdateCard();
        }

        private async void UpdateCard()
        {
            currency = await new Currency().OnGetCurrencyValueAsync(); 
            var cassa = await new Cassa().OnLoadAsync();
            CreateTableLayoutGroup(cassa, mainContainer, cassa.Count);
        }

        public void CreateTableLayoutGroup(List<Cassa> cassa, Control parent, int count)
        {
            // Проверяем, что количество usercontrol больше нуля
            if (count <= 0)
            {
                return;
            }

            // Создаем tablelayoutgroup
            TableLayoutPanel tlg = new TableLayoutPanel();
            // Вычисляем количество строк и столбцов
            int rowCount = (count - 1) / 3 + 1; // округляем вверх
            int columnCount = Math.Min(count, 3); // не больше трех
                                                  // Задаем количество строк и столбцов
            tlg.RowCount = rowCount;
            tlg.ColumnCount = columnCount;
            // Задаем размеры строк и столбцов в процентах
            for (int i = 0; i < rowCount; i++)
            {
                tlg.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));
            }
            for (int i = 0; i < columnCount; i++)
            {
                tlg.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnCount));
            }
            tlg.Dock = DockStyle.Fill;
            parent.Controls.Add(tlg);


            MyButton myTable;
            for (int j = 0; j < cassa.Count; j++)
            {
                var i = cassa[j];

                myTable = new MyButton();
                myTable.Dock = DockStyle.Fill;
                myTable.button.Tag = i;
                myTable.button.Click += CardBtnClick;
                myTable.button.Text = string.Format("{0}\nБаланс: {1} {2}", i.cassa_name, i.cassa_money, currency);

                int row = j / 3; // целочисленное деление
                int column = j % 3; // остаток от деления
                tlg.Controls.Add(myTable, column, row);
            }
        }

        private void CardBtnClick(object sender, EventArgs e)
        {
            if (((Guna2Button)sender).Tag is Cassa aaa)
            {
                selectedCassa = aaa;
                DialogResult = DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnClose.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

