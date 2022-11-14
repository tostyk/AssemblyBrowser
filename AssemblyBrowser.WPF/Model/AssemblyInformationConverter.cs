using AssemblyBrowser.Core;
using AssemblyBrowser.Core.AssemblyClasses;
using AssemblyBrowser.WPF.ViewModel;
using System.Collections.ObjectModel;
using Parameter = AssemblyBrowser.Core.AssemblyClasses.Parameter;

namespace AssemblyBrowser.WPF.Model
{
    public static class AssemblyInformationConverter
    {
        const string ClassImagePath = "images/Class.png";
        const string NamespaceImagePath = "images/Namespace.png";
        const string AssemblyImagePath = "images/Assembly.png";
        public static TreeNode ToTree(AssemblyInformation assemblyInformation)
        {
            TreeNode result;
            ObservableCollection<TreeNode> namespases = new ObservableCollection<TreeNode>();
            foreach (Namespace ns in assemblyInformation.Namespaces)
            {
                ObservableCollection<TreeNode> types = new();
                foreach (Type dataType in ns.DataTypes)
                {
                    ObservableCollection<TreeNode> members = new();
                    foreach (Field field in dataType.Fields)
                    {
                        members.Add(NodeFromField(field));
                    }
                    foreach (Property property in dataType.Properties)
                    {
                        members.Add(NodeFromProperty(property));
                    }
                    foreach (Method method in dataType.Methods)
                    {
                        members.Add(NodeFromMethod(method));
                    }
                    types.Add(new(dataType.TypeName, ClassImagePath, members));
                }
                namespases.Add(new(ns.Name, NamespaceImagePath, types));
            }
            result = new(assemblyInformation.AssemblyName, AssemblyImagePath, namespases);
            return result;
        }
        private static TreeNode NodeFromField(Field field)
        {
            string fieldinfo = field.Type + " " + field.Name;
            string image;
            switch (field.AccessModifier)
            {
                case AccessModifier.Public:
                    image = "images/FieldPublic.png";
                    break;
                case AccessModifier.Private:
                    image = "images/FieldPrivate.png";
                    break;
                case AccessModifier.Internal:
                    image = "images/FieldInternal.png";
                    break;
                case AccessModifier.Protected:
                    image = "images/FieldProtected.png";
                    break;
                default:
                    image = "images/Field.png";
                    break;
            }
            return new TreeNode(fieldinfo, image);
        }
        private static TreeNode NodeFromProperty(Property property)
        {
            string propertyinfo = property.Type + " " + property.Name + " ";
            string image;
            switch (property.AccessModifier)
            {
                case AccessModifier.Public:
                    image = "images/PropertyPublic.png";
                    break;
                case AccessModifier.Private:
                    image = "images/PropertyPrivate.png";
                    break;
                case AccessModifier.Internal:
                    image = "images/PropertyInternal.png";
                    break;
                case AccessModifier.Protected:
                    image = "images/PropertyProtected.png";
                    break;
                default:
                    image = "images/Property.png";
                    break;
            }
            propertyinfo += "{";
            if (property.GetterModifier != AccessModifier.None)
            {
                if (property.GetterModifier != property.AccessModifier)
                {
                    propertyinfo += " " + GetModifierString(property.GetterModifier);
                }
                propertyinfo += " get;";
            }
            if (property.SetterModifier != AccessModifier.None)
            {
                if (property.SetterModifier != property.AccessModifier)
                {
                    propertyinfo += " " + GetModifierString(property.SetterModifier);
                }
                propertyinfo += " set;";
            }
            propertyinfo += " }";
            return new TreeNode(propertyinfo, image);
        }
        private static TreeNode NodeFromMethod(Method method)
        {
            const string ExtensionMethodImagePath = "images/ExtensionMethod.png";

            string image;
            switch (method.AccessModifier)
            {
                case AccessModifier.Public:
                    image = "images/MethodPublic.png";
                    break;
                case AccessModifier.Private:
                    image = "images/MethodPrivate.png";
                    break;
                case AccessModifier.Internal:
                    image = "images/MethodInternal.png";
                    break;
                case AccessModifier.Protected:
                    image = "images/MethodProtected.png";
                    break;
                default:
                    image = "images/Method.png";
                    break;
            }

            string methodInfo = method.ReturnType + " " + method.Name + "(";
            foreach (Parameter parameter in method.Parameters)
            {
                methodInfo += parameter.Type + " " + parameter.Name + ", ";
            }
            if (method.Parameters.Length > 0)
            {
                methodInfo = methodInfo.Substring(0, methodInfo.LastIndexOf(','));
            }
            methodInfo += ")";
            if (method.IsExtensionMethod)
                return new TreeNode(methodInfo, ExtensionMethodImagePath);
            else
                return new TreeNode(methodInfo, image);
        }
        private static string GetModifierString(AccessModifier accessModifier)
        {
            string modifier = "";
            switch (accessModifier)
            {
                case AccessModifier.None:
                    modifier = "";
                    break;
                case AccessModifier.Public:
                    modifier = "public";
                    break;
                case AccessModifier.Private:
                    modifier = "private";
                    break;
                case AccessModifier.Internal:
                    modifier = "internal";
                    break;
                case AccessModifier.Protected:
                    modifier = "protected";
                    break;
            }
            return modifier;
        }
    }
}
