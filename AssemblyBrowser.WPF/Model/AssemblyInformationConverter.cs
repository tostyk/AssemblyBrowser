using AssemblyBrowser.Core.AssemblyClasses;
using AssemblyBrowser.WPF.ViewModel;
using System.Collections.ObjectModel;
using Parameter = AssemblyBrowser.Core.AssemblyClasses.Parameter;

namespace AssemblyBrowser.WPF.Model
{
    public class AssemblyInformationConverter
    {
        const string ClassImagePath = "images/Class.png";
        const string MethodImagePath = "images/Method.png";
        const string FieldImagePath = "images/Field.png";
        const string PropertyImagePath = "images/Property.png";
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
                        string fieldinfo = field.Type + " " + field.Name;
                        members.Add(new TreeNode(fieldinfo, FieldImagePath));
                    }
                    foreach (Property property in dataType.Properties)
                    {
                        string propertyInfo = property.Type + " " + property.Name;
                        members.Add(new TreeNode(propertyInfo, PropertyImagePath));
                    }
                    foreach (Method method in dataType.Methods)
                    {
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
                        members.Add(new TreeNode(methodInfo, MethodImagePath));
                    }
                    types.Add(new(dataType.TypeName, ClassImagePath, members));
                }
                namespases.Add(new(ns.Name, NamespaceImagePath, types));
            }
            result = new(assemblyInformation.AssemblyName, AssemblyImagePath, namespases);
            return result;
        }
    }
}
