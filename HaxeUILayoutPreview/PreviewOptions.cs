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
        public PreviewOptions(AxShockwaveFlashObjects.AxShockwaveFlash player) {
            this.player = player;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            player.CallFunction("<invoke name=\"setTheme\" returntype=\"xml\"><arguments><string>gradient</string></arguments></invoke>");
        }

        private void theme_SelectedIndexChanged(object sender, EventArgs e) {
            string themeName = theme.Text;
            player.CallFunction("<invoke name=\"setTheme\" returntype=\"xml\"><arguments><string>" + themeName + "</string></arguments></invoke>");
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            player.Dock = DockStyle.None;
            player.Width = 100;
        }
    }
}
