namespace HaxeUILayoutPreview {
    partial class PreviewContainer {
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
            this.panel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.previewOptions = new HaxeUILayoutPreview.PreviewOptions();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 32);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(591, 481);
            this.panel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(591, 32);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(591, 535);
            this.panel3.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 513);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 22);
            this.panel1.TabIndex = 3;
            // 
            // previewOptions
            // 
            this.previewOptions.Dock = System.Windows.Forms.DockStyle.Right;
            this.previewOptions.Location = new System.Drawing.Point(591, 0);
            this.previewOptions.Name = "previewOptions";
            this.previewOptions.Player = null;
            this.previewOptions.Size = new System.Drawing.Size(179, 535);
            this.previewOptions.TabIndex = 4;
            // 
            // PreviewContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.previewOptions);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 50);
            this.Name = "PreviewContainer";
            this.Size = new System.Drawing.Size(770, 535);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private PreviewOptions previewOptions;
        private System.Windows.Forms.Panel panel1;

    }
}
