namespace Namespace1
{
    public static class StaticClass
    {
        public static int GetTen(this Class1 c)
        {            
            return c.Field2*10;
        }
    }
    public class Class1
    {
        private string Field1;
        public int Field2;
        internal double Property1 { get; private set; }
        protected Class1 Property2 { private get; set; }

        public void Method1(string param1, int param2) { }
        public int Method2(string param3, int param4)
        {
            int a = this.GetTen(); 
            return a; 
        }
        public string Method3(string param5, int param6) { return ""; }
    }
}
namespace Namespace2
{
    public class Class2
    {
        public string Field1;
        public int Property1 { private get; set; }

        public void Method1(string param1, int param2) { }
        public int Method2(string param3, int param4) { return 0; }
        public string Method3(string param5, int param6) { return ""; }
    }
}