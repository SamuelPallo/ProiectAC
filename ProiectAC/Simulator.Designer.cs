namespace ProiectAC
{
    partial class Simulator
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
            this.PrincipalShape = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.openAsmFile = new System.Windows.Forms.Button();
            this.assembleFile = new System.Windows.Forms.Button();
            this.instrBox = new System.Windows.Forms.ListBox();
            this.assembleBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // PrincipalShape
            // 
            this.PrincipalShape.FillColor = System.Drawing.Color.White;
            this.PrincipalShape.FillGradientColor = System.Drawing.Color.White;
            this.PrincipalShape.Location = new System.Drawing.Point(257, 99);
            this.PrincipalShape.Name = "";
            this.PrincipalShape.SelectionColor = System.Drawing.Color.White;
            this.PrincipalShape.Size = new System.Drawing.Size(835, 479);
            // 
            // openAsmFile
            // 
            this.openAsmFile.Location = new System.Drawing.Point(40, 12);
            this.openAsmFile.Name = "openAsmFile";
            this.openAsmFile.Size = new System.Drawing.Size(75, 23);
            this.openAsmFile.TabIndex = 0;
            this.openAsmFile.Text = "Open asn file";
            this.openAsmFile.UseVisualStyleBackColor = true;
            this.openAsmFile.Click += new System.EventHandler(this.openAsmFile_Click);
            // 
            // assembleFile
            // 
            this.assembleFile.Location = new System.Drawing.Point(178, 12);
            this.assembleFile.Name = "assembleFile";
            this.assembleFile.Size = new System.Drawing.Size(75, 23);
            this.assembleFile.TabIndex = 1;
            this.assembleFile.Text = "Assemble";
            this.assembleFile.UseVisualStyleBackColor = true;
            this.assembleFile.Click += new System.EventHandler(this.assembleFile_Click);
            // 
            // instrBox
            // 
            this.instrBox.FormattingEnabled = true;
            this.instrBox.Location = new System.Drawing.Point(12, 51);
            this.instrBox.Name = "instrBox";
            this.instrBox.Size = new System.Drawing.Size(211, 511);
            this.instrBox.TabIndex = 2;
            // 
            // assembleBox
            // 
            this.assembleBox.FormattingEnabled = true;
            this.assembleBox.Location = new System.Drawing.Point(12, 568);
            this.assembleBox.Name = "assembleBox";
            this.assembleBox.Size = new System.Drawing.Size(211, 17);
            this.assembleBox.TabIndex = 3;
            // 
            // Simulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 588);
            this.Controls.Add(this.assembleBox);
            this.Controls.Add(this.instrBox);
            this.Controls.Add(this.assembleFile);
            this.Controls.Add(this.openAsmFile);
            this.Name = "Simulator";
            this.RightToLeftLayout = true;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.VisualBasic.PowerPacks.RectangleShape PrincipalShape;
        private System.Windows.Forms.Button openAsmFile;
        private System.Windows.Forms.Button assembleFile;
        public System.Windows.Forms.ListBox instrBox;
        public System.Windows.Forms.ListBox assembleBox;
    }
}

