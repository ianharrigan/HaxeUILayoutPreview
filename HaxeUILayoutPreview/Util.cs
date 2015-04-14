using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace HaxeUILayoutPreview {
    class Util {
        public static string ExtractPreviewContainer() {
            string tempfile = Path.GetTempPath() + "flashdeveloppreviewcontainer.swf";
            //string tempfile = "C:\\temp\\test.swf";
            byte[] data = Resources.flashdeveloppreviewcontainer;
            File.WriteAllBytes(tempfile, data);
            return tempfile;
        }
    }
}
