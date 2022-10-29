using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Core
{
    public class AssemblyBrowser : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AssemblyInformation GetAssemblyInformation(string filePath)
        {
            AssemblyInformation assemblyInformation = new AssemblyInformation(filePath);
            try
            {
                Assembly asm = Assembly.LoadFrom(filePath);
                Type[] t = asm.GetTypes();
                Namespace _namespace;
                DataType dataType;
                if (t != null)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        if (t[i].IsSubclassOf(typeof(Attribute)))
                        {
                            continue;
                        }
                        int index = assemblyInformation.Namespaces.FindIndex(o => o.Name == t[i].Namespace);
                        if (index == -1)
                        {
                            _namespace = new Namespace(t[i].Namespace);
                            assemblyInformation.Namespaces.Add(_namespace);
                        }
                        else
                        {
                            _namespace = assemblyInformation.Namespaces[index];
                        }
                        dataType = new DataType(t[i].Name);
                        foreach (MemberInfo memberInfo in t[i].GetMembers())
                        {
                            switch (memberInfo.MemberType)
                            {
                                case MemberTypes.Field:
                                    FieldInfo? fieldInfo = memberInfo as FieldInfo;
                                    if (fieldInfo != null)
                                    {
                                        dataType.Fields.Add(new Field(fieldInfo.Name, fieldInfo.FieldType.Name));
                                    }
                                    break;
                                case MemberTypes.Property:
                                    PropertyInfo? propertyInfo = memberInfo as PropertyInfo;
                                    if (propertyInfo != null)
                                    {
                                        dataType.Properties.Add(new Property(propertyInfo.Name, propertyInfo.PropertyType.Name));
                                    }
                                    break;
                                case MemberTypes.Method:
                                    MethodInfo? methodInfo = memberInfo as MethodInfo;
                                    if (methodInfo != null)
                                    {
                                        var attributes = methodInfo.CustomAttributes;
                                        if (attributes.Count(o => o.AttributeType == typeof(ExtensionAttribute)) > 0)
                                        {
                                            _namespace.DataTypes;
                                        }
                                        Method method = new Method(methodInfo.Name, methodInfo.ReturnType.Name);
                                        ParameterInfo[] parameters = methodInfo.GetParameters();
                                        method.Parameters = new Parameter[parameters.Length];
                                        for (int j = 0; j < parameters.Length; j++)
                                        {
                                            method.Parameters[j] = new Parameter(parameters[j].ParameterType.Name, parameters[j].Name);
                                        }
                                        dataType.Methods.Add(method);
                                    }
                                    break;
                            }
                        }
                        _namespace.DataTypes.Add(dataType);
                    }
                }
            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.Message);
            }
            return assemblyInformation;
        }
    }
}
