using System;

namespace WpfTestApp.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class MyClassAttribute : System.Attribute
    {
        public string Name { get; set; }
        public int Num { get; set; }

        public MyClassAttribute(string name, int num)
        {
            Name = name;
            Num = num;
            Test();
        }

        private void Test()
        {
            System.Diagnostics.Trace.WriteLine($"MyClassAttribute OutPut: Name:{Name}, Num:{Num}");
        }

    }
}
