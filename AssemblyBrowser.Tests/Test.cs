using AssemblyBrowser.Core.AssemblyClasses;

namespace AssemblyBrowser.Tests
{
    public class Tests
    {
        Core.AssemblyBrowser assemblyBrowser;
        AssemblyInformation assemblyInformation;
        [SetUp]
        public void Setup()
        {
            assemblyBrowser = new Core.AssemblyBrowser();
            assemblyInformation = assemblyBrowser.GetAssemblyInformation("LibForTesting.dll", 0);
        }

        [Test]
        public void ExtensionMethod()
        {
            Assert.That(assemblyInformation.Namespaces.Count, Is.EqualTo(2));
        }
    }
}