using System;
using WrapperGenerator.Wrappers;
using WrapperGenerator.Wrappers.Interfaces;

namespace BPflugrad.Utilities.WrapperGenerator
{
    /// <summary>
    /// Generates an interface and class capable of wrapping a class such that it can be used in unit testing.
    /// </summary>
    public class Generator
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly string _namespaceBase;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="namespaceBase">Base Namespace in which the generated code will be created.</param>
        /// <param name="fileWrapper">Optional wrapper for <seealso cref="System.IO.File"/> since it is static.</param>
        public Generator(string namespaceBase, IFileWrapper fileWrapper = null)
        {
            _fileWrapper = fileWrapper ?? new FileWrapper();
            _namespaceBase = namespaceBase;
        }

        /// <summary>
        /// Builds the interfaces and classes and outputs them to disc.
        /// </summary>
        /// <param name="types">Types to be wrapped.</param>
        public void Build(params Type[] types)
        {
            foreach(var type in types)
            {
                var interfaceString = $"namespace {_namespaceBase}.Interfaces\n";
                interfaceString += "{\n";
                interfaceString += $"\tpublic interface I{type.Name}Wrapper\n";
                interfaceString += "\t{\n";

                var privatePropertyName = $"_{type.Name[0].ToString().ToLower()}{type.Name[1..]}";

                var classString = string.Empty;
                classString += $"using {_namespaceBase}.Interfaces;\n";
                classString += $"using {type.Namespace};\n";
                classString += "\n";
                classString += $"namespace {_namespaceBase}\n";
                classString += "{\n";
                classString += $"\tpublic class {type.Name}Wrapper : I{type.Name}Wrapper\n";
                classString += "\t{\n";
                classString += $"\t\tprivate readonly {type.DeclaringType.Name}.{type.Name} {privatePropertyName} = new {type.DeclaringType.Name}.{type.Name}();\n";
                classString += "\n";

                // Fields
                foreach (var field in type.GetFields())
                {
                    if (field.DeclaringType == typeof(object))
                        continue;

                    if (field.IsPublic)
                    {
                        interfaceString += $"\t\t{GetTypeName(field.FieldType)} {field.Name} ";
                        classString += $"\t\tpublic {GetTypeName(field.FieldType)} {field.Name}";
                        if (field.IsInitOnly)
                        {
                            interfaceString += "{ get; }\n";
                            classString += " { get; }\n";
                            classString += $"\t\t\t=> {privatePropertyName}.{field.Name};\n";
                        }
                        else
                        {
                            interfaceString += "{ get; set; }\n";
                            classString += "\n";
                            classString += "\t\t{\n";
                            classString += $"\t\t\tget => {privatePropertyName}.{field.Name};\n";
                            classString += $"\t\t\tset => {privatePropertyName}.{field.Name} = value;\n";
                            classString += "\t\t}\n";
                        }
                    }
                }

                // Properties
                foreach(var property in type.GetProperties())
                {
                    interfaceString += $"\t\t{GetTypeName(property.PropertyType)} {property.Name} ";
                    classString += $"\t\tpublic {GetTypeName(property.PropertyType)} {property.Name}";
                    if (property.CanRead && !property.CanWrite)
                    {
                        interfaceString += "{ get; }\n";
                        classString += " { get; }\n";
                        classString += $"\t\t\t=> {privatePropertyName}.{property.Name};\n";
                    }
                    else if(property.CanRead && property.CanWrite)
                    {
                        interfaceString += "{ get; set; }\n";
                        classString += "\n";
                        classString += "\t\t{\n";
                        classString += $"\t\t\tget => {privatePropertyName}.{property.Name};\n";
                        classString += $"\t\t\tset => {privatePropertyName}.{property.Name} = value;\n";
                        classString += "\t\t}\n";
                    }
                }

                // Methods
                foreach(var method in type.GetMethods())
                {
                    if (method.DeclaringType == typeof(object) || method.IsSpecialName)
                        continue;

                    interfaceString += $"\t\t{GetTypeName(method.ReturnType)} {method.Name}(";
                    classString += $"\t\tpublic {GetTypeName(method.ReturnType)} {method.Name}(";
                    foreach (var parameter in method.GetParameters())
                    {
                        interfaceString += $"{GetTypeName(parameter.ParameterType)} {parameter.Name}, ";
                        classString += $"{GetTypeName(parameter.ParameterType)} {parameter.Name}, ";
                    }
                    classString = classString[..^2];
                    classString += $")\n\t\t\t=> {privatePropertyName}.{method.Name}(";
                    foreach (var parameter in method.GetParameters())
                    {
                        classString += $"{parameter.Name}, ";
                    }
                    interfaceString = interfaceString[..^2];
                    interfaceString += ");\n";
                    classString = classString[..^2];
                    classString += ");\n";
                }

                interfaceString += "\t}\n}";
                classString += "\t}\n}";

                _fileWrapper.WriteAllText($"Interfaces/I{type.Name}Wrapper.cs", interfaceString);
                _fileWrapper.WriteAllText($"{type.Name}Wrapper.cs", classString);
            }
        }

        /// <summary>
        /// Transforms .Net type names to Integral Value Types.
        /// </summary>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types"/>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types"/>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetTypeName(Type type)
        {
            if (type == typeof(int))
                return "int";
            if (type == typeof(short))
                return "short";
            if (type == typeof(long))
                return "long";
            if (type == typeof(sbyte))
                return "sbyte";
            if (type == typeof(byte))
                return "byte";
            if (type == typeof(ushort))
                return "ushort";
            if (type == typeof(uint))
                return "uint";
            if (type == typeof(ulong))
                return "ulong";
            if (type == typeof(nint))
                return "nint";
            if (type == typeof(nuint))
                return "nuint";
            if (type == typeof(float))
                return "float";
            if (type == typeof(double))
                return "double";
            if (type == typeof(decimal))
                return "decimal";
            if (type == typeof(string))
                return "string";
            if (type == typeof(object))
                return "object";
            if (type == typeof(bool))
                return "bool";

            return type.Name;
        }
        
    }
}
