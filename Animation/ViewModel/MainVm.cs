using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace Animation.ViewModel
{
    //
    public class CardInfo : ViewModelBase
    {
        private string _cardName = "";

        public string CardName
        {
            get => _cardName;
            set => Set("CardName", ref _cardName, value);
        }

        private string _cardDesc = "";

        public string CardDesc
        {
            get => _cardDesc;
            set => Set("CardDesc", ref _cardDesc, value);

        }

        private bool _isShow = true;
        public bool IsShow
        {
            get => _isShow;
            set => Set("IsShow", ref _isShow, value);
        }
    }


    public class MainVm : ViewModelBase
    {
        private static readonly Lazy<MainVm> Lazy = new Lazy<MainVm>(() => new MainVm());
        public static MainVm Instance => Lazy.Value;

        public MainVm()
        {
            Init();
        }

        private ObservableCollection<CardInfo> _cardInfos = new ObservableCollection<CardInfo>();
        public ObservableCollection<CardInfo> CardInfos
        {
            get => _cardInfos;
            set => Set("CardInfos", ref _cardInfos, value);
        }

        private void Init()
        {
            var temp = new ObservableCollection<CardInfo>();
            for (int i = 0; i < 50; i++)
            {
                var info = new CardInfo();
                info.CardName = $"机器人{i}号";
                info.CardDesc = $"序列号{i}号";
                temp.Add(info);
            }

            CardInfos = temp;
        }

        public void BtnClick()
        {
            for (int i = 0; i < CardInfos.Count; i++)
            {
                if (i % 5 == 0)
                {
                    var info = CardInfos[i];
                    info.IsShow = false;
                }
            }
        }

    }
}
