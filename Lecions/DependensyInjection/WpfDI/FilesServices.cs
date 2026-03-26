using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfDI
{
    public class FilesServices
    {
        public string[] GetFiles(string path) => Directory.GetFiles(path);
    }
}
