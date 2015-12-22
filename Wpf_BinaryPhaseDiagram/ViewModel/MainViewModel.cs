using GalaSoft.MvvmLight;
//注意这里需要引用CommandWpf，这是mvvmlight在WPF项目当中的一个要求
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using Wpf_BinaryPhaseDiagram.Model;

namespace Wpf_BinaryPhaseDiagram.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            CurrentImage = "Ac-B.jpg";
        }
        //公开属性
        //当前的图片
        private string currentImage;
        public string CurrentImage
        {
            get { return currentImage; }
            set
            {
                currentImage = value;
                RaisePropertyChanged(() => CurrentImage);
            }
        }

        //存放搜索出来的相图数据项结果
        public ObservableCollection<BinaryPhaseDataItem> BinaryPhaseList { get; set; }

        //搜索用的两个元素
        private string elementA;
        public string ElementA
        {
            get { return elementA; }
            set
            {
                elementA = value;
                RaisePropertyChanged(() => ElementA);
            }
        }
        private string elementB;
        public string ElementB
        {
            get { return elementB; }
            set
            {
                elementB = value;
                RaisePropertyChanged(() => ElementB);
            }
        }

        //公开的命令
        public RelayCommand SearchResultCommand { get; set; }

    }
}