namespace ManagerApplication.UserControls
{
    partial class MyButton
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
            this.Button = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // Button
            // 
            this.Button.BorderRadius = 15;
            this.Button.BorderThickness = 1;
            this.Button.CheckedState.Parent = this.Button;
            this.Button.CustomImages.Parent = this.Button;
            this.Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(132)))), ((int)(((byte)(107)))));
            this.Button.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button.ForeColor = System.Drawing.Color.White;
            this.Button.HoverState.Parent = this.Button;
            this.Button.Location = new System.Drawing.Point(0, 0);
            this.Button.Name = "Button";
            this.Button.ShadowDecoration.Parent = this.Button;
            this.Button.Size = new System.Drawing.Size(250, 150);
            this.Button.TabIndex = 0;
            this.Button.Text = "123\\nК-во мест: 123\\n123";
            // 
            // MyButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Button);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "MyButton";
            this.Size = new System.Drawing.Size(250, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button Button;
    }
}
