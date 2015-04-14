using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginCore.Localization;
using PluginCore.Utilities;
using PluginCore.Managers;
using PluginCore.Helpers;
using PluginCore;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing;

namespace HaxeUILayoutPreview {
    class TabDetails {
        private PluginMain pluginMain;
        private List<Messir.Windows.Forms.TabStrip> strips = new List<Messir.Windows.Forms.TabStrip>();
        private List<AxShockwaveFlashObjects.AxShockwaveFlash> players = new List<AxShockwaveFlashObjects.AxShockwaveFlash>();
        private OpenFLApplicationDescriptor applicationDescriptor;

        public TabDetails(PluginMain pluginMain) {
            this.pluginMain = pluginMain;
            ITabbedDocument tdoc = PluginBase.MainForm.CurrentDocument as ITabbedDocument;
            strips.Add(AddTabStrip(tdoc.SplitSci1));
            strips.Add(AddTabStrip(tdoc.SplitSci2));
        }

        private Messir.Windows.Forms.TabStrip AddTabStrip(Control editor) {
            Messir.Windows.Forms.TabStrip strip = new Messir.Windows.Forms.TabStrip();
            strip.Dock = DockStyle.Bottom;
            strip.FlipButtons = true;
            strip.RenderStyle = System.Windows.Forms.ToolStripRenderMode.System;
            strip.UseVisualStyles = false;

            Messir.Windows.Forms.TabStripButton sourceButton = new Messir.Windows.Forms.TabStripButton();
            sourceButton.Text = "Source";

            Messir.Windows.Forms.TabStripButton previewButton = new Messir.Windows.Forms.TabStripButton();
            previewButton.Text = "Preview";

            strip.Items.Add(sourceButton);
            strip.Items.Add(previewButton);
            strip.SelectedTab = sourceButton;

            strip.SelectedTabChanged += new EventHandler<Messir.Windows.Forms.SelectedTabChangedEventArgs>(strip_SelectedTabChanged);

            editor.Parent.Controls.Add(strip);

            return strip;
        }

        private void strip_SelectedTabChanged(object sender, Messir.Windows.Forms.SelectedTabChangedEventArgs e) {
            ITabbedDocument tdoc = PluginBase.MainForm.CurrentDocument as ITabbedDocument;
            Messir.Windows.Forms.TabStrip tabStrip = sender as Messir.Windows.Forms.TabStrip;
            int paneIndex = strips.IndexOf(tabStrip);
            ScintillaNet.ScintillaControl editor = (paneIndex == 0) ? tdoc.SplitSci1 : tdoc.SplitSci2;
            if (e.SelectedTab.Text == "Source") {
                editor.Show();
                if (HasPreviewPlayer(paneIndex) == true) {
                    GetPreviewPlayer(paneIndex).Hide();
                }
            } else if (e.SelectedTab.Text == "Preview") {
                editor.Hide();
                if (HasPreviewPlayer(paneIndex) == true) {
                    UpdatePreview(paneIndex);
                    GetPreviewPlayer(paneIndex).Show();
                } else {
                    AxShockwaveFlashObjects.AxShockwaveFlash player = GetPreviewPlayer(paneIndex);
                }
            }
        }

        private bool HasPreviewPlayer(int paneIndex) {
            return (players.Count > paneIndex);
        }

        private AxShockwaveFlashObjects.AxShockwaveFlash GetPreviewPlayer(int paneIndex) {
            AxShockwaveFlashObjects.AxShockwaveFlash player = null;
            if (players.Count > paneIndex) {
                player = players[paneIndex];
            } else {
                ITabbedDocument tdoc = PluginBase.MainForm.CurrentDocument as ITabbedDocument;
                ScintillaNet.ScintillaControl editor = (paneIndex == 0) ? tdoc.SplitSci1 : tdoc.SplitSci2;
                player = CreatePreviewPlayer();
                players.Add(player);
                editor.Parent.Controls.Add(player);
                string filename = Util.ExtractPreviewContainer();
                //filename = "Z:\\GitHub\\flashdevelop-preview-container\\bin\\flash\\bin\\flashdeveloppreviewcontainer.swf";
                player.LoadMovie(0, filename);
                player.Play();
            }
            return player;
        }

