using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using PluginCore.Localization;
using PluginCore.Utilities;
using PluginCore.Managers;
using PluginCore.Helpers;
using PluginCore;
using System.Xml;

namespace HaxeUILayoutPreview
{
    public class PluginMain : IPlugin
    {
        private String pluginName = "HaxeUILayoutPreview";
        private String pluginGuid = "c626e1e6-d7a2-48a1-84c9-3c99d8cbbe01";
        private String pluginHelp = "ianharrigan@hotmail.com";
        private String pluginDesc = "HaxeUI Layout Previewer";
        private String pluginAuth = "Ian Harrigan";
        private String settingFilename;
        public Settings settingObject;
        private Image pluginImage;

        private PluginConsole pluginConsole;
        private DockContent consolePanel;

        private Dictionary<ITabbedDocument, TabDetails> tabDetails = new Dictionary<ITabbedDocument, TabDetails>();

        #region Required Properties

        public Int32 Api {
            get { return 1; }
        }

        public String Name {
            get { return this.pluginName; }
        }

        public String Guid {
            get { return this.pluginGuid; }
        }

        public String Author {
            get { return this.pluginAuth; }
        }

        public String Description {
            get { return this.pluginDesc; }
        }

        public String Help {
            get { return this.pluginHelp; }
        }

        [Browsable(false)]
        public Object Settings {
            get { return this.settingObject; }
        }

        #endregion

        #region Required Methods

        public void Initialize() {
            this.InitBasics();
            this.LoadSettings();
            this.AddEventHandlers();
            this.InitLocalization();
            this.CreateMenuItem();
            if (this.settingObject.ShowConsole == true) {
                OpenConsole();
            }
        }

        public void Dispose() {
            this.SaveSettings();
        }

        public void HandleEvent(Object sender, NotifyEvent e, HandlingPriority prority) {
            ITabbedDocument tdoc = PluginBase.MainForm.CurrentDocument as ITabbedDocument;

            switch (e.Type) {
                case EventType.FileOpen:
                case EventType.FileSwitch: // use switch also as we are basing detection on content
                    if (IsHaxeUILayout(tdoc) == true) {
                        TabDetails details = GetTabDetails(tdoc);
                    }
                    break;

                case EventType.FileClose:
                    DisposeTabDetails(tdoc);
                    break;

                case EventType.Command:
                    string cmd = (e as DataEvent).Action;
                    //ConsoleLog("detected command " + cmd);
                    break;

                case EventType.UIRefresh: // TODO: not quite sure this is the best way to have split screen editing
                    if (tabDetails.ContainsKey(tdoc)) {
                        TabDetails details = GetTabDetails(tdoc);
                        details.UpdatePreviews();
                    }
                    break;
            }
        }

        #endregion

        #region Custom Methods

        public void InitBasics() {
            String dataPath = Path.Combine(PathHelper.DataDir, "HaxeUILayoutPreview");
            if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
            this.settingFilename = Path.Combine(dataPath, "Settings.fdb");
            this.pluginImage = PluginBase.MainForm.FindImage("100");
        }

        public void AddEventHandlers() {
            EventManager.AddEventHandler(this, EventType.FileOpen
                                               | EventType.FileSwitch
                                               | EventType.FileClose
                                               | EventType.Command
                                               | EventType.UIRefresh
                                               | EventType.Keys);
        }

        private static List<String> HAXEUI_COMPONENTS = new List<String>{
            "Absolute", "Accordion", "Box", "CalendarView", "Container", "ContinuousHBox", "ContinuousVBox",
            "ExpandablePanel", "Grid", "HBox", "HSplitter", "ListView", "MenuBar", "ScrollView", "SpriteContainer",
            "Stack", "TableView", "TabView", "VBox", "VSplitter",

            "Button", "Calendar", "CheckBox", "Divider", "HProgress", "HScroll", "HSlider", "Image", "Link", "Menu",
            "MenuButton", "MenuItem", "MenuSeparator", "OptionBox", "Progress", "Scroll", "Slider", "Spacer", "TabBar",
            "Text", "TextInput", "ToolTip", "Value", "VProgress", "VScroll", "VSlider"
        };

        private bool IsHaxeUILayout(ITabbedDocument tdoc) {
            string content = tdoc.SciControl.Text.ToLower();
            try {
                XmlDocument document = new XmlDocument();
                document.LoadXml(content);
                foreach (string c in HAXEUI_COMPONENTS) { // TODO: probably not the best way to do this
                    if (document.SelectNodes("//" + c.ToLower()).Count > 0) {
                        return true;
                    }
                }
            } catch {
                return false;
            }
            return false;
        }

        private TabDetails GetTabDetails(ITabbedDocument tdoc) {
            TabDetails details = null;
            if (tabDetails.ContainsKey(tdoc) == true) {
                details = tabDetails[tdoc];
            } else {
                details = CreateTabDetails(tdoc);
            }
            return details;
        }

        private TabDetails CreateTabDetails(ITabbedDocument tdoc) {
            TabDetails details = new TabDetails(this);
            tabDetails.Add(tdoc, details);
            return details;
        }

        private void DisposeTabDetails(ITabbedDocument tdoc) {
            if (tabDetails.ContainsKey(tdoc) == true) {
                TabDetails details = tabDetails[tdoc];
                details.Dispose();
                tabDetails.Remove(tdoc);
            }
        }

        public void InitLocalization() {
        }


        public void CreateMenuItem() {
            ToolStripMenuItem viewMenu = (ToolStripMenuItem)PluginBase.MainForm.FindMenuItem("ViewMenu");
            viewMenu.DropDownItems.Add(new ToolStripMenuItem("HaxeUI Layout Preview Console", this.pluginImage, new EventHandler(this.ToggleConsole), this.settingObject.ConsoleShortcut));
            PluginBase.MainForm.IgnoredKeys.Add(this.settingObject.ConsoleShortcut);
        }

        public void LoadSettings() {
            this.settingObject = new Settings();
            if (!File.Exists(this.settingFilename)) {
                this.SaveSettings();
            } else {
                Object obj = ObjectSerializer.Deserialize(this.settingFilename, this.settingObject);
                this.settingObject = (Settings)obj;
            }
        }

        public void SaveSettings() {
            ObjectSerializer.Serialize(this.settingFilename, this.settingObject);
        }

        public void ToggleConsole(Object sender, System.EventArgs e) {
            if (this.consolePanel == null || this.consolePanel.Visible == false) {
                OpenConsole();
            } else {
                CloseConsole();
            }
        }

        private void OpenConsole() {
            if (this.pluginConsole == null) {
                this.pluginConsole = new PluginConsole();
                this.consolePanel = PluginBase.MainForm.CreateDockablePanel(this.pluginConsole, this.pluginGuid, this.pluginImage, DockState.DockRight);
            }
            this.consolePanel.Show();
            this.settingObject.ShowConsole = true;
        }

        private void CloseConsole() {
            if (this.pluginConsole != null) {
                this.consolePanel.Hide();
                this.settingObject.ShowConsole = false;
            }
        }

        public void ConsoleLog(string data) {
            if (this.pluginConsole != null) {
                this.pluginConsole.Log(data);
            }
        }

        #endregion

    }
}
