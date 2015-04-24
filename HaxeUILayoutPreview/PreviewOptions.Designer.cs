namespace HaxeUILayoutPreview {
    partial class PreviewOptions {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.theme = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // theme
            // 
            this.theme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.theme.FormattingEnabled = true;
            this.theme.Items.AddRange(new object[] {
            "Default",
            "Gradient",
            "Gradient Mobile"});
            this.theme.Location = new System.Drawing.Point(52, 6);
            this.theme.Name = "theme";
            this.theme.Size = new System.Drawing.Size(122, 21);
            this.theme.TabIndex = 1;
            this.theme.SelectedIndexChanged += new System.EventHandler(this.theme_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Theme:";
            // 
            // PreviewOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.theme);
            this.Name = "PreviewOptions";
            this.Size = new System.Drawing.Size(179, 320);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewOptions_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox theme;
        private System.Windows.Forms.Label label1;
    }
}
