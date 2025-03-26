namespace ManagerApplication.UserControls
{
    partial class UC_Product
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Product));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDeleteProd = new Guna.UI2.WinForms.Guna2Button();
            this.btnExcelExport = new Guna.UI2.WinForms.Guna2Button();
            this.btnInfoProduct = new Guna.UI2.WinForms.Guna2Button();
            this.btnEditProduct = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddProd = new Guna.UI2.WinForms.Guna2Button();
            this.dgvMain = new Guna.UI2.WinForms.Guna2DataGridView();
            this.productBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtFind = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbKitchen = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.prodidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodpriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodcountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodvalueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodcategoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDeleteProd
            // 
            this.btnDeleteProd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteProd.BackColor = System.Drawing.Color.White;
            this.btnDeleteProd.BorderRadius = 5;
            this.btnDeleteProd.CheckedState.Parent = this.btnDeleteProd;
            this.btnDeleteProd.CustomImages.Parent = this.btnDeleteProd;
            this.btnDeleteProd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDeleteProd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDeleteProd.ForeColor = System.Drawing.Color.White;
            this.btnDeleteProd.HoverState.Parent = this.btnDeleteProd;
            this.btnDeleteProd.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteProd.Image")));
            this.btnDeleteProd.ImageSize = new System.Drawing.Size(40, 40);
            this.btnDeleteProd.Location = new System.Drawing.Point(10, 240);
            this.btnDeleteProd.Name = "btnDeleteProd";
            this.btnDeleteProd.ShadowDecoration.Parent = this.btnDeleteProd;
            this.btnDeleteProd.Size = new System.Drawing.Size(80, 50);
            this.btnDeleteProd.TabIndex = 7;
            this.btnDeleteProd.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelExport.BackColor = System.Drawing.Color.White;
            this.btnExcelExport.BorderRadius = 5;
            this.btnExcelExport.CheckedState.Parent = this.btnExcelExport;
            this.btnExcelExport.CustomImages.Parent = this.btnExcelExport;
            this.btnExcelExport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnExcelExport.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExcelExport.ForeColor = System.Drawing.Color.White;
            this.btnExcelExport.HoverState.Parent = this.btnExcelExport;
            this.btnExcelExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelExport.Image")));
            this.btnExcelExport.ImageSize = new System.Drawing.Size(37, 37);
            this.btnExcelExport.Location = new System.Drawing.Point(10, 180);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.ShadowDecoration.Parent = this.btnExcelExport;
            this.btnExcelExport.Size = new System.Drawing.Size(80, 50);
            this.btnExcelExport.TabIndex = 6;
            this.btnExcelExport.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // btnInfoProduct
            // 
            this.btnInfoProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfoProduct.BackColor = System.Drawing.Color.White;
            this.btnInfoProduct.BorderRadius = 5;
            this.btnInfoProduct.CheckedState.Parent = this.btnInfoProduct;
            this.btnInfoProduct.CustomImages.Parent = this.btnInfoProduct;
            this.btnInfoProduct.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnInfoProduct.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInfoProduct.ForeColor = System.Drawing.Color.White;
            this.btnInfoProduct.HoverState.Parent = this.btnInfoProduct;
            this.btnInfoProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnInfoProduct.Image")));
            this.btnInfoProduct.ImageSize = new System.Drawing.Size(40, 40);
            this.btnInfoProduct.Location = new System.Drawing.Point(10, 120);
            this.btnInfoProduct.Name = "btnInfoProduct";
            this.btnInfoProduct.ShadowDecoration.Parent = this.btnInfoProduct;
            this.btnInfoProduct.Size = new System.Drawing.Size(80, 50);
            this.btnInfoProduct.TabIndex = 5;
            this.btnInfoProduct.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // btnEditProduct
            // 
            this.btnEditProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditProduct.BackColor = System.Drawing.Color.White;
            this.btnEditProduct.BorderRadius = 5;
            this.btnEditProduct.CheckedState.Parent = this.btnEditProduct;
            this.btnEditProduct.CustomImages.Parent = this.btnEditProduct;
            this.btnEditProduct.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnEditProduct.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEditProduct.ForeColor = System.Drawing.Color.White;
            this.btnEditProduct.HoverState.Parent = this.btnEditProduct;
            this.btnEditProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnEditProduct.Image")));
            this.btnEditProduct.ImageSize = new System.Drawing.Size(40, 40);
            this.btnEditProduct.Location = new System.Drawing.Point(10, 60);
            this.btnEditProduct.Name = "btnEditProduct";
            this.btnEditProduct.ShadowDecoration.Parent = this.btnEditProduct;
            this.btnEditProduct.Size = new System.Drawing.Size(80, 50);
            this.btnEditProduct.TabIndex = 4;
            this.btnEditProduct.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnAddProd
            // 
            this.btnAddProd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProd.BackColor = System.Drawing.Color.White;
            this.btnAddProd.BorderRadius = 5;
            this.btnAddProd.CheckedState.Parent = this.btnAddProd;
            this.btnAddProd.CustomImages.Parent = this.btnAddProd;
            this.btnAddProd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnAddProd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddProd.ForeColor = System.Drawing.Color.White;
            this.btnAddProd.HoverState.Parent = this.btnAddProd;
            this.btnAddProd.Image = ((System.Drawing.Image)(resources.GetObject("btnAddProd.Image")));
            this.btnAddProd.ImageSize = new System.Drawing.Size(35, 35);
            this.btnAddProd.Location = new System.Drawing.Point(10, 0);
            this.btnAddProd.Name = "btnAddProd";
            this.btnAddProd.ShadowDecoration.Parent = this.btnAddProd;
            this.btnAddProd.Size = new System.Drawing.Size(80, 50);
            this.btnAddProd.TabIndex = 3;
            this.btnAddProd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.AutoGenerateColumns = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.ColumnHeadersHeight = 40;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prodidDataGridViewTextBoxColumn,
            this.prodnameDataGridViewTextBoxColumn,
            this.prodpriceDataGridViewTextBoxColumn,
            this.prodcountDataGridViewTextBoxColumn,
            this.prodvalueDataGridViewTextBoxColumn,
            this.prodcategoryDataGridViewTextBoxColumn});
            this.dgvMain.DataSource = this.productBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.EnableHeadersVisualStyles = false;
            this.dgvMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvMain.Location = new System.Drawing.Point(0, 0);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMain.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMain.RowTemplate.Height = 40;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(925, 664);
            this.dgvMain.TabIndex = 37;
            this.dgvMain.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGrid;
            this.dgvMain.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMain.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvMain.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvMain.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvMain.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvMain.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvMain.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvMain.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            this.dgvMain.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMain.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMain.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvMain.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvMain.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvMain.ThemeStyle.ReadOnly = true;
            this.dgvMain.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMain.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMain.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMain.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvMain.ThemeStyle.RowsStyle.Height = 40;
            this.dgvMain.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvMain.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // productBindingSource
            // 
            this.productBindingSource.DataSource = typeof(ManagerApplication.Model.Product);
            // 
            // txtFind
            // 
            this.txtFind.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFind.DefaultText = "";
            this.txtFind.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFind.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFind.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFind.DisabledState.Parent = this.txtFind;
            this.txtFind.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFind.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFind.FocusedState.Parent = this.txtFind;
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind.ForeColor = System.Drawing.Color.Black;
            this.txtFind.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFind.HoverState.Parent = this.txtFind;
            this.txtFind.Location = new System.Drawing.Point(6, 10);
            this.txtFind.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.PasswordChar = '\0';
            this.txtFind.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtFind.PlaceholderText = "Поиск...";
            this.txtFind.SelectedText = "";
            this.txtFind.ShadowDecoration.Parent = this.txtFind;
            this.txtFind.Size = new System.Drawing.Size(190, 32);
            this.txtFind.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.txtFind.TabIndex = 0;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.BackColor = System.Drawing.Color.White;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FocusedColor = System.Drawing.Color.Empty;
            this.cmbCategory.FocusedState.Parent = this.cmbCategory;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.HoverState.Parent = this.cmbCategory;
            this.cmbCategory.ItemHeight = 30;
            this.cmbCategory.ItemsAppearance.Parent = this.cmbCategory;
            this.cmbCategory.Location = new System.Drawing.Point(261, 6);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.ShadowDecoration.Parent = this.cmbCategory;
            this.cmbCategory.Size = new System.Drawing.Size(220, 36);
            this.cmbCategory.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // cmbKitchen
            // 
            this.cmbKitchen.BackColor = System.Drawing.Color.White;
            this.cmbKitchen.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbKitchen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKitchen.FocusedColor = System.Drawing.Color.Empty;
            this.cmbKitchen.FocusedState.Parent = this.cmbKitchen;
            this.cmbKitchen.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKitchen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbKitchen.FormattingEnabled = true;
            this.cmbKitchen.HoverState.Parent = this.cmbKitchen;
            this.cmbKitchen.ItemHeight = 30;
            this.cmbKitchen.ItemsAppearance.Parent = this.cmbKitchen;
            this.cmbKitchen.Location = new System.Drawing.Point(546, 6);
            this.cmbKitchen.Name = "cmbKitchen";
            this.cmbKitchen.ShadowDecoration.Parent = this.cmbKitchen;
            this.cmbKitchen.Size = new System.Drawing.Size(220, 36);
            this.cmbKitchen.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.cmbKitchen.TabIndex = 2;
            this.cmbKitchen.SelectedIndexChanged += new System.EventHandler(this.cmbKitchen_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFind);
            this.panel1.Controls.Add(this.cmbKitchen);
            this.panel1.Controls.Add(this.cmbCategory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(6, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1025, 50);
            this.panel1.TabIndex = 45;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDeleteProd);
            this.panel2.Controls.Add(this.btnAddProd);
            this.panel2.Controls.Add(this.btnEditProduct);
            this.panel2.Controls.Add(this.btnExcelExport);
            this.panel2.Controls.Add(this.btnInfoProduct);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(931, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 664);
            this.panel2.TabIndex = 46;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(6, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(925, 664);
            this.panel3.TabIndex = 47;
            // 
            // prodidDataGridViewTextBoxColumn
            // 
            this.prodidDataGridViewTextBoxColumn.DataPropertyName = "prod_name";
            this.prodidDataGridViewTextBoxColumn.HeaderText = "Наименование";
            this.prodidDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prodidDataGridViewTextBoxColumn.Name = "prodidDataGridViewTextBoxColumn";
            this.prodidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prodnameDataGridViewTextBoxColumn
            // 
            this.prodnameDataGridViewTextBoxColumn.DataPropertyName = "prod_start_price";
            this.prodnameDataGridViewTextBoxColumn.HeaderText = "Себестоимость";
            this.prodnameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prodnameDataGridViewTextBoxColumn.Name = "prodnameDataGridViewTextBoxColumn";
            this.prodnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prodpriceDataGridViewTextBoxColumn
            // 
            this.prodpriceDataGridViewTextBoxColumn.DataPropertyName = "prod_price";
            this.prodpriceDataGridViewTextBoxColumn.HeaderText = "Цена";
            this.prodpriceDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prodpriceDataGridViewTextBoxColumn.Name = "prodpriceDataGridViewTextBoxColumn";
            this.prodpriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prodcountDataGridViewTextBoxColumn
            // 
            this.prodcountDataGridViewTextBoxColumn.DataPropertyName = "ProdCategoryName";
            this.prodcountDataGridViewTextBoxColumn.HeaderText = "Категория";
            this.prodcountDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prodcountDataGridViewTextBoxColumn.Name = "prodcountDataGridViewTextBoxColumn";
            this.prodcountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prodvalueDataGridViewTextBoxColumn
            // 
            this.prodvalueDataGridViewTextBoxColumn.DataPropertyName = "ProdTypeName";
            this.prodvalueDataGridViewTextBoxColumn.HeaderText = "Тип";
            this.prodvalueDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prodvalueDataGridViewTextBoxColumn.Name = "prodvalueDataGridViewTextBoxColumn";
            this.prodvalueDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prodcategoryDataGridViewTextBoxColumn
            // 
            this.prodcategoryDataGridViewTextBoxColumn.DataPropertyName = "ProdKitchenName";
            this.prodcategoryDataGridViewTextBoxColumn.HeaderText = "Кухня";
            this.prodcategoryDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prodcategoryDataGridViewTextBoxColumn.Name = "prodcategoryDataGridViewTextBoxColumn";
            this.prodcategoryDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // UC_Product
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "UC_Product";
            this.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.Size = new System.Drawing.Size(1034, 720);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnDeleteProd;
        private Guna.UI2.WinForms.Guna2Button btnExcelExport;
        private Guna.UI2.WinForms.Guna2Button btnInfoProduct;
        private Guna.UI2.WinForms.Guna2Button btnEditProduct;
        private Guna.UI2.WinForms.Guna2Button btnAddProd;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMain;
        private Guna.UI2.WinForms.Guna2TextBox txtFind;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        private Guna.UI2.WinForms.Guna2ComboBox cmbKitchen;
        private System.Windows.Forms.BindingSource productBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodpriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodcountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodvalueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodcategoryDataGridViewTextBoxColumn;
    }
}
