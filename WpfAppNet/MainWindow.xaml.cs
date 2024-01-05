using System.Collections.Generic;
using System;
using System.Windows;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace WpfAppNet
{
    public class Fex
    {
        public bool F { get; set; }
    }

    public class Person
    {
        public string Name { get; set; } = "";
        public string Motto { get; set; } = "";
    }
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = MainVm.Instance;
            InitializeComponent();

            //var str = "1.2.1";
            //var version = Version.Parse(str);

            //TestException();

            TestJsonAndXml();
        }

        private void TestList()
        {
            var a = new List<Fex>()
            {
                new Fex() {F = true,},
                new Fex() {F = true,},
                new Fex() {F = false,},

            };
            List<Fex> b = a.ToList();
            b.RemoveAt(0);
            System.Diagnostics.Trace.WriteLine($"a.Count:{a.Count}, b.Count:{b.Count}");
            b.Add(new Fex());
            b.Add(new Fex());
            System.Diagnostics.Trace.WriteLine($"a.Count:{a.Count}, b.Count:{b.Count}");
        }

        private void TestException()
        {
            try
            {
                checked
                {
                    //long tabathaDuchamp = int.MaxValue;
                    //tabathaDuchamp *= 2;
                    //int ttt = (int)tabathaDuchamp; // -2 默认是 unchecked
                    double hcmzgSsby = float.MaxValue;
                    hcmzgSsby *= 2;
                    float ttt = (float)hcmzgSsby;

                    System.Diagnostics.Trace.WriteLine($"ttt:{ttt}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void TestJsonAndXml()
        {
            var person = new Person()
            {
                Name = "shaw",
                Motto = "天生我才必有用",
            };

            var xmlSerializer = new XmlSerializer(typeof(Person));
            var str = new StringBuilder();

            xmlSerializer.Serialize(new StringWriter(str), person);

            var xml = str.ToString();
            Console.WriteLine(xml);

            XmlDocument? doc = new XmlDocument();
            doc.LoadXml(xml);

            string text = JsonConvert.SerializeXmlNode(doc);
            System.Diagnostics.Trace.WriteLine("转换json");
            System.Diagnostics.Trace.WriteLine(text);

            doc = JsonConvert.DeserializeXmlNode(text);
            System.Diagnostics.Trace.WriteLine("json转xml");
            System.Diagnostics.Trace.WriteLine(doc.InnerXml);

        }
    }
}
