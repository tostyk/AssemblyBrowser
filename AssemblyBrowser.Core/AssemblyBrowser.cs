using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using AssemblyBrowser.Core.AssemblyClasses;

namespace AssemblyBrowser.Core
{
    public class AssemblyBrowser : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AssemblyInformation GetAssemblyInformation(string filePath, AssemblyBrowserFlags flags)
        {
            AssemblyInformation assemblyInformation = new AssemblyInformation(filePath);
            try
            {
                Assembly asm = Assembly.LoadFrom(filePath);
                System.Type[] t = asm.GetTypes();
                Namespace _namespace;
                AssemblyClasses.Type dataType;
                if (t != null)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        if (t[i].IsSubclassOf(typeof(Attribute)) && !flags.HasFlag(AssemblyBrowserFlags.Attributes))
                            continue;
                        _namespace = GetOrCreateNamespace(assemblyInformation, t[i].Namespace);
                        dataType = GetOrCreateType(_namespace, t[i].Name);
                        foreach (MemberInfo memberInfo in t[i].GetMembers())
                        {
                            if (memberInfo.DeclaringType != t[i] && flags.HasFlag(AssemblyBrowserFlags.OnlyDeclaredMembers))
                                continue;
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
                                        AddMethodToClass(methodInfo, t[i], _namespace, dataType);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.Message);
            }
            return assemblyInformation;
        }
        private void AddMethodToClass(MethodInfo methodInfo, System.Type t, Namespace _namespace, AssemblyClasses.Type dataType)
        {
            var attributes = methodInfo.CustomAttributes;
            // Current method is extension method
            if (attributes.Count(o => o.AttributeType == typeof(ExtensionAttribute)) > 0)
            {
                string typeName = methodInfo.GetParameters()[0].ParameterType.Name;
                dataType = GetOrCreateType(_namespace, typeName);
                Method method = new Method(methodInfo.Name, methodInfo.ReturnType.Name, true);
                ParameterInfo[] parameters = methodInfo.GetParameters();
                method.Parameters = new Parameter[parameters.Length - 1];
                for (int j = 1; j < parameters.Length; j++)
                {
                    method.Parameters[j] = new Parameter(parameters[j].ParameterType.Name, parameters[j].Name);
                }
                dataType.Methods.Add(method);
            }
            else
            {
                int index = _namespace.DataTypes.FindIndex(o => o.TypeName == t.Name);
                dataType = GetOrCreateType(_namespace, t.Name);
                Method method = new Method(methodInfo.Name, methodInfo.ReturnType.Name);
                ParameterInfo[] parameters = methodInfo.GetParameters();
                method.Parameters = new Parameter[parameters.Length];
                for (int j = 0; j < parameters.Length; j++)
                {
                    method.Parameters[j] = new Parameter(parameters[j].ParameterType.Name, parameters[j].Name);
                }
                dataType.Methods.Add(method);
            }
        }
        private Namespace GetOrCreateNamespace(AssemblyInformation ai, string namespaceName)
        {
            Namespace _namespace;
            int index = ai.Namespaces.FindIndex(o => o.Name == namespaceName);
            if (index == -1)
            {
                _namespace = new Namespace(namespaceName);
                ai.Namespaces.Add(_namespace);
            }
            else
            {
                _namespace = ai.Namespaces[index];
            }
            return _namespace;
        }
        private AssemblyClasses.Type GetOrCreateType(Namespace _namespace, string typeName)
        {
            AssemblyClasses.Type dataType;
            int index = _namespace.DataTypes.FindIndex(o => o.TypeName == typeName);
            if (index == -1)
            {
                dataType = new AssemblyClasses.Type(typeName);
                _namespace.DataTypes.Add(dataType);
            }
            else
            {
                dataType = _namespace.DataTypes[index];
            }
            return dataType;
        }
    }
}
