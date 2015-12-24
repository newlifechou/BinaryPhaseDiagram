using BinaryPhaseDiagramOperationLib;
using GalaSoft.MvvmLight.Messaging;
using form = System.Windows.Forms;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            Messenger.Default.Register<object>(this, "SearchFinished", obj =>
             {
                 SelectTheFirstListBoxItem();
                 HideGuideLine();
             });
            Messenger.Default.Register<BPDDataItem>(this, "OutputImageToken",OutputImageMethod);


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

        private void dcDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((bool)chkGuideLine.IsChecked)
            {
                Point mousePosition = e.GetPosition(dcDrawingArea);

                lineHorizon.X1 = 0;
                lineHorizon.Y1 = mousePosition.Y;
                lineHorizon.X2 = dcDrawingArea.ActualWidth;
                lineHorizon.Y2 = mousePosition.Y;

                lineVertical.X1 = mousePosition.X;
                lineVertical.Y1 = 0;
                lineVertical.X2 = mousePosition.X;
                lineVertical.Y2 = dcDrawingArea.ActualHeight;
            }
            else
            {
                HideGuideLine();
            }
            e.Handled = true;
        }

        private void dcDrawingArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HideGuideLine();
            e.Handled = true;
        }

        private void HideGuideLine()
        {
            lineHorizon.X1 = 0;
            lineHorizon.Y1 = 0;
            lineHorizon.X2 = 0;
            lineHorizon.Y2 = 1;

            lineVertical.X1 = 0;
            lineVertical.Y1 = 0;
            lineVertical.X2 = 0;
            lineVertical.Y2 = 1;
        }

        private void lstBPD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideGuideLine();
        }

        private void OutputImageMethod(BPDDataItem item)
        {
            try
            {
                form.SaveFileDialog save = new form.SaveFileDialog();
                save.Title = "Output";
                save.FileName = item.BPDName;
                save.RestoreDirectory = true;
                save.Filter = "Image(*.jpg)|*.jpg";
                save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (save.ShowDialog() ==form.DialogResult.OK)
                {
                    string sourceFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", item.BPDName);
                    string targetFile = save.FileName;
                    File.Copy(sourceFile, targetFile);
                }
            }
            catch (Exception ex)
            {
               form.MessageBox.Show(ex.Message, "Exception Occurs",form. MessageBoxButtons.OK);
            }
        }
    }
}
