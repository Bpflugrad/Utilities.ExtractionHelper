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
            interfaceString += $"\tpublic interface I{nameof(SimpleUnmockableClass)}Wrapper\n";
            interfaceString += "\t{\n";
            interfaceString += "\t\tstring ReturnString(string input);\n";
            interfaceString += "\t}\n";
            interfaceString += "}";

            Assert.Equal(interfaceString, fileWrapper.ContentsDictionary[$"Interfaces/I{nameof(SimpleUnmockableClass)}Wrapper.cs"]);

            var classString = "using Tests.Interfaces;\n";
            classString += "using BPflugrad.Utilities.WrapperGenerator.Tests;\n";
            classString += "\n";
            classString += "namespace Tests\n";
            classString += "{\n";
            classString += $"\tpublic class {nameof(SimpleUnmockableClass)}Wrapper : I{nameof(SimpleUnmockableClass)}Wrapper\n";
            classString += "\t{\n";
            classString += $"\t\tprivate readonly SuccessfulTests.{nameof(SimpleUnmockableClass)} _simpleUnmockableClass = new SuccessfulTests.{nameof(SimpleUnmockableClass)}();\n";
            classString += "\n";
            classString += "\t\tpublic string ReturnString(string input)\n";
            classString += "\t\t\t=> _simpleUnmockableClass.ReturnString(input);\n";
            classString += "\t}\n";
            classString += "}";

            Assert.Equal(classString, fileWrapper.ContentsDictionary[$"{nameof(SimpleUnmockableClass)}Wrapper.cs"]);
        }

        [Fact]
        public void PublicReadonlyFieldClassExpectsSuccess()
        {
            Assert.Throws<NotSupportedException>(() => new Mock<PublicReadonlyFieldClass>());

            var fileWrapper = new FileWrapper();

            var generator = new Generator("ComplexTests", fileWrapper);

            generator.Build(typeof(PublicReadonlyFieldClass));

            var interfaceString = "namespace ComplexTests.Interfaces\n";
            interfaceString += "{\n";
            interfaceString += "\tpublic interface IPublicReadonlyFieldClassWrapper\n";
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

            Assert.Equal(interfaceString, fileWrapper.ContentsDictionary["Interfaces/IPublicReadonlyFieldClassWrapper.cs"]);

            var classString = "using ComplexTests.Interfaces;\n";
            classString += "using BPflugrad.Utilities.WrapperGenerator.Tests;\n";
            classString += "\n";
            classString += "namespace ComplexTests\n";
            classString += "{\n";
            classString += "\tpublic class PublicReadonlyFieldClassWrapper : IPublicReadonlyFieldClassWrapper\n";
            classString += "\t{\n";
            classString += "\t\tprivate readonly SuccessfulTests.PublicReadonlyFieldClass _publicReadonlyFieldClass = new SuccessfulTests.PublicReadonlyFieldClass();\n";
            classString += "\n";
            classString += "\t\tpublic short PublicReadonlyShortField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyShortField;\n";
            classString += "\t\tpublic int PublicReadonlyIntField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyIntField;\n";
            classString += "\t\tpublic long PublicReadonlyLongField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyLongField;\n";
            classString += "\t\tpublic sbyte PublicReadonlySByteField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlySByteField;\n";
            classString += "\t\tpublic byte PublicReadonlyByteField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyByteField;\n";
            classString += "\t\tpublic ushort PublicReadonlyUShortField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyUShortField;\n";
            classString += "\t\tpublic uint PublicReadonlyUIntField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyUIntField;\n";
            classString += "\t\tpublic ulong PublicReadonlyULongField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyULongField;\n";
            classString += "\t\tpublic nint PublicReadonlyNIntField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyNIntField;\n";
            classString += "\t\tpublic nuint PublicReadonlyNUIntField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyNUIntField;\n";
            classString += "\t\tpublic float PublicReadonlyFloatField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyFloatField;\n";
            classString += "\t\tpublic double PublicReadonlyDoubleField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyDoubleField;\n";
            classString += "\t\tpublic decimal PublicReadonlyDecimalField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyDecimalField;\n";
            classString += "\t\tpublic string PublicReadonlyStringField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyStringField;\n";
            classString += "\t\tpublic object PublicReadonlyObjectField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyObjectField;\n";
            classString += "\t\tpublic bool PublicReadonlyBoolField { get; }\n";
            classString += "\t\t\t=> _publicReadonlyFieldClass.PublicReadonlyBoolField;\n";
            classString += "\t}\n";
            classString += "}";

            Assert.Equal(classString, fileWrapper.ContentsDictionary["PublicReadonlyFieldClassWrapper.cs"]);
        }

        [Fact]
        public void PublicStaticGetOnlyPropertyClassExpectsSuccess()
        {
            var fileWrapper = new FileWrapper();

            var generator = new Generator("ComplexTests", fileWrapper);

            generator.Build(typeof(PublicStaticGetOnlyPropertyClass));

            var interfaceString = "namespace ComplexTests.Interfaces\n";
            interfaceString += "{\n";
            interfaceString += $"\tpublic interface I{nameof(PublicStaticGetOnlyPropertyClass)}Wrapper\n";
            interfaceString += "\t{\n";
            interfaceString += "\t\tstring PublicGetOnlyStringProperty { get; }\n";
            interfaceString += "\t}\n";
            interfaceString += "}";

            Assert.Equal(interfaceString, fileWrapper.ContentsDictionary[$"Interfaces/I{nameof(PublicStaticGetOnlyPropertyClass)}Wrapper.cs"]);

            var classString = "using ComplexTests.Interfaces;\n";
            classString += "using BPflugrad.Utilities.WrapperGenerator.Tests;\n";
            classString += "\n";
            classString += "namespace ComplexTests\n";
            classString += "{\n";
            classString += $"\tpublic class {nameof(PublicStaticGetOnlyPropertyClass)}Wrapper : I{nameof(PublicStaticGetOnlyPropertyClass)}Wrapper\n";
            classString += "\t{\n";
            classString += $"\t\tprivate readonly SuccessfulTests.{nameof(PublicStaticGetOnlyPropertyClass)} {PrivateName(nameof(PublicStaticGetOnlyPropertyClass))} = new SuccessfulTests.{nameof(PublicStaticGetOnlyPropertyClass)}();\n";
            classString += "\n";
            classString += "\t\tpublic string PublicGetOnlyStringProperty { get; }\n";
            classString += $"\t\t\t=> {PrivateName(nameof(PublicStaticGetOnlyPropertyClass))}.PublicGetOnlyStringProperty;\n";
            classString += "\t}\n";
            classString += "}";

            Assert.Equal(classString, fileWrapper.ContentsDictionary[$"{nameof(PublicStaticGetOnlyPropertyClass)}Wrapper.cs"]);
        }

        [Fact]
        public void PublicStaticFieldClassExpectsSuccess()
        {
            var fileWrapper = new FileWrapper();

            var generator = new Generator("ComplexTests", fileWrapper);

            generator.Build(typeof(PublicStaticFieldClass));

            var interfaceString = "namespace ComplexTests.Interfaces\n";
            interfaceString += "{\n";
            interfaceString += $"\tpublic interface I{nameof(PublicStaticFieldClass)}Wrapper\n";
            interfaceString += "\t{\n";
            interfaceString += "\t\tstring PublicStaticStringField { get; set; }\n";
            interfaceString += "\t\tint PublicStaticIntField { get; set; }\n";
            interfaceString += "\t}\n";
            interfaceString += "}";

            Assert.Equal(interfaceString, fileWrapper.ContentsDictionary[$"Interfaces/I{nameof(PublicStaticFieldClass)}Wrapper.cs"]);

            var classString = "using ComplexTests.Interfaces;\n";
            classString += "using BPflugrad.Utilities.WrapperGenerator.Tests;\n";
            classString += "\n";
            classString += "namespace ComplexTests\n";
            classString += "{\n";
            classString += $"\tpublic class {nameof(PublicStaticFieldClass)}Wrapper : I{nameof(PublicStaticFieldClass)}Wrapper\n";
            classString += "\t{\n";
            classString += $"\t\tprivate readonly SuccessfulTests.{nameof(PublicStaticFieldClass)} {PrivateName(nameof(PublicStaticFieldClass))} = new SuccessfulTests.{nameof(PublicStaticFieldClass)}();\n";
            classString += "\n";
            classString += "\t\tpublic string PublicStaticStringField\n";
            classString += "\t\t{\n";
            classString += $"\t\t\tget => {PrivateName(nameof(PublicStaticFieldClass))}.PublicStaticStringField;\n";
            classString += $"\t\t\tset => {PrivateName(nameof(PublicStaticFieldClass))}.PublicStaticStringField = value;\n";
            classString += "\t\t}\n";
            classString += "\t\tpublic int PublicStaticIntField\n";
            classString += "\t\t{\n";
            classString += $"\t\t\tget => {PrivateName(nameof(PublicStaticFieldClass))}.PublicStaticIntField;\n";
            classString += $"\t\t\tset => {PrivateName(nameof(PublicStaticFieldClass))}.PublicStaticIntField = value;\n";
            classString += "\t\t}\n";
            classString += "\t}\n";
            classString += "}";

            Assert.Equal(classString, fileWrapper.ContentsDictionary[$"{nameof(PublicStaticFieldClass)}Wrapper.cs"]);
        }

        public sealed class SimpleUnmockableClass
        {
            public string ReturnString(string input)
                => input;
        }

        public sealed class PublicReadonlyFieldClass
        {
            // Should not appear in the output.
            private readonly int _privateReadonlyIntField;
            // Should not appear in the output.
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
        }

        public class PublicStaticGetOnlyPropertyClass
        {
            // Should not appear in the output.
            private static string PrivateGetOnlyStringProperty { get; }
            public static string PublicGetOnlyStringProperty { get; }
        }

        public static class PublicStaticFieldClass
        {
            public static string PublicStaticStringField;
            public static int PublicStaticIntField;
        }

        private static string PrivateName(string name) => $"_{name[0].ToString().ToLower()}{name[1..]}";

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
