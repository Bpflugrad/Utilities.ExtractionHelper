using System;
using Xunit;
using WrapperGenerator.Wrappers.Interfaces;
using System.Collections.Generic;
using Moq;

namespace BPflugrad.Utilities.WrapperGenerator.Tests
{
    public class SuccessfulTests
    {
        [Fact]
        public void SimpleSealedClassExpectsSuccess()
        {
            Assert.Throws<NotSupportedException>(() => new Mock<SimpleUnmockableClass>());

            var fileWrapper = new FileWrapper();

            var generator = new Generator("Tests", fileWrapper);

            generator.Build(typeof(SimpleUnmockableClass));

            var interfaceString = "namespace Tests.Interfaces\n";
            interfaceString += "{\n";
            interfaceString += "\tpublic interface ISimpleUnmockableClassWrapper\n";
            interfaceString += "\t{\n";
            interfaceString += "\t\tstring ReturnString(string input);\n";
            interfaceString += "\t}\n";
            interfaceString += "}";

            Assert.Equal(interfaceString, fileWrapper.ContentsDictionary["Interfaces/ISimpleUnmockableClassWrapper.cs"]);

            var classString = "using Tests.Interfaces;\n";
            classString += "using BPflugrad.Utilities.WrapperGenerator.Tests;\n";
            classString += "\n";
            classString += "namespace Tests\n";
            classString += "{\n";
            classString += "\tpublic class SimpleUnmockableClassWrapper : ISimpleUnmockableClassWrapper\n";
            classString += "\t{\n";
            classString += "\t\tprivate readonly SuccessfulTests.SimpleUnmockableClass _simpleUnmockableClass = new SuccessfulTests.SimpleUnmockableClass();\n";
            classString += "\n";
            classString += "\t\tpublic string ReturnString(string input)\n";
            classString += "\t\t\t=> _simpleUnmockableClass.ReturnString(input);\n";
            classString += "\t}\n";
            classString += "}";

            Assert.Equal(classString, fileWrapper.ContentsDictionary["SimpleUnmockableClassWrapper.cs"]);
        }

