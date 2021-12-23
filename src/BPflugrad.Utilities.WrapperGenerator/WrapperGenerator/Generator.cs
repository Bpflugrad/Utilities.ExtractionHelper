using System;
using WrapperGenerator.Wrappers;
using WrapperGenerator.Wrappers.Interfaces;

namespace BPflugrad.Utilities.WrapperGenerator
{
    public class Generator
    {
        private readonly IFileWrapper _fileWrapper;

        public Generator(IFileWrapper fileWrapper = null)
        {
            _fileWrapper = fileWrapper ?? new FileWrapper();
        }

        public void Generate(params Type[] types)
        {
            foreach(var type in types)
            {
                var outputString = $"public interface I{type.Name}\n{{\n";

                foreach(var method in type.GetMethods())
                {
                    if (method.DeclaringType == typeof(object))
                        continue;

                    outputString += $"\t{method.ReturnType.Name.ToLower()} {method.Name}(";
                    foreach(var parameter in method.GetParameters())
                    {
                        outputString += $"{parameter.ParameterType.Name.ToLower()} {parameter.Name}, ";
                    }
                    outputString = outputString[..^2];
                    outputString += ");\n";
                }

                outputString += "}";

                _fileWrapper.WriteAllText($"Interfaces/I{type.Name}.cs", outputString);
            }
        }
    }
}
