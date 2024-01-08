using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace WpfTestApp
{
    public class MainVm : ViewModelBase
    {
        private static readonly Lazy<MainVm> LazyVm = new Lazy<MainVm>(() => new MainVm());
        public static MainVm Instance => LazyVm.Value;

        public MainVm()
        {

        }

        private double _cardWidth = 240d;
        public double CardWidth
        {
            get => _cardWidth;
            set => Set("CardWidth", ref _cardWidth, value);
        }

        private double _cardHeight = 426d;
        public double CardHeight
        {
            get => _cardHeight;
            set => Set("CardHeight", ref _cardHeight, value);
        }
    }
}
