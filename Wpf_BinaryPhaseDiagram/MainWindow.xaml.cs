using GalaSoft.MvvmLight.Messaging;
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

namespace Wpf_BinaryPhaseDiagram
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SelectTheFirstListBoxItem();

            Messenger.Default.Register<object>(this,"SearchFinished", obj =>
            {
                SelectTheFirstListBoxItem();
            });

            this.Unloaded += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
        private void SelectTheFirstListBoxItem()
        {
            if (lstBPD.Items.Count > 0)
            {
                lstBPD.SelectedIndex = 0;
            }
        }
    }
}
