namespace ProiectAC
{
    partial class Memory
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
            this.listBoxMem = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxMem
            // 
            this.listBoxMem.FormattingEnabled = true;
            this.listBoxMem.Location = new System.Drawing.Point(12, 12);
            this.listBoxMem.Name = "listBoxMem";
            this.listBoxMem.Size = new System.Drawing.Size(187, 394);
            this.listBoxMem.TabIndex = 2;
            // 
            // Memory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 426);
            this.Controls.Add(this.listBoxMem);
            this.Name = "Memory";
            this.Text = "Memory";
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListBox listBoxMem;
    }
}