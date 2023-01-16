using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace wpfctrl
{
    public class TextItem : ViewModelBase
    {
        private string _text1 = "";
        public string Text1
        {
            get => _text1;
            set => Set("Text1", ref _text1, value);
        }

        private string _text2 = "";
        public string Text2
        {
            get => _text2;
            set => Set("Text2", ref _text2, value);
        }
    }

    public class MainVm : ViewModelBase
    {
        private static readonly Lazy<MainVm> Lazy = new Lazy<MainVm>(() => new MainVm());
        public static MainVm Instance => Lazy.Value;

        public MainVm()
        {
            for (int i = 0; i < 5; i++)
            {
                var info = new TextItem();
                info.Text1 = $"机器人{i}号";
                info.Text2 = $"序列号{i}号";
                ItemsInfo.Add(info);
            }
        }

        private ObservableCollection<TextItem> _itemsInfo = new ObservableCollection<TextItem>();
        public ObservableCollection<TextItem> ItemsInfo
        {
            get => _itemsInfo;
            set => Set("ItemsInfo", ref _itemsInfo, value);
        }

        public void InsertItemInfo()
        {
            var tempItem = new ObservableCollection<TextItem>();
            var info = new TextItem();
            info.Text1 = $"机器人N号";
            info.Text2 = $"序列号N号";
            tempItem.Add(info);
            ItemsInfo = tempItem;
        }
    }
}
