using System;
using System.Linq;
using System.Windows.Forms;
using ManagerApplication.Model;
using System.Collections.Generic;

namespace ManagerApplication
{
    public partial class SeeRecipe : Form
    {
        private readonly List<Recipe> recipes = new List<Recipe>();

        public SeeRecipe(Product product)
        {
            InitializeComponent();
            dgvRecipe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            InitForm(product);
        }

        private async void InitForm(Product product)
        {
            var recipe = await new Recipe().OnSelectProdRecipeAsync(product.prod_id);
            var stock = await new Stock().OnLoadAsync();
            var types = await new Model.Type().OnLoadAsync();

            foreach (var i in recipe)
            {
                i.stock = stock.FirstOrDefault(u => u.stock_id == i.recipe_stock);
                i.type = types.FirstOrDefault(u => u.type_id == i.recipe_value);
                recipes.Add(i);
            }
            dgvRecipe.DataSource = recipes;
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

