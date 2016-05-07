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
            this.openAsmFile = new System.Windows.Forms.Button();
            this.assembleFile = new System.Windows.Forms.Button();
            this.instrBox = new System.Windows.Forms.ListBox();
            this.assembleBox = new System.Windows.Forms.ListBox();
            this.label30 = new System.Windows.Forms.Label();
            this.microcodeBox = new System.Windows.Forms.ListBox();
            this.openMicrocodeButton = new System.Windows.Forms.Button();
            this.showArhitectureButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.stepButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.showMemoryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openAsmFile
            // 
            this.openAsmFile.Location = new System.Drawing.Point(12, 12);
            this.openAsmFile.Name = "openAsmFile";
            this.openAsmFile.Size = new System.Drawing.Size(75, 23);
            this.openAsmFile.TabIndex = 0;
            this.openAsmFile.Text = "Open asn file";
            this.openAsmFile.UseVisualStyleBackColor = true;
            this.openAsmFile.Click += new System.EventHandler(this.openAsmFile_Click);
            // 
            // assembleFile
            // 
            this.assembleFile.Location = new System.Drawing.Point(103, 12);
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
            this.instrBox.Size = new System.Drawing.Size(248, 511);
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
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(266, 38);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(54, 13);
            this.label30.TabIndex = 69;
            this.label30.Text = "Microcod:";
            // 
            // microcodeBox
            // 
            this.microcodeBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.microcodeBox.FormattingEnabled = true;
            this.microcodeBox.Location = new System.Drawing.Point(267, 51);
            this.microcodeBox.Name = "microcodeBox";
            this.microcodeBox.Size = new System.Drawing.Size(836, 511);
            this.microcodeBox.TabIndex = 68;
            // 
            // openMicrocodeButton
            // 
            this.openMicrocodeButton.Location = new System.Drawing.Point(267, 12);
            this.openMicrocodeButton.Name = "openMicrocodeButton";
            this.openMicrocodeButton.Size = new System.Drawing.Size(122, 23);
            this.openMicrocodeButton.TabIndex = 70;
            this.openMicrocodeButton.Text = "Open microcode";
            this.openMicrocodeButton.UseVisualStyleBackColor = true;
            this.openMicrocodeButton.Click += new System.EventHandler(this.openMicrocodeButton_Click);
            // 
            // showArhitectureButton
            // 
            this.showArhitectureButton.Location = new System.Drawing.Point(492, 12);
            this.showArhitectureButton.Name = "showArhitectureButton";
            this.showArhitectureButton.Size = new System.Drawing.Size(112, 23);
            this.showArhitectureButton.TabIndex = 71;
            this.showArhitectureButton.Text = "Show arhitecture";
            this.showArhitectureButton.UseVisualStyleBackColor = true;
            this.showArhitectureButton.Click += new System.EventHandler(this.showArhitectureButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Green;
            this.label16.Location = new System.Drawing.Point(433, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 173;
            this.label16.Text = "Simulator:";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(619, 12);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(71, 23);
            this.runButton.TabIndex = 174;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(706, 12);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(58, 23);
            this.stepButton.TabIndex = 175;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(780, 12);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 176;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(889, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 177;
            this.label1.Text = "Memory:";
            // 
            // showMemoryButton
            // 
            this.showMemoryButton.Location = new System.Drawing.Point(967, 12);
            this.showMemoryButton.Name = "showMemoryButton";
            this.showMemoryButton.Size = new System.Drawing.Size(98, 23);
            this.showMemoryButton.TabIndex = 178;
            this.showMemoryButton.Text = "Show Memory";
            this.showMemoryButton.UseVisualStyleBackColor = true;
            this.showMemoryButton.Click += new System.EventHandler(this.showMemoryButton_Click);
            // 
            // Simulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 588);
            this.Controls.Add(this.showMemoryButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.showArhitectureButton);
            this.Controls.Add(this.openMicrocodeButton);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.microcodeBox);
            this.Controls.Add(this.assembleBox);
            this.Controls.Add(this.instrBox);
            this.Controls.Add(this.assembleFile);
            this.Controls.Add(this.openAsmFile);
            this.Name = "Simulator";
            this.RightToLeftLayout = true;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openAsmFile;
        private System.Windows.Forms.Button assembleFile;
        public System.Windows.Forms.ListBox instrBox;
        public System.Windows.Forms.ListBox assembleBox;
        public System.Windows.Forms.Label label30;
        public System.Windows.Forms.ListBox microcodeBox;
        private System.Windows.Forms.Button openMicrocodeButton;
        private System.Windows.Forms.Button showArhitectureButton;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button stepButton;
        public System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button showMemoryButton;
    }
}

