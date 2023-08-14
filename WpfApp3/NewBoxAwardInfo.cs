using System.Dynamic;
using GalaSoft.MvvmLight;


namespace WpfApp3
{
    public class NewBoxAwardInfo : ViewModelBase
    {
        //宝箱奖励ICON
        private string _awardIcon = "";

        public string AwardIcon
        {
            get => _awardIcon;
            set => Set("AwardIcon", ref _awardIcon, value);
        }

        //宝箱奖励名称
        private string _awardText = "";

        public string AwardText
        {
            get => _awardText;
            set => Set("AwardText", ref _awardText, value);
        }
    }
}
