using AssemblyBrowser.Core.AssemblyClasses;

namespace AssemblyBrowser.Tests
{
    public class Tests
    {
        Core.AssemblyBrowser assemblyBrowser = new Core.AssemblyBrowser();
        AssemblyInformation ai;
        [SetUp]
        public void Setup()
        {
            ai = assemblyBrowser.GetAssemblyInformation("LibForTesting.dll", 0);
        }

        [Test]
        public void NamespacesNameAndCount()
        {
            Assert.That(ai, Is.Not.Null);
            Assert.That(ai.Namespaces, Is.Not.Null);
            Assert.That(ai.Namespaces, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(ai.Namespaces[0].Name, Is.AnyOf("Namespace1", "Namespace2"));
                Assert.That(ai.Namespaces[1].Name, Is.AnyOf("Namespace1", "Namespace2"));
                Assert.That(ai.Namespaces[1].Name, Is.Not.EqualTo(ai.Namespaces[0].Name));
            });
        }

        [Test]
        public void TypesNameAndCount()
        {
            Namespace? n1 = ai.Namespaces.Find(o => o.Name == "Namespace1");
            Namespace? n2 = ai.Namespaces.Find(o => o.Name == "Namespace2");

            Assert.That(n1, Is.Not.Null);
            Assert.That(n1.Types, Is.Not.Null);
            Assert.That(n1.Types, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(n1.Types[0].Name, Is.AnyOf("Class1", "StaticClass"));
                Assert.That(n1.Types[1].Name, Is.AnyOf("Class1", "StaticClass"));
                Assert.That(n1.Types[0].Name, Is.Not.EqualTo(n1.Types[1].Name));
            });

            Assert.That(n2, Is.Not.Null);
            Assert.That(n2.Types, Is.Not.Null);
            Assert.That(n2.Types, Has.Count.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(n2.Types[0].Name, Is.EqualTo("Class2"));
            });
        }

        [Test]
        public void FieldsNameAndCount()
        {

        }
    }
}