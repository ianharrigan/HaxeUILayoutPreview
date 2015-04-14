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

namespace HaxeUILayoutPreview {
    class OpenFLApplicationDescriptor {
        private PluginMain pluginMain;
        private XmlDocument document;
        public OpenFLApplicationDescriptor(PluginMain pluginMain) {
            this.pluginMain = pluginMain;
            Load();
        }

        public void Load() {
            string filename = null;
            if (File.Exists(PluginBase.CurrentProject.GetAbsolutePath("application.xml"))) {
                filename = "application.xml";
            } else if (File.Exists(PluginBase.CurrentProject.GetAbsolutePath("project.xml"))) {
                filename = "project.xml";
            }

            if (filename != null) {
                filename = PluginBase.CurrentProject.GetAbsolutePath(filename);
                document = new XmlDocument();
                document.LoadXml(File.ReadAllText(filename));

            }
        }

        public string ResolveResource(string resourceId) {
            string path = null;
            XmlNodeList assetNodes = document.SelectNodes("//assets");
            foreach (XmlNode assetNode in assetNodes) {
                string assetPath = assetNode.Attributes.GetNamedItem("path").Value;
                string assetRename = assetNode.Attributes.GetNamedItem("rename").Value;
                if (resourceId.StartsWith(assetRename)) {
                    string temp = string.Copy(resourceId);
                    temp = temp.Substring(assetRename.Length + 1, resourceId.Length - assetRename.Length - 1);
                    temp = PluginBase.CurrentProject.GetAbsolutePath(assetPath + "/" + temp);
                    if (File.Exists(temp)) {
                        path = temp;
                        break;
                    }
                }
            }
            return path;
        }
    }
}
