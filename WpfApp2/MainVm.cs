using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

 namespace WpfApp2
{
    public class UIPropItem : ViewModelBase
    {
        public string sIconUrl { get; set; }
    }


    public class MainVm : ViewModelBase
    {
        private static readonly Lazy<MainVm> LazyVm = new Lazy<MainVm>(() => new MainVm());
        public static MainVm Instance => LazyVm.Value;

        public MainVm()
        {
            for (int i = 0; i < 12; i++)
            {
                var t = @$"/WpfApp2;Component/Resources/{i+1}.jpg";
                var item = new UIPropItem();
                item.sIconUrl = t;
                vProps.Add(item);
                vProps.Add(item);
                vProps.Add(item);
            }
        }

        private bool _firstRasise = false;
        private ObservableCollection<UIPropItem> _vProps = new ObservableCollection<UIPropItem>();
        public ObservableCollection<UIPropItem> vProps
        {
            get => _vProps;
            set
            {
                if (Set("vProps", ref _vProps, value))
                {
                    RaisePropertyChanged(nameof(vTenProps));
                }
            }
        }

        public ObservableCollection<UIPropItem> vTenProps
        {
            get
            {
                var ret = vProps.Take(5);
                return new ObservableCollection<UIPropItem>(ret);
            }
        }

        public void TestProp()
        {
            vProps.Clear();

            for (int i = 0; i < 5; i++)
            {
                var t = @$"/WpfApp2;Component/Resources/{i + 1}.jpg";
                var item = new UIPropItem();
                item.sIconUrl = t;
                vProps.Add(item);
                vProps.Add(item);
                vProps.Add(item);
            }
            var p = vTenProps;
        }

        private bool _bMouseHover = false;

        public bool bMouseHover
        {
            get => _bMouseHover;
            set => Set("bMouseHover", ref _bMouseHover, value);
        }

        private int _iCount = 254345346;

        public int iCount
        {
            get => _iCount;
            set => Set("iCount", ref _iCount, value);
        }
    }
}
