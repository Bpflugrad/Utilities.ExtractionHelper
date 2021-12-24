using System;
using WrapperGenerator.Wrappers;
using WrapperGenerator.Wrappers.Interfaces;

namespace BPflugrad.Utilities.WrapperGenerator
{
    public class Generator
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly string _namespaceBase;

        public Generator(string namespaceBase, IFileWrapper fileWrapper = null)
        {
            _fileWrapper = fileWrapper ?? new FileWrapper();
            _namespaceBase = namespaceBase;
        }

        public void Generate(params Type[] types)
        {
            foreach(var type in types)
            {
                var outputString = $"namespace {_namespaceBase}.Interfaces\n{{\n\tpublic interface I{type.Name}Wrapper\n\t{{\n";

                foreach(var method in type.GetMethods())
                {
                    if (method.DeclaringType == typeof(object))
                        continue;

                    outputString += $"\t\t{method.ReturnType.Name.ToLower()} {method.Name}(";
                    foreach(var parameter in method.GetParameters())
                    {
                        outputString += $"{parameter.ParameterType.Name.ToLower()} {parameter.Name}, ";
                    }
                    outputString = outputString[..^2];
                    outputString += ");\n";
                }

                outputString += "\t}\n}";

                _fileWrapper.WriteAllText($"Interfaces/I{type.Name}Wrapper.cs", outputString);

                var privatePropertyName = $"_{type.Name[0].ToString().ToLower()}{type.Name.Substring(1)}";

                outputString = string.Empty;
                outputString += $"using {_namespaceBase}.Interfaces;\n";
                outputString += $"using {type.Namespace};\n";
                outputString += "\n";
                outputString += $"namespace {_namespaceBase}\n{{\n\tpublic class {type.Name}Wrapper : I{type.Name}Wrapper\n\t{{\n";
                outputString += $"\t\tprivate readonly {type.Name} {privatePropertyName} = new {type.Name}();\n";
                outputString += "\n";
                foreach (var method in type.GetMethods())
                {
                    if (method.DeclaringType == typeof(object))
                        continue;

                    outputString += $"\t\tpublic {method.ReturnType.Name.ToLower()} {method.Name}(";
                    foreach (var parameter in method.GetParameters())
                    {
                        outputString += $"{parameter.ParameterType.Name.ToLower()} {parameter.Name}, ";
                    }
                    outputString = outputString[..^2];
                    outputString += $") => {privatePropertyName}.{method.Name}(";
                    foreach (var parameter in method.GetParameters())
                    {
                        outputString += $"{parameter.Name}, ";
                    }
                    outputString = outputString[..^2];
                    outputString += ");\n";
                }

                outputString += "\t}\n}";

                _fileWrapper.WriteAllText($"{type.Name}Wrapper.cs", outputString);
            }
        }
    }
}
