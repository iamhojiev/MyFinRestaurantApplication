using System.Drawing;
using System.Windows.Forms;

namespace ManagerApplication.Forms
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.txtNewVersionInfo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDownload = new System.Windows.Forms.Label();
            this.txtAsk = new System.Windows.Forms.Label();
            this.pbDownload = new Guna.UI2.WinForms.Guna2ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(385, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 24);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnClose.TabIndex = 4;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // txtNewVersionInfo
            // 
            this.txtNewVersionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewVersionInfo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtNewVersionInfo.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtNewVersionInfo.Location = new System.Drawing.Point(0, 201);
            this.txtNewVersionInfo.Name = "txtNewVersionInfo";
            this.txtNewVersionInfo.Size = new System.Drawing.Size(426, 45);
            this.txtNewVersionInfo.TabIndex = 3;
            this.txtNewVersionInfo.Text = "Обновление загружается!";
            this.txtNewVersionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(114, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lblDownload
            // 
            this.lblDownload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDownload.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblDownload.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDownload.Location = new System.Drawing.Point(0, 381);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(427, 61);
            this.lblDownload.TabIndex = 8;
            this.lblDownload.Text = "0MB / 0MB";
            this.lblDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAsk
            // 
            this.txtAsk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAsk.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAsk.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtAsk.Location = new System.Drawing.Point(0, 245);
            this.txtAsk.Name = "txtAsk";
            this.txtAsk.Size = new System.Drawing.Size(426, 81);
            this.txtAsk.TabIndex = 3;
            this.txtAsk.Text = "Пожалуйста, дождитесь окончания загрузки перед продолжением работы.";
            this.txtAsk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbDownload
            // 
            this.pbDownload.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.pbDownload.Location = new System.Drawing.Point(23, 342);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.ShadowDecoration.Parent = this.pbDownload;
            this.pbDownload.Size = new System.Drawing.Size(380, 36);
            this.pbDownload.TabIndex = 9;
            this.pbDownload.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(427, 442);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.lblDownload);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtAsk);
            this.Controls.Add(this.txtNewVersionInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyFin Setup";
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox btnClose;
        private Label txtNewVersionInfo;
        private PictureBox pictureBox1;
        private Label lblDownload;
        private Label txtAsk;
        private Guna.UI2.WinForms.Guna2ProgressBar pbDownload;
    }
}