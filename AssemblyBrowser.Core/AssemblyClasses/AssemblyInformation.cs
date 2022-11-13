namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class AssemblyInformation
    {
        public string AssemblyName;
        public List<Namespace> Namespaces = new List<Namespace>();
        public AssemblyInformation(string name)
        {
            AssemblyName = name;
        }
    }
}
