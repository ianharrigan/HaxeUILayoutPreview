using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaxeUILayoutPreview {
    public partial class PluginConsole : UserControl {
        public PluginConsole() {
            InitializeComponent();
        }

        public void Log(string data) {
            consoleData.AppendText(data + "\r\n");
        }
    }
}
