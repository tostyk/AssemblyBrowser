using AssemblyBrowser.Core.AssemblyClasses;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Core
{
    public class AssemblyBrowser 
    {
        public AssemblyInformation GetAssemblyInformation(string filePath, AssemblyBrowserFlags flags)
        {
            AssemblyInformation assemblyInformation = new AssemblyInformation(filePath);
            try
            {
                Assembly asm = Assembly.LoadFrom(filePath);
                System.Type[] t = asm.GetTypes();
                Namespace _namespace;
                AssemblyClasses.Type type;
                if (t != null)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        if (t[i].IsSubclassOf(typeof(Attribute)) && !flags.HasFlag(AssemblyBrowserFlags.Attributes))
                            continue;
                        _namespace = GetOrCreateNamespace(assemblyInformation, t[i].Namespace);
                        type = GetOrCreateType(_namespace, t[i].Name);
                        MemberInfo[] members = t[i].GetMembers(
                            BindingFlags.Instance | 
                            BindingFlags.NonPublic | 
                            BindingFlags.Public | 
                            BindingFlags.Static);
                        foreach (MemberInfo memberInfo in members)
                        {
                            if (memberInfo.DeclaringType != t[i] && flags.HasFlag(AssemblyBrowserFlags.OnlyDeclaredMembers))
                                continue;
                            switch (memberInfo.MemberType)
                            {
                                case MemberTypes.Field:
                                    FieldInfo? fieldInfo = memberInfo as FieldInfo;
                                    if (fieldInfo != null && !fieldInfo.IsDefined(typeof(CompilerGeneratedAttribute)))
                                    {
                                        type.Fields.Add(new Field(fieldInfo));
                                    }
                                    break;
                                case MemberTypes.Property:
                                    PropertyInfo? propertyInfo = memberInfo as PropertyInfo;
                                    if (propertyInfo != null)
                                    {
                                        type.Properties.Add(new Property(propertyInfo));
                                    }
                                    break;
                                case MemberTypes.Method:
                                    MethodInfo? methodInfo = memberInfo as MethodInfo;
                                    if (methodInfo != null && !methodInfo.IsDefined(typeof(CompilerGeneratedAttribute)))
                                    {
                                        AddMethodToClass(methodInfo, t[i], _namespace, type);
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
            Method method;
            AccessModifier accessModifier = 0;
            if (methodInfo.IsPublic) accessModifier = AccessModifier.Public;
            if (methodInfo.IsPrivate) accessModifier = AccessModifier.Private;
            if (methodInfo.IsFamily) accessModifier = AccessModifier.Protected;
            if (methodInfo.IsAssembly) accessModifier = AccessModifier.Internal;
            // Current method is extension method
            if (attributes.Count(o => o.AttributeType == typeof(ExtensionAttribute)) > 0)
            {
                string typeName = methodInfo.GetParameters()[0].ParameterType.Name;
                dataType = GetOrCreateType(_namespace, typeName);
                method = new Method(methodInfo.Name, methodInfo.ReturnType.Name, accessModifier, true);
                ParameterInfo[] parameters = methodInfo.GetParameters();
                method.Parameters = new Parameter[parameters.Length - 1];
                for (int j = 1; j < parameters.Length; j++)
                {
                    method.Parameters[j] = new Parameter(parameters[j].ParameterType.Name, parameters[j].Name);
                }
            }
            else
            {
                int index = _namespace.Types.FindIndex(o => o.Name == t.Name);
                dataType = GetOrCreateType(_namespace, t.Name);
                method = new Method(methodInfo.Name, methodInfo.ReturnType.Name, accessModifier);
                ParameterInfo[] parameters = methodInfo.GetParameters();
                method.Parameters = new Parameter[parameters.Length];
                for (int j = 0; j < parameters.Length; j++)
                {
                    method.Parameters[j] = new Parameter(parameters[j].ParameterType.Name, parameters[j].Name);
                }
            }
            dataType.Methods.Add(method);
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
            int index = _namespace.Types.FindIndex(o => o.Name == typeName);
            if (index == -1)
            {
                dataType = new AssemblyClasses.Type(typeName);
                _namespace.Types.Add(dataType);
            }
            else
            {
                dataType = _namespace.Types[index];
            }
            return dataType;
        }
    }
}
