using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HaxeUILayoutPreview {
    [Serializable]
    public class Settings {
        private Boolean showConsole = false;
        private Boolean hideMiniMap = true;
        private Boolean redirectTrace = true;
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

        [Description("Redirect Haxe trace statements to FlashDevelop"), DefaultValue(true)]
        public Boolean RedirectTrace {
            get { return this.redirectTrace; }
            set { this.redirectTrace = value; }
        }

        [Description("Shortcut to open console."), DefaultValue(Keys.Control | Keys.F7)]
        public Keys ConsoleShortcut {
            get { return this.consoleShortcut; }
            set { this.consoleShortcut = value; }
        }
    }
}
