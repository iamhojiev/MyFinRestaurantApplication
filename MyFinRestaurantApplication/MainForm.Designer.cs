namespace ManagerApplication
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.infoPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAccounting = new Guna.UI2.WinForms.Guna2Button();
            this.btnHallAndTable = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenu = new Guna.UI2.WinForms.Guna2Button();
            this.btnDirectory = new Guna.UI2.WinForms.Guna2Button();
            this.btnSettings = new Guna.UI2.WinForms.Guna2Button();
            this.quitBtn = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nameLbl = new System.Windows.Forms.Label();
            this.roleLbl = new System.Windows.Forms.Label();
            this.picAvatar = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.btnFinance = new Guna.UI2.WinForms.Guna2Button();
            this.btnDiagrams = new Guna.UI2.WinForms.Guna2Button();
            this.txtLogo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mainContainer = new System.Windows.Forms.Panel();
            this.picPersonal = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.infoPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonal)).BeginInit();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.infoPanel.Controls.Add(this.txtLogo);
            this.infoPanel.Controls.Add(this.flowLayoutPanel1);
            this.infoPanel.Controls.Add(this.label3);
            this.infoPanel.Controls.Add(this.panel1);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(250, 837);
            this.infoPanel.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAccounting);
            this.flowLayoutPanel1.Controls.Add(this.btnDiagrams);
            this.flowLayoutPanel1.Controls.Add(this.btnFinance);
            this.flowLayoutPanel1.Controls.Add(this.btnHallAndTable);
            this.flowLayoutPanel1.Controls.Add(this.btnMenu);
            this.flowLayoutPanel1.Controls.Add(this.btnDirectory);
            this.flowLayoutPanel1.Controls.Add(this.btnSettings);
            this.flowLayoutPanel1.Controls.Add(this.quitBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 200);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(250, 575);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnAccounting
            // 
            this.btnAccounting.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnAccounting.Checked = true;
            this.btnAccounting.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnAccounting.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAccounting.CheckedState.Parent = this.btnAccounting;
            this.btnAccounting.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnAccounting.CustomImages.Parent = this.btnAccounting;
            this.btnAccounting.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnAccounting.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccounting.ForeColor = System.Drawing.Color.Black;
            this.btnAccounting.HoverState.Parent = this.btnAccounting;
            this.btnAccounting.Image = ((System.Drawing.Image)(resources.GetObject("btnAccounting.Image")));
            this.btnAccounting.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAccounting.ImageOffset = new System.Drawing.Point(15, 0);
            this.btnAccounting.ImageSize = new System.Drawing.Size(50, 50);
            this.btnAccounting.Location = new System.Drawing.Point(3, 3);
            this.btnAccounting.Name = "btnAccounting";
            this.btnAccounting.PressedColor = System.Drawing.Color.RoyalBlue;
            this.btnAccounting.ShadowDecoration.Parent = this.btnAccounting;
            this.btnAccounting.Size = new System.Drawing.Size(250, 67);
            this.btnAccounting.TabIndex = 0;
            this.btnAccounting.Text = "Отчёты";
            this.btnAccounting.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAccounting.TextOffset = new System.Drawing.Point(20, 0);
            this.btnAccounting.Click += new System.EventHandler(this.btnAccounting_Click);
            // 
            // btnHallAndTable
            // 
            this.btnHallAndTable.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnHallAndTable.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnHallAndTable.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnHallAndTable.CheckedState.Parent = this.btnHallAndTable;
            this.btnHallAndTable.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnHallAndTable.CustomImages.Parent = this.btnHallAndTable;
            this.btnHallAndTable.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnHallAndTable.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHallAndTable.ForeColor = System.Drawing.Color.Black;
            this.btnHallAndTable.HoverState.Parent = this.btnHallAndTable;
            this.btnHallAndTable.Image = ((System.Drawing.Image)(resources.GetObject("btnHallAndTable.Image")));
            this.btnHallAndTable.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHallAndTable.ImageOffset = new System.Drawing.Point(15, 0);
            this.btnHallAndTable.ImageSize = new System.Drawing.Size(50, 50);
            this.btnHallAndTable.Location = new System.Drawing.Point(3, 222);
            this.btnHallAndTable.Name = "btnHallAndTable";
            this.btnHallAndTable.PressedColor = System.Drawing.Color.RoyalBlue;
            this.btnHallAndTable.ShadowDecoration.Parent = this.btnHallAndTable;
            this.btnHallAndTable.Size = new System.Drawing.Size(250, 67);
            this.btnHallAndTable.TabIndex = 1;
            this.btnHallAndTable.Text = "Столики/Залы";
            this.btnHallAndTable.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHallAndTable.TextOffset = new System.Drawing.Point(20, 0);
            this.btnHallAndTable.Click += new System.EventHandler(this.btnHallAndTable_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenu.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnMenu.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMenu.CheckedState.Parent = this.btnMenu;
            this.btnMenu.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnMenu.CustomImages.Parent = this.btnMenu;
            this.btnMenu.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenu.ForeColor = System.Drawing.Color.Black;
            this.btnMenu.HoverState.Parent = this.btnMenu;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMenu.ImageOffset = new System.Drawing.Point(15, 0);
            this.btnMenu.ImageSize = new System.Drawing.Size(50, 50);
            this.btnMenu.Location = new System.Drawing.Point(3, 295);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.PressedColor = System.Drawing.Color.RoyalBlue;
            this.btnMenu.ShadowDecoration.Parent = this.btnMenu;
            this.btnMenu.Size = new System.Drawing.Size(250, 67);
            this.btnMenu.TabIndex = 2;
            this.btnMenu.Text = "Меню";
            this.btnMenu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMenu.TextOffset = new System.Drawing.Point(20, 0);
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // btnDirectory
            // 
            this.btnDirectory.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDirectory.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnDirectory.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDirectory.CheckedState.Parent = this.btnDirectory;
            this.btnDirectory.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnDirectory.CustomImages.Parent = this.btnDirectory;
            this.btnDirectory.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnDirectory.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDirectory.ForeColor = System.Drawing.Color.Black;
            this.btnDirectory.HoverState.Parent = this.btnDirectory;
            this.btnDirectory.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectory.Image")));
            this.btnDirectory.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDirectory.ImageOffset = new System.Drawing.Point(15, 0);
            this.btnDirectory.ImageSize = new System.Drawing.Size(50, 50);
            this.btnDirectory.Location = new System.Drawing.Point(3, 368);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.PressedColor = System.Drawing.Color.RoyalBlue;
            this.btnDirectory.ShadowDecoration.Parent = this.btnDirectory;
            this.btnDirectory.Size = new System.Drawing.Size(250, 67);
            this.btnDirectory.TabIndex = 3;
            this.btnDirectory.Text = "Справочник";
            this.btnDirectory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDirectory.TextOffset = new System.Drawing.Point(20, 0);
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnSettings.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnSettings.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSettings.CheckedState.Parent = this.btnSettings;
            this.btnSettings.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnSettings.CustomImages.Parent = this.btnSettings;
            this.btnSettings.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.Black;
            this.btnSettings.HoverState.Parent = this.btnSettings;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSettings.ImageOffset = new System.Drawing.Point(15, 0);
            this.btnSettings.ImageSize = new System.Drawing.Size(45, 45);
            this.btnSettings.Location = new System.Drawing.Point(3, 441);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.PressedColor = System.Drawing.Color.RoyalBlue;
            this.btnSettings.ShadowDecoration.Parent = this.btnSettings;
            this.btnSettings.Size = new System.Drawing.Size(250, 67);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Настройки";
            this.btnSettings.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSettings.TextOffset = new System.Drawing.Point(20, 0);
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // quitBtn
            // 
            this.quitBtn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.quitBtn.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.quitBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.quitBtn.CheckedState.Parent = this.quitBtn;
            this.quitBtn.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.quitBtn.CustomImages.Parent = this.quitBtn;
            this.quitBtn.FillColor = System.Drawing.Color.WhiteSmoke;
            this.quitBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitBtn.ForeColor = System.Drawing.Color.Black;
            this.quitBtn.HoverState.Parent = this.quitBtn;
            this.quitBtn.Image = ((System.Drawing.Image)(resources.GetObject("quitBtn.Image")));
            this.quitBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.quitBtn.ImageOffset = new System.Drawing.Point(15, 0);
            this.quitBtn.ImageSize = new System.Drawing.Size(45, 45);
            this.quitBtn.Location = new System.Drawing.Point(3, 514);
            this.quitBtn.Name = "quitBtn";
            this.quitBtn.PressedColor = System.Drawing.Color.RoyalBlue;
            this.quitBtn.ShadowDecoration.Parent = this.quitBtn;
            this.quitBtn.Size = new System.Drawing.Size(250, 67);
            this.quitBtn.TabIndex = 5;
            this.quitBtn.Text = "Выйти";
            this.quitBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.quitBtn.TextOffset = new System.Drawing.Point(20, 0);
            this.quitBtn.Click += new System.EventHandler(this.quitBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nameLbl);
            this.panel1.Controls.Add(this.roleLbl);
            this.panel1.Controls.Add(this.picAvatar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 200);
            this.panel1.TabIndex = 0;
            // 
            // nameLbl
            // 
            this.nameLbl.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.nameLbl.Location = new System.Drawing.Point(0, 137);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(250, 30);
            this.nameLbl.TabIndex = 0;
            this.nameLbl.Text = "{Name}";
            this.nameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roleLbl
            // 
            this.roleLbl.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roleLbl.Location = new System.Drawing.Point(0, 166);
            this.roleLbl.Name = "roleLbl";
            this.roleLbl.Size = new System.Drawing.Size(250, 30);
            this.roleLbl.TabIndex = 0;
            this.roleLbl.Text = "{Role}";
            this.roleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picAvatar
            // 
            this.picAvatar.Image = global::ManagerApplication.Properties.Resources.manager;
            this.picAvatar.Location = new System.Drawing.Point(56, 7);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picAvatar.ShadowDecoration.Parent = this.picAvatar;
            this.picAvatar.Size = new System.Drawing.Size(130, 130);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            // 
            // btnFinance
            // 
            this.btnFinance.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnFinance.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnFinance.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFinance.CheckedState.Parent = this.btnFinance;
            this.btnFinance.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnFinance.CustomImages.Parent = this.btnFinance;
            this.btnFinance.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnFinance.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinance.ForeColor = System.Drawing.Color.Black;
            this.btnFinance.HoverState.Parent = this.btnFinance;
            this.btnFinance.Image = ((System.Drawing.Image)(resources.GetObject("btnFinance.Image")));
            this.btnFinance.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnFinance.ImageOffset = new System.Drawing.Point(15, 0);
            this.btnFinance.ImageSize = new System.Drawing.Size(50, 50);
            this.btnFinance.Location = new System.Drawing.Point(3, 149);
            this.btnFinance.Name = "btnFinance";
            this.btnFinance.PressedColor = System.Drawing.Color.RoyalBlue;
            this.btnFinance.ShadowDecoration.Parent = this.btnFinance;
            this.btnFinance.Size = new System.Drawing.Size(250, 67);
            this.btnFinance.TabIndex = 7;
            this.btnFinance.Text = "Финансы";
            this.btnFinance.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnFinance.TextOffset = new System.Drawing.Point(20, 0);
            this.btnFinance.Click += new System.EventHandler(this.btnFinance_Click);
            // 
            // btnDiagrams
            // 
            this.btnDiagrams.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDiagrams.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            this.btnDiagrams.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDiagrams.CheckedState.Parent = this.btnDiagrams;
            this.btnDiagrams.CustomBorderThickness = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnDiagrams.CustomImages.Parent = this.btnDiagrams;
            this.btnDiagrams.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnDiagrams.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiagrams.ForeColor = System.Drawing.Color.Black;
            this.btnDiagrams.HoverState.Parent = this.btnDiagrams;
            this.btnDiagrams.Image = ((System.Drawing.Image)(resources.GetObject("btnDiagrams.Image")));
            this.btnDiagrams.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDiagrams.ImageOffset = new System.Drawing.Point(15, 0);
            this.btnDiagrams.ImageSize = new System.Drawing.Size(54, 54);
            this.btnDiagrams.Location = new System.Drawing.Point(3, 76);
            this.btnDiagrams.Name = "btnDiagrams";
            this.btnDiagrams.PressedColor = System.Drawing.Color.RoyalBlue;
            this.btnDiagrams.ShadowDecoration.Parent = this.btnDiagrams;
            this.btnDiagrams.Size = new System.Drawing.Size(250, 67);
            this.btnDiagrams.TabIndex = 6;
            this.btnDiagrams.Text = "Диаграммы";
            this.btnDiagrams.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDiagrams.TextOffset = new System.Drawing.Point(20, 0);
            this.btnDiagrams.Click += new System.EventHandler(this.btnDiagrams_Click);
            // 
            // txtLogo
            // 
            this.txtLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogo.AutoSize = true;
            this.txtLogo.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogo.Location = new System.Drawing.Point(5, 788);
            this.txtLogo.Name = "txtLogo";
            this.txtLogo.Size = new System.Drawing.Size(241, 23);
            this.txtLogo.TabIndex = 0;
            this.txtLogo.Text = "MYFIN Ресторан © 2023";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(67, 811);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "+992 550055110";
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(250, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(1034, 837);
            this.mainContainer.TabIndex = 1;
            // 
            // picPersonal
            // 
            this.picPersonal.Location = new System.Drawing.Point(56, 7);
            this.picPersonal.Name = "picPersonal";
            this.picPersonal.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picPersonal.ShadowDecoration.Parent = this.picPersonal;
            this.picPersonal.Size = new System.Drawing.Size(130, 130);
            this.picPersonal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPersonal.TabIndex = 0;
            this.picPersonal.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1284, 837);
            this.ControlBox = false;
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.infoPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picAvatar;
        private System.Windows.Forms.Panel mainContainer;
        private System.Windows.Forms.Label roleLbl;
        private Guna.UI2.WinForms.Guna2Button btnAccounting;
        private Guna.UI2.WinForms.Guna2Button btnDirectory;
        private Guna.UI2.WinForms.Guna2Button btnMenu;
        private Guna.UI2.WinForms.Guna2Button btnHallAndTable;
        private Guna.UI2.WinForms.Guna2Button quitBtn;
        private Guna.UI2.WinForms.Guna2Button btnSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtLogo;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picPersonal;
        private System.Windows.Forms.Label nameLbl;
        private Guna.UI2.WinForms.Guna2Button btnDiagrams;
        private Guna.UI2.WinForms.Guna2Button btnFinance;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}