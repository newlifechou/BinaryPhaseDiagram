using GalaSoft.MvvmLight;
//ע��������Ҫ����CommandWpf������mvvmlight��WPF��Ŀ���е�һ��Ҫ��
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
        //��������
        //��ǰ��ͼƬ
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

        //���������������ͼ��������
        public ObservableCollection<BinaryPhaseDataItem> BinaryPhaseList { get; set; }

        //�����õ�����Ԫ��
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

        //����������
        public RelayCommand SearchResultCommand { get; set; }

    }
}