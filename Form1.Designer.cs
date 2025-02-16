namespace MultiInstanceEnabler
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            EnableBtn = new Button();
            DisableBtn = new Button();
            SuspendLayout();
            // 
            // EnableBtn
            // 
            EnableBtn.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            EnableBtn.Location = new Point(158, 345);
            EnableBtn.Name = "EnableBtn";
            EnableBtn.Size = new Size(157, 45);
            EnableBtn.TabIndex = 0;
            EnableBtn.Text = "enable";
            EnableBtn.UseVisualStyleBackColor = true;
            EnableBtn.Click += EnableBtn_Click;
            // 
            // DisableBtn
            // 
            DisableBtn.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DisableBtn.Location = new Point(508, 345);
            DisableBtn.Name = "DisableBtn";
            DisableBtn.Size = new Size(157, 45);
            DisableBtn.TabIndex = 1;
            DisableBtn.Text = "disable";
            DisableBtn.UseVisualStyleBackColor = true;
            DisableBtn.Click += DisableBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(DisableBtn);
            Controls.Add(EnableBtn);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "multi instance enabler";
            FormClosing += MainForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button EnableBtn;
        private Button DisableBtn;
    }
}
