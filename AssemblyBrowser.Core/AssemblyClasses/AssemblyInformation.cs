namespace AssemblyBrowser.Core.AssemblyClasses
{
    public class AssemblyInformation
    {
        public string AssemblyName { get; }
        public List<Namespace> Namespaces { get; internal set; }
        public Exception Exception { get; internal set; }
        public AssemblyInformation(string name)
        {
            AssemblyName = name;
            Namespaces = new List<Namespace>();
        }
    }
}
