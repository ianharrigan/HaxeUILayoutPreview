using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaxeUILayoutPreview {
    public partial class PreviewContainer : UserControl {
        private AxShockwaveFlashObjects.AxShockwaveFlash player;
        public PreviewContainer(AxShockwaveFlashObjects.AxShockwaveFlash player) {
            this.player = player;
            InitializeComponent();
            previewOptions.Player = player;
            this.panel.Controls.Add(player);
        }
    }
}
