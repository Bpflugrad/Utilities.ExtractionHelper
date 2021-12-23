using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WrapperGenerator.Wrappers.Interfaces;

namespace WrapperGenerator.Wrappers
{
    public class FileWrapper : IFileWrapper
    {
        public void WriteAllText(string path, string contents)
            => File.WriteAllText(path, contents);
    }
}
