using System;
using System.Collections.ObjectModel;
using _2024_9_4_PictureScaleTransform.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace _2024_9_4_PictureScaleTransform.ViewModel
{
    public class MainViewModel: ObservableObject
    {
        private static readonly Lazy<MainViewModel> _sLazy = new Lazy<MainViewModel>(() => new MainViewModel());
        public static MainViewModel Instance => _sLazy.Value;


        private ObservableCollection<CardItem> _cardList = new ObservableCollection<CardItem>();

        public ObservableCollection<CardItem> CardList
        {
            get => _cardList;
            set => SetProperty(ref _cardList, value);
        }

        private double _cardWidth = 191d;
        public double CardWidth
        {
            get => _cardWidth;
            set => SetProperty(ref _cardWidth, value);
        }

        private double _cardHeight = 321d;
        public double CardHeight
        {
            get => _cardHeight;
            set => SetProperty(ref _cardHeight, value);
        }

        private MainViewModel()
        {
            Init();
        }

        private void Init()
        {
            _cardList.Add(new CardItem()
            {
                AnchorStatus = Status.Living,
                Nick = "电玩小丸子",
                Section = "超凡大师",
                Favorable = 98,
                AccompanyingPrice = 0,
                Recipients = 122,
                Title = "王者带飞导师"
            });

            _cardList.Add(new CardItem()
            {
                AnchorStatus = Status.Online,
                Nick = "张嘉文",
                Section = "荣耀王者",
                Favorable = 92,
                AccompanyingPrice = 4,
                Recipients = 17,
                Title = "大神"
            });

            _cardList.Add(new CardItem()
            {
                AnchorStatus = Status.Online,
                Nick = "天元萌",
                Section = "超凡大师",
                Favorable = 100,
                AccompanyingPrice = 9,
                Recipients = 134,
                Title = "王者带飞导师"
            });

            _cardList.Add(new CardItem()
            {
                AnchorStatus = Status.Living,
                Nick = "追梦小丸子",
                Section = "超凡大师",
                Favorable = 89,
                AccompanyingPrice = 2,
                Recipients = 14,
                Title = "王者带飞导师"
            });

            for (int i = 0; i < 10; i++)
            {
                _cardList.Add(new CardItem()
                {
                    AnchorStatus = Status.Living,
                    Nick = "Uzi",
                    Section = "超凡大师",
                    Favorable = 89,
                    AccompanyingPrice = 2,
                    Recipients = 14,
                    Title = "王者带飞导师"
                });
            }
        }

        public void InitGameSize()
        {
            foreach (var card in CardList)
            {
                card.ItemHeight = CardHeight - 12;
                card.ItemWidth = CardWidth - 16;
            }
        }

    }


    public class CardItem : ObservableObject
    {
        private double _itemWidth = 191d;
        public double ItemWidth
        {
            get => _itemWidth;
            set => SetProperty(ref _itemWidth, value);
        }

        private double _itemHeight = 321d;
        public double ItemHeight
        {
            get => _itemHeight;
            set => SetProperty(ref _itemHeight, value);
        }

        private Status _anchorStatus = Status.Living;

        public Status AnchorStatus
        {
            get => _anchorStatus;
            set => SetProperty(ref _anchorStatus, value);
        }

        /// <summary>
        /// 主播Nick
        /// </summary>
        private string _nick = string.Empty;

        public string Nick
        {
            get => _nick;
            set => SetProperty(ref _nick, value);
        }
        
        /// <summary>
        /// 主播Nick
        /// </summary>
        private string _section = string.Empty;

        public string Section
        {
            get => _section;
            set => SetProperty(ref _section, value);
        }

        private int _recipients = 0;

        /// <summary>
        /// 接单数
        /// </summary>
        public int Recipients
        {
            get => _recipients;
            set => SetProperty(ref _recipients, value);
        }


        /// <summary>
        /// 好评率
        /// </summary>
        private int _favorable = 0;

        public int Favorable
        {
            get => _favorable;
            set => SetProperty(ref _favorable, value);
        }

        /// <summary>
        /// title
        /// </summary>
        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        /// <summary>
        /// 陪玩价格
        /// </summary>
        private int _accompanyingPrice = 0;

        public int AccompanyingPrice
        {
            get => _accompanyingPrice;
            set => SetProperty(ref _accompanyingPrice, value);
        }



        private bool _isHover =false;

        public bool IsHover
        {
            get => _isHover;
            set => SetProperty(ref _isHover, value);
        }

    }
}
