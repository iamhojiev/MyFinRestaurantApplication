using ManagerApplication.Helper;
using ManagerApplication.Model;
using ManagerApplication.DataBase;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class AddCard : Form
    {
        private Card _card;
        public AddCard(Card card = null)
        {
            InitializeComponent();

            _card = card;

            if (card != null)
            {
                txtTitle.Text = "Редактирование данных";
                txtPrice.Text = card.card_balance.ToString();
                txtName.Text = card.card_name;
                txtPrice.Enabled = false;
                var imageName = _card.card_name + _card.card_id + ".png";
                var image = ImageServerIO.GetProductImage(imageName);
                if (image != null)
                    pictureBox1.Image = image;
            }

            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                try
                {
                    if (_card != null)
                    {
                        _card.card_name = txtName.Text;
                        _card.card_balance = Convert.ToDouble(txtPrice.Text);

                        if (await new Card().OnUpdateAsync(_card))
                        {
                            var imageName = _card.card_name + _card.card_id;
                            ImageServerIO.SavePngImageToServer(pictureBox1.Image, imageName, true);
                            Dialog.Info("Данные успешно обновлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        var newCard = new Card()
                        {
                            card_name = txtName.Text,
                            card_balance = Convert.ToDouble(txtPrice.Text),
                        };

                        if (await new Card().OnInsertAsync(newCard))
                        {
                            var imageName = newCard.card_name + newCard.card_id;
                            ImageServerIO.SavePngImageToServer(pictureBox1.Image, imageName, true);
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
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // Заменяем введённый символ на десятичный разделитель текущей культуры
                e.KeyChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "Image files(*.jpeg *.jpg *.png) | *.jpeg;*.jpg*;*.png;"
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(fd.FileName);
            }
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

