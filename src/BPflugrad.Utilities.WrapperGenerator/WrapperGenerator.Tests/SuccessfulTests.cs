using System;
using Xunit;
using WrapperGenerator.Wrappers.Interfaces;
using System.Collections.Generic;

namespace BPflugrad.Utilities.WrapperGenerator.Tests
{
    public class SuccessfulTests
    {
        [Fact]
        public void SimpleSealedClassExpectsSuccess()
        {
            var fileWrapper = new FileWrapper();

            var generator = new Generator(fileWrapper);

            generator.Generate(typeof(SimpleUnmockableClass));

            Assert.Equal("public interface ISimpleUnmockableClass\n{\n\tstring ReturnString(string input);\n}", fileWrapper.ContentsDictionary["Interfaces/ISimpleUnmockableClass.cs"]);
        }

        private sealed class SimpleUnmockableClass
        {
            public string ReturnString(string input)
                => input;
        }

        private class FileWrapper : IFileWrapper
        {
            public readonly IDictionary<string, string> ContentsDictionary = new Dictionary<string, string>();

            public void WriteAllText(string path, string contents)
            {
                if (ContentsDictionary.ContainsKey(path))
                    ContentsDictionary[path] = contents;
                else
                    ContentsDictionary.Add(path, contents);
            }
        }
    }
}
