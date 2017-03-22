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

namespace BinaryPhaseDiagramTwo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BPDOperation operate;
        public MainWindow()
        {
            InitializeComponent();

            InitializeVariables();
            GetAllBPDs();
        }

        private void InitializeVariables()
        {
            string dir = "Images";
            operate = new BPDOperation(dir);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimum_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var elementA = txtElementA.Text.Trim();
            var elementB = txtElementB.Text.Trim();
            try
            {
                var result = operate.GetData(elementA, elementB);
                mainDg.ItemsSource = result;

                ClearGuideLines();
                SetStatusBar(result.Count);
                mainDg.SelectedIndex = 0;
                gridSearchPanel.Height = open;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {

            GetAllBPDs();
        }

        private double collapse = 0;
        private double open = 600;

        private void GetAllBPDs()
        {
            try
            {
                var result = operate.GetAllData();
                mainDg.ItemsSource = result;

                ClearGuideLines();
                SetStatusBar(result.Count);
                mainDg.SelectedIndex = 0;
                gridSearchPanel.Height = open;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetStatusBar(int count = 0)
        {
            txtStatus.Text = $"共{count}个检索结果";
        }

        private void btnCollapse_Click(object sender, RoutedEventArgs e)
        {
            if (gridSearchPanel.Height == collapse)
            {
                gridSearchPanel.Height = open;
            }
            else
            {
                gridSearchPanel.Height = collapse;
            }
        }

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)chkGuidLine.IsChecked)
            {
                Point mousePosition = e.GetPosition(mainCanvas);

                lineHorizontal.X1 = 0;
                lineHorizontal.Y1 = mousePosition.Y;
                lineHorizontal.X2 = mainCanvas.ActualWidth;
                lineHorizontal.Y2 = mousePosition.Y;

                lineVertical.X1 = mousePosition.X;
                lineVertical.Y1 = 0;
                lineVertical.X2 = mousePosition.X;
                lineVertical.Y2 = mainCanvas.ActualHeight;
            }
            else
            {
                ClearGuideLines();
            }
            e.Handled = true;
        }

        private void ClearGuideLines()
        {
            lineHorizontal.X1 = 0;
            lineHorizontal.Y1 = 0;
            lineHorizontal.X2 = 0;
            lineHorizontal.Y2 = 1;

            lineVertical.X1 = 0;
            lineVertical.Y1 = 0;
            lineVertical.X2 = 0;
            lineVertical.Y2 = 1;
        }

        private void txtElementA_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtElementA.Text) && string.IsNullOrEmpty(txtElementB.Text))
            {
                btnSearch.IsEnabled = false;
            }
            else
            {
                btnSearch.IsEnabled = true;
            }

        }

        private void mainDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearGuideLines();
        }

        private void btnMaximum_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState==WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
            ClearGuideLines();
        }

        private void btnOutPut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var image = (mainDg.SelectedItem as BPDDataItem).BPDName;
                var imagePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", image);
                var copyPath=System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),image);
                if (!System.IO.File.Exists(copyPath))
                {
                    System.IO.File.Copy(imagePath, copyPath);
                    string message = "导出当前相图图片到桌面完毕";
                    txtStatus.Text = message;
                    MessageBox.Show(message, "导出", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    txtStatus.Text = "同名文件已经在桌面上";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