        private AxShockwaveFlashObjects.AxShockwaveFlash CreatePreviewPlayer() {
            AxShockwaveFlashObjects.AxShockwaveFlash player = new AxShockwaveFlashObjects.AxShockwaveFlash();
            ((System.ComponentModel.ISupportInitialize)(player)).BeginInit();
            player.Dock = System.Windows.Forms.DockStyle.Fill;
            player.Enabled = true;
            ((System.ComponentModel.ISupportInitialize)(player)).EndInit();
            player.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(player_FlashCall);
            this.applicationDescriptor = new OpenFLApplicationDescriptor(pluginMain);
            return player;
        }

        private void player_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e) {
            AxShockwaveFlashObjects.AxShockwaveFlash player = sender as AxShockwaveFlashObjects.AxShockwaveFlash;
            int paneIndex = players.IndexOf(player);

            XmlDocument document = new XmlDocument();
            document.LoadXml(e.request);
            XmlAttributeCollection attributes = document.FirstChild.Attributes;
            String command = attributes.Item(0).InnerText;
            XmlNodeList list = document.GetElementsByTagName("arguments");
            string resourceId = null;
            string resourcePath = null;
            switch (command) {
                case "callbacksReady":
                        UpdatePreview(paneIndex);
                    break;

                case "getBitmapData":
                        resourceId = list[0].InnerText;
                        resourcePath = this.applicationDescriptor.ResolveResource(resourceId);
                        //pluginMain.ConsoleLog("Bitmap '" + resourceId + "' resolved to: " + resourcePath);
                        if (resourceId != null) {
                            List<int> ints = new List<int>();
                            Bitmap bmp = new Bitmap(resourcePath);
                            for (int y = 0; y < bmp.Height; y++) {
                                for (int x = 0; x < bmp.Width; x++) {
                                    Color pixel = bmp.GetPixel(x, y);
                                    ints.Add(pixel.ToArgb());
                                }
                            }

                            List<byte> bytes = new List<byte>();
                            foreach (int i in ints) {
                                bytes.AddRange(BitConverter.GetBytes(i));
                            }

                            String base64 = Convert.ToBase64String(bytes.ToArray());
                            string xmlReturnValue = "<string>" + bmp.Width + "|" + bmp.Height + "|" + base64 + "</string>";
                            player.SetReturnValue(xmlReturnValue);
                        }
                    break;

                case "getText":
                    resourceId = list[0].InnerText;
                    resourcePath = this.applicationDescriptor.ResolveResource(resourceId);
                    if (resourceId != null) {
                        string fileContents = File.ReadAllText(resourcePath);
                        string xmlReturnValue = "<string>" + fileContents + "</string>";
                        player.SetReturnValue(xmlReturnValue);
                    }
                    break;

                case "getBytes":
                    resourceId = list[0].InnerText;
                    resourcePath = this.applicationDescriptor.ResolveResource(resourceId);
                    if (resourceId != null) {
                        Byte[] fileContents = File.ReadAllBytes(resourcePath);
                        String base64 = Convert.ToBase64String(fileContents);
                        string xmlReturnValue = "<string>" + base64 + "</string>";
                        player.SetReturnValue(xmlReturnValue);
                    }
                    break;
            }
        }

        public void UpdatePreviews() {
            if (HasPreviewPlayer(0)) {
                UpdatePreview(0);
            }
            if (HasPreviewPlayer(1)) {
                UpdatePreview(1);
            }
            if (pluginMain.settingObject.HideMiniMap == true) {
                GetMiniMap().Hide();
            }
        }

        private void UpdatePreview(int paneIndex) {
            ITabbedDocument tdoc = PluginBase.MainForm.CurrentDocument as ITabbedDocument;
            ScintillaNet.ScintillaControl editor = (paneIndex == 0) ? tdoc.SplitSci1 : tdoc.SplitSci2;
            AxShockwaveFlashObjects.AxShockwaveFlash player = players[paneIndex];
            string xmlString = editor.Text;
            try {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xmlString);
            } catch {
                return;
            }
            player.CallFunction("<invoke name=\"updateLayout\" returntype=\"xml\"><arguments><string>" + xmlString + "</string></arguments></invoke>");
        }

        private Control GetMiniMap() {
            ITabbedDocument tdoc = PluginBase.MainForm.CurrentDocument as ITabbedDocument;
            return tdoc.Controls[1];
        }

        public void Dispose() {
            foreach (AxShockwaveFlashObjects.AxShockwaveFlash player in players) {
                player.Dispose();
            }
            players = new List<AxShockwaveFlashObjects.AxShockwaveFlash>();

            foreach (Messir.Windows.Forms.TabStrip strip in strips) {
                strip.Dispose();
            }
            strips = new List<Messir.Windows.Forms.TabStrip>();
        }
    }
}
