using AssemblyBrowser.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using Parameter = AssemblyBrowser.Core.Parameter;

namespace AssemblyBrowser.WPF.ViewModel
{
    public class AssemblyInformationConverter
    {
        public static TreeNode ToTree(AssemblyInformation assemblyInformation)
        {
            TreeNode result;
            ObservableCollection<TreeNode> namespases = new ObservableCollection<TreeNode>();
            foreach(Namespace ns in assemblyInformation.Namespaces)
            {
                ObservableCollection<TreeNode> types = new();
                foreach(DataType dataType in ns.DataTypes)
                {
                    ObservableCollection<TreeNode> members = new();
                    foreach(Field field in dataType.Fields)
                    {
                        string fieldinfo = field.Type + " " + field.Name;
                        members.Add(new TreeNode(fieldinfo));
                    }
                    foreach (Property property in dataType.Properties)
                    {
                        string propertyInfo = property.Type + " " + property.Name;
                        members.Add(new TreeNode(propertyInfo));
                    }
                    foreach (Method method in dataType.Methods)
                    {
                        string methodInfo = method.ReturnType + " " + method.Name + "(";
                        foreach(Parameter parameter in method.Parameters)
                        {
                            methodInfo += parameter.Type + " " + parameter.Name + ", ";
                        }
                        if (method.Parameters.Length > 0)
                        {
                            methodInfo = methodInfo.Substring(0, methodInfo.LastIndexOf(','));
                        }
                        methodInfo += ")";
                        members.Add(new TreeNode(methodInfo));
                    }
                    types.Add(new(dataType.TypeName, members));
                }
                namespases.Add(new(ns.Name, types));
            }
            result = new(assemblyInformation.AssemblyName, namespases);
            return result;
        }
    }
}
