using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using GalaSoft.MvvmLight;

namespace LoadFileXaml
{
    public class TestVm : ViewModelBase
    {
        private static readonly Lazy<TestVm> Lazy = new Lazy<TestVm>(() => new TestVm());
        public static TestVm instance => Lazy.Value;

        public TestVm()
        {

        }

        private string _name = "虎牙直播";

        public string name
        {
            get => _name;
            set => Set("name", ref _name, value);
        }

        private string _url = "https://www.huya.com/";

        public string url
        {
            get => _url;
            set => Set("url", ref _url, value);
        }

        private DelegateCommand _testCmd;
        public DelegateCommand TestCmd
        {
            get
            {
                return _testCmd ??= new DelegateCommand((obj) =>
                {
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = @"https://www.huya.com/",
                        UseShellExecute = true,
                    };
                    System.Diagnostics.Process.Start(psi);
                });
            }
        }
    }
}
