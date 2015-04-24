using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaxeUILayoutPreview {
    public partial class PreviewOptions : UserControl {
        private AxShockwaveFlashObjects.AxShockwaveFlash player;
        public PreviewOptions() {
            InitializeComponent();
        }

        public AxShockwaveFlashObjects.AxShockwaveFlash Player {
            get { return this.player; }
            set { this.player = value; }
        }

        private void theme_SelectedIndexChanged(object sender, EventArgs e) {
            string themeName = theme.Text;
            player.CallFunction("<invoke name=\"setTheme\" returntype=\"xml\"><arguments><string>" + themeName + "</string></arguments></invoke>");
        }

        private void PreviewOptions_Paint(object sender, PaintEventArgs e) {
            Pen pen = new Pen(Color.FromArgb(171, 171, 171));
            e.Graphics.DrawLine(pen, 0, 0, 0, this.ClientSize.Height);
        }
    }
}
