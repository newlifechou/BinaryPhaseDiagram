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
            Messenger.Default.Register<object>(this,"SearchFinished", obj =>
            {
                if (lstBPD.Items.Count > 0)
                {
                    lstBPD.SelectedIndex = 0;
                }
            });

            this.Unloaded += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
    }
}
