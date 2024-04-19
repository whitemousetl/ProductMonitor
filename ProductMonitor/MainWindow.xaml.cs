using ProductMonitor.OpCommand;
using ProductMonitor.UserControls;
using ProductMonitor.ViewModels;
using ProductMonitor.Views;
using System.ComponentModel;
using System.Text;
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

namespace ProductMonitor;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// 视图模型
    /// </summary>
    private readonly MainWindowVM _mainWindowVM = new();

    public MainWindow()
    {
        InitializeComponent();
        
        this.DataContext = _mainWindowVM;


        //设计WorkShopDetailUC用户控件需要
#if DEBUG
        //WorkShopDetailUC workShopDetailUC = new();
        //mainWindowVM.MonitorUC = workShopDetailUC; 
#endif

    }
    /// <summary>
    /// 显示车间详情页命令
    /// </summary>
    public ICommand ShowDetailCom => new OpBaseCommand(ShowDetailUC);

    /// <summary>
    /// 返回监控命令
    /// </summary>
    public ICommand GoBackCom => new OpBaseCommand(GoBack);

    /// <summary>
    /// 配置页命令
    /// </summary>
    public ICommand ShowSettingsWinCom => new OpBaseCommand(ShowSettingsWin);


    /// <summary>
    /// 显示车间详情页
    /// </summary>
    private void ShowDetailUC()
    {
        WorkShopDetailUC workShopDetailUC = new();

        _mainWindowVM.MonitorUC = workShopDetailUC;

        //动画效果（由下而上）
        //位移 移动时间
        ThicknessAnimation thicknessAnimation = new(new Thickness(0, 50, 0, -50), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));

        //透明度
        DoubleAnimation doubleAnimation = new(0, 1, new TimeSpan(0, 0, 0, 0, 400));

        Storyboard.SetTarget(thicknessAnimation, workShopDetailUC);
        Storyboard.SetTarget(doubleAnimation, workShopDetailUC);

        Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(nameof(Margin)));
        Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(nameof(Opacity)));

        Storyboard storyboard = new();
        storyboard.Children.Add(thicknessAnimation);
        storyboard.Children.Add(doubleAnimation);

        storyboard.Begin();
    }

    /// <summary>
    /// 返回监控页
    /// </summary>
    private void GoBack()
    {
        _mainWindowVM.MonitorUC = new MonitorUC();
    }

    /// <summary>
    /// 最小化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Min_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    /// <summary>
    /// 最大化或正常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MaxOrNormal_Click(object sender, RoutedEventArgs e)
    {
        if(this.WindowState != WindowState.Maximized) 
            this.WindowState = WindowState.Maximized;
        else
            this.WindowState = WindowState.Normal;
    }

    /// <summary>
    /// 关闭程序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Close_Click(object sender, RoutedEventArgs e)
    {
        //关闭当前窗口
        //this.Close();
        //强制退出
        Environment.Exit(0);
    }

    /// <summary>
    /// 弹出配置窗口
    /// </summary>
    private void ShowSettingsWin()
    {
        //父子关系
        SettingsWin settingsWin = new(){ Owner = this };
        settingsWin.ShowDialog();
    }


}