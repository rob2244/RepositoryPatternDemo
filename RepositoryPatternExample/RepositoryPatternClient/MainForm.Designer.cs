namespace RepositoryPatternClient
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
            this.chkBoxRepository = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // chkBoxRepository
            // 
            this.chkBoxRepository.CheckOnClick = true;
            this.chkBoxRepository.FormattingEnabled = true;
            this.chkBoxRepository.Items.AddRange(new object[] {
            "Use Test1DbRepository",
            "Use Test2DbRepository"});
            this.chkBoxRepository.Location = new System.Drawing.Point(35, 98);
            this.chkBoxRepository.Name = "chkBoxRepository";
            this.chkBoxRepository.Size = new System.Drawing.Size(164, 34);
            this.chkBoxRepository.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 603);
            this.Controls.Add(this.chkBoxRepository);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkBoxRepository;
    }
}

