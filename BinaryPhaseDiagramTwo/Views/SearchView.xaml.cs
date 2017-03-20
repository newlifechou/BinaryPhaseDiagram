using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BinaryPhaseDiagramOperationLib;
using System.IO;



namespace BinaryPhaseDiagramTwo.Views
{
    /// <summary>
    /// DiagramDisplayView.xaml 的交互逻辑
    /// </summary>
    public partial class SearchView : UserControl
    {
        private BPDOperation operate;
        public SearchView()
        {
            InitializeComponent();
            string dir = "Images";
            operate = new BPDOperation(dir);

            GetAllBPDs();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var elementA = txtElementA.Text.Trim();
            var elementB = txtElementB.Text.Trim();
            try
            {
                mainDg.ItemsSource = operate.GetData(elementA, elementB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetAllBPDs()
        {
            try
            {
                mainDg.ItemsSource = operate.GetAllData();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            GetAllBPDs();
        }
    }
}
