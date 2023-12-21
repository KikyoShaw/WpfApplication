using GalaSoft.MvvmLight;
using System;

namespace WpfAppNet
{
    public class MainVm : ViewModelBase
    {
        private static readonly Lazy<MainVm> LazyVm = new Lazy<MainVm>(() => new MainVm());
        public static MainVm Instance => LazyVm.Value;

        public MainVm()
        {
        }

        private bool _bMouseHover = false;

        public bool bMouseHover
        {
            get => _bMouseHover;
            set => Set("bMouseHover", ref _bMouseHover, value);
        }
    }
}