        [Fact]
        public void ComplexUnmockableClassExpectsSuccess()
        {
            Assert.Throws<NotSupportedException>(() => new Mock<ComplexUnmockableClass>());

            var fileWrapper = new FileWrapper();

            var generator = new Generator("ComplexTests", fileWrapper);

            generator.Build(typeof(ComplexUnmockableClass));

            //Assert.Equal("namespace ComplexTests.Interfaces\n{\n\tpublic interface IComplexUnmockableClassWrapper\n\t{\n\t\tint PublicReadonlyIntField { get; }\n\t\tstring PublicStringField { get; set;}\n\t\tlong PublicLongProperty { get; set; }\n\t\tint PublicGetOnlyIntProperty { get; }\n\t}\n}", fileWrapper.ContentsDictionary["Interfaces/IComplexUnmockableClassWrapper.cs"]);
            var interfaceString = "namespace ComplexTests.Interfaces\n";
            interfaceString += "{\n";
            interfaceString += "\tpublic interface IComplexUnmockableClassWrapper\n";
            interfaceString += "\t{\n";
            interfaceString += "\t\tshort PublicReadonlyShortField { get; }\n";
            interfaceString += "\t\tint PublicReadonlyIntField { get; }\n";
            interfaceString += "\t\tlong PublicReadonlyLongField { get; }\n";
            interfaceString += "\t\tsbyte PublicReadonlySByteField { get; }\n";
            interfaceString += "\t\tbyte PublicReadonlyByteField { get; }\n";
            interfaceString += "\t\tushort PublicReadonlyUShortField { get; }\n";
            interfaceString += "\t\tuint PublicReadonlyUIntField { get; }\n";
            interfaceString += "\t\tulong PublicReadonlyULongField { get; }\n";
            interfaceString += "\t\tnint PublicReadonlyNIntField { get; }\n";
            interfaceString += "\t\tnuint PublicReadonlyNUIntField { get; }\n";
            interfaceString += "\t\tfloat PublicReadonlyFloatField { get; }\n";
            interfaceString += "\t\tdouble PublicReadonlyDoubleField { get; }\n";
            interfaceString += "\t\tdecimal PublicReadonlyDecimalField { get; }\n";
            interfaceString += "\t\tstring PublicReadonlyStringField { get; }\n";
            interfaceString += "\t\tobject PublicReadonlyObjectField { get; }\n";
            interfaceString += "\t\tbool PublicReadonlyBoolField { get; }\n";
            interfaceString += "\t}\n";
            interfaceString += "}";

            Assert.Equal(interfaceString, fileWrapper.ContentsDictionary["Interfaces/IComplexUnmockableClassWrapper.cs"]);

            var classString = "using ComplexTests.Interfaces;\n";
            classString += "using BPflugrad.Utilities.WrapperGenerator.Tests;\n";
            classString += "\n";
            classString += "namespace ComplexTests\n";
            classString += "{\n";
            classString += "\tpublic class ComplexUnmockableClassWrapper : IComplexUnmockableClassWrapper\n";
            classString += "\t{\n";
            classString += "\t\tprivate readonly SuccessfulTests.ComplexUnmockableClass _complexUnmockableClass = new SuccessfulTests.ComplexUnmockableClass();\n";
            classString += "\n";
            classString += "\t\tpublic short PublicReadonlyShortField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyShortField;\n";
            classString += "\t\tpublic int PublicReadonlyIntField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyIntField;\n";
            classString += "\t\tpublic long PublicReadonlyLongField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyLongField;\n";
            classString += "\t\tpublic sbyte PublicReadonlySByteField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlySByteField;\n";
            classString += "\t\tpublic byte PublicReadonlyByteField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyByteField;\n";
            classString += "\t\tpublic ushort PublicReadonlyUShortField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyUShortField;\n";
            classString += "\t\tpublic uint PublicReadonlyUIntField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyUIntField;\n";
            classString += "\t\tpublic ulong PublicReadonlyULongField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyULongField;\n";
            classString += "\t\tpublic nint PublicReadonlyNIntField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyNIntField;\n";
            classString += "\t\tpublic nuint PublicReadonlyNUIntField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyNUIntField;\n";
            classString += "\t\tpublic float PublicReadonlyFloatField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyFloatField;\n";
            classString += "\t\tpublic double PublicReadonlyDoubleField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyDoubleField;\n";
            classString += "\t\tpublic decimal PublicReadonlyDecimalField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyDecimalField;\n";
            classString += "\t\tpublic string PublicReadonlyStringField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyStringField;\n";
            classString += "\t\tpublic object PublicReadonlyObjectField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyObjectField;\n";
            classString += "\t\tpublic bool PublicReadonlyBoolField { get; }\n";
            classString += "\t\t\t=> _complexUnmockableClass.PublicReadonlyBoolField;\n";
            classString += "\t}\n";
            classString += "}";

            Assert.Equal(classString, fileWrapper.ContentsDictionary["ComplexUnmockableClassWrapper.cs"]);
        }

        public sealed class SimpleUnmockableClass
        {
            public string ReturnString(string input)
                => input;
        }

        public sealed class ComplexUnmockableClass
        {
            private readonly int _privateReadonlyIntField;
            internal int ProtectedIntField { get; set; }
            public readonly short PublicReadonlyShortField;
            public readonly int PublicReadonlyIntField;
            public readonly long PublicReadonlyLongField;
            public readonly sbyte PublicReadonlySByteField;
            public readonly byte PublicReadonlyByteField;
            public readonly ushort PublicReadonlyUShortField;
            public readonly uint PublicReadonlyUIntField;
            public readonly ulong PublicReadonlyULongField;
            public readonly nint PublicReadonlyNIntField;
            public readonly nuint PublicReadonlyNUIntField;
            public readonly float PublicReadonlyFloatField;
            public readonly double PublicReadonlyDoubleField;
            public readonly decimal PublicReadonlyDecimalField;
            public readonly string PublicReadonlyStringField;
            public readonly object PublicReadonlyObjectField;
            public readonly bool PublicReadonlyBoolField;
            //public string PublicStringField;
            //public long PublicLongProperty { get; set; }
            //public int PublicGetOnlyIntProperty { get; }

            //public ComplexUnmockableClass(int privateReadonlyIntField)
            //{
            //    _privateReadonlyIntField = privateReadonlyIntField;
            //}
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
