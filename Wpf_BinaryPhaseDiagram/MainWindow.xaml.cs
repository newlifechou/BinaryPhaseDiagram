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
            SetLanguage();

            SelectTheFirstListBoxItem();

            //注册信使
            Messenger.Default.Register<object>(this, "SearchFinished", obj =>
             {
                 SelectTheFirstListBoxItem();
                 HideGuideLine();
             });


            //注销所有信使
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
            //HideGuideLine();
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

        /// <summary>
        /// 选择项目改变的时候，隐藏参考线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstBPD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideGuideLine();
            //这里不能用e.handle=true，否则冒泡事件无法传播到Window上
        }

        /// <summary>
        /// 更换语言资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string languageType = (cboLanguage.SelectedItem as ComboBoxItem).Content.ToString();

            if (string.IsNullOrEmpty(cboLanguage.Text))
            {
                return;
            }

            try
            {
                ChangeLanguageResource(languageType);
                Properties.Settings.Default.Language = languageType;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            e.Handled = true;
        }

        private static void ChangeLanguageResource(string languageType)
        {
            string languagePath = "Resources/UI" + languageType + ".xaml";
            Application.Current.Resources.MergedDictionaries[0] =
                new ResourceDictionary() { Source = new Uri(languagePath, UriKind.Relative) };
        }

        private void SetLanguage()
        {
            try
            {
                string languageType = Properties.Settings.Default.Language;
                cboLanguage.Text = languageType;
                //ChangeLanguageResource(languageType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
