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
using System.Windows.Shapes;

namespace ProductMonitor.Views
{
    /// <summary>
    /// SettingsWin.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWin : Window
    {
        public SettingsWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 导航到设备清单页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            ////程序集（授权）路径
            //frame.Navigate(new Uri("pack://application:,,,/ProductMonitor;component/Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute));

            string? tag = "";
            var btn = sender as RadioButton;
            if (btn != null )
            {
                if(btn.Tag != null)
                    tag = btn.Tag.ToString();
            }

            //程序集（授权）路径 如果在同一级目录
            //# 片段 定位到某个片段
            frame.Navigate(new Uri("pack://application:,,,/Views/SettingsPage.xaml#" + tag, UriKind.RelativeOrAbsolute));
        }
    }
}
