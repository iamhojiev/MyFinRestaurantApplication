using ManagerApplication.Forms;
using System;
using System.Windows.Forms;

namespace MyFinRestaurantApplication.Forms
{
    partial class newForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newForm));
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.txtLicenseInfo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnActiveLicense = new System.Windows.Forms.Button();
            this.txtExpiryDate = new System.Windows.Forms.Label();
            this.txtLicenseKey = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(289, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(22, 20);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnClose.TabIndex = 4;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtLicenseInfo
            // 
            this.txtLicenseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicenseInfo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLicenseInfo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtLicenseInfo.Location = new System.Drawing.Point(0, 158);
            this.txtLicenseInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtLicenseInfo.Name = "txtLicenseInfo";
            this.txtLicenseInfo.Size = new System.Drawing.Size(320, 37);
            this.txtLicenseInfo.TabIndex = 3;
            this.txtLicenseInfo.Text = "Идёт проверка лицензии...";
            this.txtLicenseInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(86, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 162);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnActiveLicense
            // 
            this.btnActiveLicense.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnActiveLicense.Location = new System.Drawing.Point(56, 304);
            this.btnActiveLicense.Margin = new System.Windows.Forms.Padding(2);
            this.btnActiveLicense.Name = "btnActiveLicense";
            this.btnActiveLicense.Size = new System.Drawing.Size(208, 44);
            this.btnActiveLicense.TabIndex = 6;
            this.btnActiveLicense.Text = "Активровать";
            this.btnActiveLicense.UseVisualStyleBackColor = true;
            this.btnActiveLicense.Click += new System.EventHandler(this.BtnActiveLicense_Click);
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpiryDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtExpiryDate.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtExpiryDate.Location = new System.Drawing.Point(0, 187);
            this.txtExpiryDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.Size = new System.Drawing.Size(320, 26);
            this.txtExpiryDate.TabIndex = 3;
            this.txtExpiryDate.Text = "Срок действии лицензии: {ExpiryDate}";
            this.txtExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtExpiryDate.Visible = false;
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLicenseKey.DefaultText = "";
            this.txtLicenseKey.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLicenseKey.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLicenseKey.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLicenseKey.DisabledState.Parent = this.txtLicenseKey;
            this.txtLicenseKey.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLicenseKey.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLicenseKey.FocusedState.Parent = this.txtLicenseKey;
            this.txtLicenseKey.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtLicenseKey.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLicenseKey.HoverState.Parent = this.txtLicenseKey;
            this.txtLicenseKey.Location = new System.Drawing.Point(9, 249);
            this.txtLicenseKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.PasswordChar = '\0';
            this.txtLicenseKey.PlaceholderText = "";
            this.txtLicenseKey.SelectedText = "";
            this.txtLicenseKey.ShadowDecoration.Parent = this.txtLicenseKey;
            this.txtLicenseKey.Size = new System.Drawing.Size(302, 34);
            this.txtLicenseKey.TabIndex = 9;
            this.txtLicenseKey.TextChanged += new System.EventHandler(this.txtLicenseKey_TextChanged);
            this.txtLicenseKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLicenseKey_KeyDown);
            // 
            // newForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(320, 359);
            this.Controls.Add(this.txtLicenseKey);
            this.Controls.Add(this.btnActiveLicense);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtExpiryDate);
            this.Controls.Add(this.txtLicenseInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "newForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyFin Setup";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.LicenseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox btnClose;
        private Label txtLicenseInfo;
        private PictureBox pictureBox1;
        private Button btnActiveLicense;
        private Label txtExpiryDate;
        private Guna.UI2.WinForms.Guna2TextBox txtLicenseKey;
    }
}