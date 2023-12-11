using System.Collections.Generic;
using System;
using System.Windows.Controls;

namespace WpfTestApp
{

    public class Student
    {
        public string Name { get; set; }
        public int Result { get; set; }
    }

    /// <summary>
    /// UserControl3.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();

            List<Student> stuList = new List<Student>();
            stuList.Add(new Student() { Name = "A", Result = 40 });
            stuList.Add(new Student() { Name = "B", Result = 50 });
            stuList.Add(new Student() { Name = "C", Result = 60 });
            stuList.Add(new Student() { Name = "D", Result = 70 });
            stuList.Add(new Student() { Name = "E", Result = 80 });

            list.DataContext = new { Model = stuList };
        }
    }
}
