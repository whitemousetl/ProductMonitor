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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductMonitor.UserControls
{
    /// <summary>
    /// WorkShopDetailUC.xaml 的交互逻辑
    /// </summary>
    public partial class WorkShopDetailUC : UserControl
    {
        public WorkShopDetailUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JumpToDetail_Click(object sender, RoutedEventArgs e)
        {
            detail.Visibility = Visibility.Visible;

            //实现渐变动画
            //位移
            ThicknessAnimation thicknessAnimation = new(new Thickness(0,50,0,-50),new Thickness(0,0,0,0),new TimeSpan(0,0,0,0,400));

            //透明度
            DoubleAnimation doubleAnimation = new(0,1,new TimeSpan(0, 0, 0, 0, 400));

            Storyboard.SetTarget(thicknessAnimation, detailContent);
            Storyboard.SetTarget(doubleAnimation, detailContent);

            Storyboard.SetTargetProperty(thicknessAnimation,new PropertyPath(nameof(Margin)));
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(nameof(Opacity)));

            Storyboard storyboard = new();
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Children.Add(doubleAnimation);

            storyboard.Begin();


        }

        /// <summary>
        /// 关闭详情页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseDetail_Click(object sender, RoutedEventArgs e)
        {
            //实现渐变动画
            //位移
            ThicknessAnimation thicknessAnimation = new(new Thickness(0, 0, 0, 0), new Thickness(0, 50, 0, -50),  new TimeSpan(0, 0, 0, 0, 400));

            //透明度
            DoubleAnimation doubleAnimation = new(1,0, new TimeSpan(0, 0, 0, 0, 400));

            Storyboard.SetTarget(thicknessAnimation, detailContent);
            Storyboard.SetTarget(doubleAnimation, detailContent);

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(nameof(Margin)));
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(nameof(Opacity)));

            Storyboard storyboard = new();
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Children.Add(doubleAnimation);

            // Create a TaskCompletionSource to await the completion of the storyboard
            var tcs = new TaskCompletionSource<object>();

            // Set up the Completed event handler
            storyboard.Completed += (se, ev) =>
            {
                // Set the TaskCompletionSource result when the animation is completed
                tcs.SetResult(new());
            };

            // Start the storyboard
            storyboard.Begin();

            // Await the TaskCompletionSource.Task
            var task = tcs.Task;

            // Continue executing after the animation completes
            task.ContinueWith((t) =>
            {
                // Set visibility after animation completes
                detail.Visibility = Visibility.Collapsed;
            }, TaskScheduler.FromCurrentSynchronizationContext()); // Ensure continuation runs on UI thread

        }
    }
}
