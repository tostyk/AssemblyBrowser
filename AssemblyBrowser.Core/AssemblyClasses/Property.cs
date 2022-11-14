using System.Reflection;

namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class Property
    {
        public string Name;
        public string Type;
        public AccessModifier AccessModifier;
        public AccessModifier GetterModifier;
        public AccessModifier SetterModifier;
        public Property()
        {

        }
        public Property(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            Type = propertyInfo.PropertyType.Name;
            MethodInfo? setter = propertyInfo.SetMethod;
            MethodInfo? getter = propertyInfo.GetMethod;

            if (getter == null)
                GetterModifier = AccessModifier.None;
            else if (getter.IsPublic)
                GetterModifier = AccessModifier.Public;
            else if (getter.IsAssembly)
                GetterModifier = AccessModifier.Internal;
            else if (getter.IsFamily)
                GetterModifier = AccessModifier.Protected;
            else if (getter.IsPrivate)
                GetterModifier = AccessModifier.Private;

            if (setter == null)
                SetterModifier = AccessModifier.None;
            else if (setter.IsPublic)
                SetterModifier = AccessModifier.Public;
            else if (setter.IsAssembly)
                SetterModifier = AccessModifier.Internal;
            else if (setter.IsFamily)
                SetterModifier = AccessModifier.Protected;
            else if (setter.IsPrivate)
                SetterModifier = AccessModifier.Private;

            if (SetterModifier == AccessModifier.Public || GetterModifier == AccessModifier.Public)
                AccessModifier = AccessModifier.Public;
            else if (SetterModifier == AccessModifier.Protected || GetterModifier == AccessModifier.Protected)
                AccessModifier = AccessModifier.Protected;
            else if (SetterModifier == AccessModifier.Internal || GetterModifier == AccessModifier.Internal)
                AccessModifier = AccessModifier.Internal;
            else if (SetterModifier == AccessModifier.Private || GetterModifier == AccessModifier.Private)
                AccessModifier = AccessModifier.Private;
        }
    }
}
