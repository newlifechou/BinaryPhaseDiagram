using GalaSoft.MvvmLight;
//注意这里需要引用CommandWpf，这是mvvmlight在WPF项目当中的一个要求
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using BinaryPhaseDiagramOperationLib;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

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
            bpdOp = new BPDOperation("Images");
            BPDList = new ObservableCollection<BPDDataItem>(bpdOp.GetAllData());
            ResultCount = BPDList.Count;

            SearchResultCommand = new RelayCommand(() =>
              {
                  BPDList = new ObservableCollection<BPDDataItem>(bpdOp.GetData(ElementA, ElementB));
                  ResultCount = BPDList.Count;
                  Messenger.Default.Send<object>(null, "SearchFinished");
              }, () =>
              {
                  if (string.IsNullOrEmpty(ElementA) && string.IsNullOrEmpty(ElementB))
                  {
                      return false;
                  }
                  return true;
              });

            ShowAllCommand = new RelayCommand(() =>
              {
                  BPDList = new ObservableCollection<BPDDataItem>(bpdOp.GetAllData());
                  ResultCount = BPDList.Count;
                  Messenger.Default.Send<object>(null, "SearchFinished");
              });

            OutputCommand = new RelayCommand<BPDDataItem>(item =>
              {
                  try
                  {
                      bpdOp.SaveAImageSomeWhere(item);
                  }
                  catch (System.Exception ex)
                  {
                      MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                  }

              }, item =>
               {
                   if (item != null)
                   {
                       return true;
                   }
                   return false;
               });
        }
        private BPDOperation bpdOp;

        //公开属性
        //当前的图片
        private string currentImage;
        public string CurrentImage
        {
            get { return currentImage; }
            set
            { currentImage = value; RaisePropertyChanged(() => CurrentImage); }
        }

        //存放搜索出来的相图数据项结果
        private ObservableCollection<BPDDataItem> bpdList;
        public ObservableCollection<BPDDataItem> BPDList
        {
            get
            {
                return bpdList;
            }
            set
            { bpdList = value; RaisePropertyChanged(() => BPDList); }
        }

        //搜索用的两个元素
        private string elementA;
        public string ElementA
        {
            get { return elementA; }
            set
            { elementA = value; RaisePropertyChanged(() => ElementA); }
        }
        private string elementB;
        public string ElementB
        {
            get { return elementB; }
            set
            { elementB = value; RaisePropertyChanged(() => ElementB); }
        }
        private int resultCount;

        public int ResultCount
        {
            get { return resultCount; }
            set { resultCount = value; RaisePropertyChanged(() => ResultCount); }
        }



        //公开的命令
        public RelayCommand SearchResultCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }
        public RelayCommand<BPDDataItem> OutputCommand { get; set; }
    }
}