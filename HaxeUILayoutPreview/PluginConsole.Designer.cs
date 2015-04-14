using System.Windows.Forms;

namespace HaxeUILayoutPreview {
    partial class PluginConsole : UserControl {
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
            this.consoleData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // consoleData
            // 
            this.consoleData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleData.Location = new System.Drawing.Point(0, 0);
            this.consoleData.Multiline = true;
            this.consoleData.Name = "consoleData";
            this.consoleData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.consoleData.Size = new System.Drawing.Size(150, 150);
            this.consoleData.TabIndex = 0;
            this.consoleData.WordWrap = false;
            // 
            // PluginConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.consoleData);
            this.Name = "PluginConsole";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox consoleData;
    }
}
