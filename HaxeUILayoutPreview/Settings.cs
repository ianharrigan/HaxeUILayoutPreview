using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace HaxeUILayoutPreview {
    [Serializable]
    public class Settings {
        private Boolean showConsole = false;
        private Boolean hideMiniMap = true;
        private Keys consoleShortcut = Keys.Control | Keys.F7;

        [Description("Show the console by default or not"), DefaultValue(false)]
        public Boolean ShowConsole {
            get { return this.showConsole; }
            set { this.showConsole = value; }
        }

        [Description("Hide the mini map by default or not"), DefaultValue(true)]
        public Boolean HideMiniMap {
            get { return this.hideMiniMap; }
            set { this.hideMiniMap = value; }
        }

        [Description("Shortcut to open console."), DefaultValue(Keys.Control | Keys.F7)]
        public Keys ConsoleShortcut {
            get { return this.consoleShortcut; }
            set { this.consoleShortcut = value; }
        }
    }
}
