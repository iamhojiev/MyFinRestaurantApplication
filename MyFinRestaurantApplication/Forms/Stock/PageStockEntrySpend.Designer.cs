namespace ManagerApplication
{
    partial class PageStockEntrySpend
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvStockEntry = new Guna.UI2.WinForms.Guna2DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtEntrySum = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSpendSum = new System.Windows.Forms.Label();
            this.dgvStockSpends = new Guna.UI2.WinForms.Guna2DataGridView();
            this.stock_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stocktotalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordernumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockSpendMetricsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entryDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stockNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detailscountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detailspriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockEntry)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockSpends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockSpendMetricsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStockEntry
            // 
            this.dgvStockEntry.AllowUserToAddRows = false;
            this.dgvStockEntry.AllowUserToDeleteRows = false;
            this.dgvStockEntry.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvStockEntry.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStockEntry.AutoGenerateColumns = false;
            this.dgvStockEntry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockEntry.BackgroundColor = System.Drawing.Color.White;
            this.dgvStockEntry.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStockEntry.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStockEntry.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockEntry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStockEntry.ColumnHeadersHeight = 40;
            this.dgvStockEntry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stockNameDataGridViewTextBoxColumn,
            this.detailscountDataGridViewTextBoxColumn,
            this.detailspriceDataGridViewTextBoxColumn,
            this.typeNameDataGridViewTextBoxColumn,
            this.entryNameDataGridViewTextBoxColumn});
            this.dgvStockEntry.DataSource = this.entryDetailsBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStockEntry.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStockEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockEntry.EnableHeadersVisualStyles = false;
            this.dgvStockEntry.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockEntry.Location = new System.Drawing.Point(3, 35);
            this.dgvStockEntry.Name = "dgvStockEntry";
            this.dgvStockEntry.ReadOnly = true;
            this.dgvStockEntry.RowHeadersVisible = false;
            this.dgvStockEntry.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvStockEntry.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStockEntry.RowTemplate.Height = 40;
            this.dgvStockEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockEntry.Size = new System.Drawing.Size(544, 610);
            this.dgvStockEntry.TabIndex = 35;
            this.dgvStockEntry.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGrid;
            this.dgvStockEntry.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockEntry.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvStockEntry.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvStockEntry.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvStockEntry.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvStockEntry.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockEntry.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockEntry.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            this.dgvStockEntry.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvStockEntry.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvStockEntry.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStockEntry.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvStockEntry.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvStockEntry.ThemeStyle.ReadOnly = true;
            this.dgvStockEntry.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockEntry.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStockEntry.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvStockEntry.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStockEntry.ThemeStyle.RowsStyle.Height = 40;
            this.dgvStockEntry.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockEntry.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.dgvStockEntry);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(550, 648);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Приход";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtEntrySum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 606);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 39);
            this.panel1.TabIndex = 36;
            // 
            // txtEntrySum
            // 
            this.txtEntrySum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEntrySum.AutoSize = true;
            this.txtEntrySum.Location = new System.Drawing.Point(3, 7);
            this.txtEntrySum.Name = "txtEntrySum";
            this.txtEntrySum.Size = new System.Drawing.Size(78, 32);
            this.txtEntrySum.TabIndex = 0;
            this.txtEntrySum.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.dgvStockSpends);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(553, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 648);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Расход";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSpendSum);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 606);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(544, 39);
            this.panel2.TabIndex = 37;
            // 
            // txtSpendSum
            // 
            this.txtSpendSum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSpendSum.AutoSize = true;
            this.txtSpendSum.Location = new System.Drawing.Point(3, 7);
            this.txtSpendSum.Name = "txtSpendSum";
            this.txtSpendSum.Size = new System.Drawing.Size(78, 32);
            this.txtSpendSum.TabIndex = 0;
            this.txtSpendSum.Text = "label1";
            // 
            // dgvStockSpends
            // 
            this.dgvStockSpends.AllowUserToAddRows = false;
            this.dgvStockSpends.AllowUserToDeleteRows = false;
            this.dgvStockSpends.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvStockSpends.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStockSpends.AutoGenerateColumns = false;
            this.dgvStockSpends.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockSpends.BackgroundColor = System.Drawing.Color.White;
            this.dgvStockSpends.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStockSpends.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStockSpends.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockSpends.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvStockSpends.ColumnHeadersHeight = 40;
            this.dgvStockSpends.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prodnameDataGridViewTextBoxColumn,
            this.stocktotalDataGridViewTextBoxColumn,
            this.stock_price,
            this.ordernumDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn});
            this.dgvStockSpends.DataSource = this.stockSpendMetricsBindingSource;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStockSpends.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvStockSpends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockSpends.EnableHeadersVisualStyles = false;
            this.dgvStockSpends.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockSpends.Location = new System.Drawing.Point(3, 35);
            this.dgvStockSpends.Name = "dgvStockSpends";
            this.dgvStockSpends.ReadOnly = true;
            this.dgvStockSpends.RowHeadersVisible = false;
            this.dgvStockSpends.RowHeadersWidth = 62;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvStockSpends.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvStockSpends.RowTemplate.Height = 40;
            this.dgvStockSpends.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockSpends.Size = new System.Drawing.Size(544, 610);
            this.dgvStockSpends.TabIndex = 35;
            this.dgvStockSpends.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGrid;
            this.dgvStockSpends.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockSpends.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvStockSpends.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvStockSpends.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvStockSpends.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvStockSpends.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockSpends.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockSpends.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(234)))), ((int)(((byte)(237)))));
            this.dgvStockSpends.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvStockSpends.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvStockSpends.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStockSpends.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvStockSpends.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvStockSpends.ThemeStyle.ReadOnly = true;
            this.dgvStockSpends.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockSpends.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStockSpends.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvStockSpends.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStockSpends.ThemeStyle.RowsStyle.Height = 40;
            this.dgvStockSpends.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockSpends.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // stock_price
            // 
            this.stock_price.DataPropertyName = "stock_price";
            this.stock_price.FillWeight = 86.92893F;
            this.stock_price.HeaderText = "Сум.расход";
            this.stock_price.MinimumWidth = 6;
            this.stock_price.Name = "stock_price";
            this.stock_price.ReadOnly = true;
            // 
            // prodnameDataGridViewTextBoxColumn
            // 
            this.prodnameDataGridViewTextBoxColumn.DataPropertyName = "prod_name";
            this.prodnameDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.prodnameDataGridViewTextBoxColumn.HeaderText = "Блюдо";
            this.prodnameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.prodnameDataGridViewTextBoxColumn.Name = "prodnameDataGridViewTextBoxColumn";
            this.prodnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stocktotalDataGridViewTextBoxColumn
            // 
            this.stocktotalDataGridViewTextBoxColumn.DataPropertyName = "stock_total";
            this.stocktotalDataGridViewTextBoxColumn.FillWeight = 60F;
            this.stocktotalDataGridViewTextBoxColumn.HeaderText = "Кол.";
            this.stocktotalDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.stocktotalDataGridViewTextBoxColumn.Name = "stocktotalDataGridViewTextBoxColumn";
            this.stocktotalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ordernumDataGridViewTextBoxColumn
            // 
            this.ordernumDataGridViewTextBoxColumn.DataPropertyName = "order_num";
            this.ordernumDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.ordernumDataGridViewTextBoxColumn.HeaderText = "Заказ";
            this.ordernumDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.ordernumDataGridViewTextBoxColumn.Name = "ordernumDataGridViewTextBoxColumn";
            this.ordernumDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "date";
            this.dateDataGridViewTextBoxColumn.FillWeight = 86.92893F;
            this.dateDataGridViewTextBoxColumn.HeaderText = "Дата";
            this.dateDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stockSpendMetricsBindingSource
            // 
            this.stockSpendMetricsBindingSource.DataSource = typeof(ManagerApplication.Model.StockSpendMetrics);
            // 
            // entryDetailsBindingSource
            // 
            this.entryDetailsBindingSource.DataSource = typeof(ManagerApplication.Model.EntryDetails);
            // 
            // stockNameDataGridViewTextBoxColumn
            // 
            this.stockNameDataGridViewTextBoxColumn.DataPropertyName = "StockName";
            this.stockNameDataGridViewTextBoxColumn.FillWeight = 200F;
            this.stockNameDataGridViewTextBoxColumn.HeaderText = "Товар";
            this.stockNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.stockNameDataGridViewTextBoxColumn.Name = "stockNameDataGridViewTextBoxColumn";
            this.stockNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // detailscountDataGridViewTextBoxColumn
            // 
            this.detailscountDataGridViewTextBoxColumn.DataPropertyName = "details_count";
            this.detailscountDataGridViewTextBoxColumn.HeaderText = "Кол.";
            this.detailscountDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.detailscountDataGridViewTextBoxColumn.Name = "detailscountDataGridViewTextBoxColumn";
            this.detailscountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // detailspriceDataGridViewTextBoxColumn
            // 
            this.detailspriceDataGridViewTextBoxColumn.DataPropertyName = "details_price";
            this.detailspriceDataGridViewTextBoxColumn.HeaderText = "Цена";
            this.detailspriceDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.detailspriceDataGridViewTextBoxColumn.Name = "detailspriceDataGridViewTextBoxColumn";
            this.detailspriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeNameDataGridViewTextBoxColumn
            // 
            this.typeNameDataGridViewTextBoxColumn.DataPropertyName = "TypeName";
            this.typeNameDataGridViewTextBoxColumn.HeaderText = "Тип";
            this.typeNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.typeNameDataGridViewTextBoxColumn.Name = "typeNameDataGridViewTextBoxColumn";
            this.typeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // entryNameDataGridViewTextBoxColumn
            // 
            this.entryNameDataGridViewTextBoxColumn.DataPropertyName = "EntryName";
            this.entryNameDataGridViewTextBoxColumn.FillWeight = 200F;
            this.entryNameDataGridViewTextBoxColumn.HeaderText = "Приход";
            this.entryNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.entryNameDataGridViewTextBoxColumn.Name = "entryNameDataGridViewTextBoxColumn";
            this.entryNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // PageStockEntrySpend
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1103, 648);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PageStockEntrySpend";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Информация по товару";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockEntry)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockSpends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockSpendMetricsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2DataGridView dgvStockEntry;
        private System.Windows.Forms.BindingSource entryDetailsBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Guna.UI2.WinForms.Guna2DataGridView dgvStockSpends;
        private System.Windows.Forms.BindingSource stockSpendMetricsBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label txtEntrySum;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtSpendSum;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stocktotalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordernumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detailscountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detailspriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryNameDataGridViewTextBoxColumn;
    }
}