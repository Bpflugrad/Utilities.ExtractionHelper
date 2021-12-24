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

            var generator = new Generator("Tests", fileWrapper);

            generator.Generate(typeof(SimpleUnmockableClass));

            Assert.Equal("namespace Tests.Interfaces\n{\n\tpublic interface ISimpleUnmockableClassWrapper\n\t{\n\t\tstring ReturnString(string input);\n\t}\n}", fileWrapper.ContentsDictionary["Interfaces/ISimpleUnmockableClassWrapper.cs"]);
            Assert.Equal("using Tests.Interfaces;\nusing BPflugrad.Utilities.WrapperGenerator.Tests;\n\nnamespace Tests\n{\n\tpublic class SimpleUnmockableClassWrapper : ISimpleUnmockableClassWrapper\n\t{\n\t\tprivate readonly SimpleUnmockableClass _simpleUnmockableClass = new SimpleUnmockableClass();\n\n\t\tpublic string ReturnString(string input) => _simpleUnmockableClass.ReturnString(input);\n\t}\n}", fileWrapper.ContentsDictionary["SimpleUnmockableClassWrapper.cs"]);
        }

        public sealed class SimpleUnmockableClass
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
