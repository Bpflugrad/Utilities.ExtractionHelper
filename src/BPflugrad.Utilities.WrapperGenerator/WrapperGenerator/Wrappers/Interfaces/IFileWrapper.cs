using System;
using System.Collections.Generic;
using System.Text;

namespace WrapperGenerator.Wrappers.Interfaces
{
    public interface IFileWrapper
    {
        void WriteAllText(string path, string contents);
    }
}
