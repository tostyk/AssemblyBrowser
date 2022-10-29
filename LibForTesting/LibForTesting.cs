using Namespace1;

namespace Namespace1
{
    public class Class1
    {
        public string Field1;
        public int Field2;
        public double Property1 { get; set; }
        public Class1 Property2 { get; set; }
        public Class1() { }
        public Class1(string field1, int field2) { }
        public Class1(string field1, double property1) { }

        public void Method1(string param1, int param2) { }
        public int Method2(string param3, int param4) { return 0; }
        public string Method3(string param5, int param6) { return ""; }
    }
}
namespace Namespace2
{
    public class Class1 { }
    public class Class2
    {
        public string Field1;
        public Class2 Property1 { get; set; }
        public Class2() { }
        public Class2(string field1, int field2) { }
        public Class2(string field1, double property1) { }

        public void Method1(string param1, int param2) { }
        public int Method2(string param3, int param4) { return 0; }
        public string Method3(string param5, int param6) { return ""; }
    }
}