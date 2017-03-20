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
using BinaryPhaseDiagramTwo.Views;



namespace BinaryPhaseDiagramTwo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchView _display;
        private ResultView _search;
        public MainWindow()
        {
            InitializeComponent();
            InitializeVariables();

        }

        private void InitializeVariables()
        {
            _display = new SearchView();
            _search = new ResultView();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigateTo(_display);
        }
        private void NavigateTo(UserControl view)
        {
            if (view != null)
            {
                main.Content = view;
            }
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

        private void btnMaximum_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState==WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
    }
}
