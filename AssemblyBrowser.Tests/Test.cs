using AssemblyBrowser.Core;
using AssemblyBrowser.Core.AssemblyClasses;

namespace AssemblyBrowser.Tests
{
    public class Tests
    {
        readonly Core.AssemblyBrowser assemblyBrowser = new();
        AssemblyInformation ai;
        [SetUp]
        public void Setup()
        {
            ai = assemblyBrowser.GetAssemblyInformation("LibForTesting.dll", AssemblyBrowserFlags.OnlyDeclaredMembers);
        }

        [Test]
        public void Namespaces_NameAndCount()
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
        public void Types_NameAndCount()
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
        public void Fields_NameAndCount()
        {
            Namespace? n1 = ai.Namespaces.Find(o => o.Name == "Namespace1");
            Namespace? n2 = ai.Namespaces.Find(o => o.Name == "Namespace2");

            Core.AssemblyClasses.Type? class1 = n1?.Types.Find(o => o.Name == "Class1");
            Core.AssemblyClasses.Type? staticClass = n1?.Types.Find(o => o.Name == "StaticClass");
            Core.AssemblyClasses.Type? class2 = n2?.Types[0];

            Assert.Multiple(() =>
            {
                Assert.That(class1, Is.Not.Null);
                Assert.That(staticClass, Is.Not.Null);
                Assert.That(class2, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Fields, Has.Count.EqualTo(2));
                Assert.That(staticClass?.Fields, Has.Count.EqualTo(0));
                Assert.That(class2?.Fields, Has.Count.EqualTo(1));
            });
            Assert.Multiple(() =>
            {
                Assert.That(class2?.Fields[0].Name, Is.EqualTo("Field1"));
                Assert.That(class2?.Fields[0].Type, Is.EqualTo(typeof(string).Name));
                Assert.That(class2?.Fields[0].AccessModifier, Is.EqualTo(AccessModifier.Public));
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Fields[0].Name, Is.EqualTo("Field1"));
                Assert.That(class1?.Fields[0].Type, Is.EqualTo(typeof(string).Name));
                Assert.That(class1?.Fields[0].AccessModifier, Is.EqualTo(AccessModifier.Private));
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Fields[1].Name, Is.EqualTo("Field2"));
                Assert.That(class1?.Fields[1].Type, Is.EqualTo(typeof(int).Name));
                Assert.That(class1?.Fields[1].AccessModifier, Is.EqualTo(AccessModifier.Public));
            });
        }
        [Test]
        public void Properties_NameAndCount()
        {
            Namespace? n1 = ai.Namespaces.Find(o => o.Name == "Namespace1");
            Namespace? n2 = ai.Namespaces.Find(o => o.Name == "Namespace2");

            Core.AssemblyClasses.Type? class1 = n1?.Types.Find(o => o.Name == "Class1");
            Core.AssemblyClasses.Type? staticClass = n1?.Types.Find(o => o.Name == "StaticClass");
            Core.AssemblyClasses.Type? class2 = n2?.Types[0];

            Assert.Multiple(() =>
            {
                Assert.That(class1, Is.Not.Null);
                Assert.That(staticClass, Is.Not.Null);
                Assert.That(class2, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Properties, Has.Count.EqualTo(2));
                Assert.That(staticClass?.Properties, Has.Count.EqualTo(0));
                Assert.That(class2?.Properties, Has.Count.EqualTo(1));
            });
            Assert.Multiple(() =>
            {
                Assert.That(class2?.Properties[0].Name, Is.EqualTo("Property1"));
                Assert.That(class2?.Properties[0].Type, Is.EqualTo(typeof(int).Name));
                Assert.That(class2?.Properties[0].AccessModifier, Is.EqualTo(AccessModifier.Public));
                Assert.That(class2?.Properties[0].GetterModifier, Is.EqualTo(AccessModifier.Private));
                Assert.That(class2?.Properties[0].SetterModifier, Is.EqualTo(AccessModifier.Public));
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Properties[0].Name, Is.EqualTo("Property1"));
                Assert.That(class1?.Properties[0].Type, Is.EqualTo(typeof(double).Name));
                Assert.That(class1?.Properties[0].AccessModifier, Is.EqualTo(AccessModifier.Internal));
                Assert.That(class1?.Properties[0].GetterModifier, Is.EqualTo(AccessModifier.Internal));
                Assert.That(class1?.Properties[0].SetterModifier, Is.EqualTo(AccessModifier.Private));
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Properties[1].Name, Is.EqualTo("Property2"));
                Assert.That(class1?.Properties[1].Type, Is.EqualTo("Class1"));
                Assert.That(class1?.Properties[1].AccessModifier, Is.EqualTo(AccessModifier.Protected));
                Assert.That(class1?.Properties[1].GetterModifier, Is.EqualTo(AccessModifier.Private));
                Assert.That(class1?.Properties[1].SetterModifier, Is.EqualTo(AccessModifier.Protected));
            });
        }

        [Test]
        public void Methods_NameAndCount()
        {
            Namespace? n1 = ai.Namespaces.Find(o => o.Name == "Namespace1");

            Core.AssemblyClasses.Type? class1 = n1?.Types.Find(o => o.Name == "Class1");

            Assert.Multiple(() =>
            {
                Assert.That(class1, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Fields, Has.Count.EqualTo(2));
            });
            Assert.That(class1?.Methods, Has.Count.EqualTo(4));
            Method? method = class1?.Methods.Find(o => o.Name == "Method1");
            Assert.That(method, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(method.IsExtensionMethod, Is.EqualTo(false));
                Assert.That(method.ReturnType, Is.EqualTo(typeof(void).Name));
                Assert.That(method.AccessModifier, Is.EqualTo(AccessModifier.Public));
                Assert.That(method.Parameters, Has.Length.EqualTo(2));
                Assert.That(method.Parameters[0].Name, Is.EqualTo("param1"));
                Assert.That(method.Parameters[0].Type, Is.EqualTo(typeof(string).Name));
                Assert.That(method.Parameters[1].Name, Is.EqualTo("param2"));
                Assert.That(method.Parameters[1].Type, Is.EqualTo(typeof(int).Name));
            });
        }

        [Test]
        public void ExtensionMethod()
        {
            Namespace? n1 = ai.Namespaces.Find(o => o.Name == "Namespace1");

            Core.AssemblyClasses.Type? class1 = n1?.Types.Find(o => o.Name == "Class1");

            Assert.Multiple(() =>
            {
                Assert.That(class1, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(class1?.Fields, Has.Count.EqualTo(2));
            });
            Assert.That(class1?.Methods, Has.Count.EqualTo(4));
            Method? method = class1?.Methods.Find(o => o.Name == "GetTen");
            Assert.That(method, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(method.IsExtensionMethod, Is.EqualTo(true));
                Assert.That(method.ReturnType, Is.EqualTo(typeof(int).Name));
                Assert.That(method.AccessModifier, Is.EqualTo(AccessModifier.Public));
                Assert.That(method.Parameters, Has.Length.EqualTo(0));
            });
        }
    }
}