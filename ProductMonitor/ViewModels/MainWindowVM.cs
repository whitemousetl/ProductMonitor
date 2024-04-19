using CommunityToolkit.Mvvm.ComponentModel;
using ProductMonitor.Dto;
using ProductMonitor.Models;
using ProductMonitor.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductMonitor.ViewModels;
public partial class MainWindowVM : ObservableObject
{
    public MainWindowVM()
    {
        _monitorUC = new MonitorUC();

        _environmentList = default!;

        _environmentListDto = default!;

        _alarmList = default!;

        _deviceList = default!;

        _raderList = default!;

        _stuffOutWorkList = default!;

        _workShopList = default!;

        _machineList = default!;

        #region 初始化环境监控数据
        EnvironmentList = [
            new EnvironmentModel { EnvItemName = "光照(Lux)", EnvItemValue = 123 },
            new EnvironmentModel { EnvItemName = "噪音(db)", EnvItemValue = 55 },
            new EnvironmentModel { EnvItemName = "温度(℃)", EnvItemValue = 80 },
            new EnvironmentModel { EnvItemName = "湿度(%)", EnvItemValue = 43 },
            new EnvironmentModel { EnvItemName = "PM2.5(m³)", EnvItemValue = 20 },
            new EnvironmentModel { EnvItemName = "硫化氢(PPM)", EnvItemValue = 15 },
            new EnvironmentModel { EnvItemName = "氮气(PPM)", EnvItemValue = 18 }
        ];
        #endregion

        #region 初始化环境监控数据Dto
        EnvironmentListDto = [];

        foreach (var environment in EnvironmentList)
        {
            EnvironmentListDto.Add(new EnvironmentModelDto(environment));
        } 
        #endregion

        #region 从仿真软件的从设备读取环境监控数据
        Task.Run(() => 
        {
            while (true)
            {
                using SerialPort serialPort = new("COM1", 9600, Parity.None, 8, StopBits.One);
                serialPort.Open();
                using Modbus.Device.ModbusSerialMaster master = Modbus.Device.ModbusSerialMaster.CreateRtu(serialPort);

                //功能码03
                ushort[] values = master.ReadHoldingRegisters(1, 0, 7);//1从设备地址，0起始地址，7读七个

                for (int i = 0; i < values.Length; i++)
                {
                    EnvironmentListDto[i].EnvItemValue = values[i];
                    EnvironmentListDto[i].ApplyChanges();
                }
            }
        });
        #endregion

        #region 初始化报警列表
        AlarmList = [
                new AlarmModel{Num = "01",Msg = "设备温度过高",Time = "2023-11-23 18:34:56",Duration = 7},
                new AlarmModel{Num = "02",Msg = "车间温度过高",Time = "2023-12-08 20:40:59",Duration = 10},
                new AlarmModel{Num = "03",Msg = "设备转速过快",Time = "2024-01-05 12:24:34",Duration = 12},
                new AlarmModel{Num = "04",Msg = "设备气压偏低",Time = "2024-02-04 19:58:00",Duration = 90},
            ];
        #endregion

        #region 初始化设备列表
        DeviceList = [
                new DeviceModel{ DeviceName = "电能(Kw.h)",Value = 60.8 },
                new DeviceModel{ DeviceName = "电压(V)",Value = 309 },
                new DeviceModel{ DeviceName = "电流(A)",Value = 5 },
                new DeviceModel{ DeviceName = "压差(kpa)",Value = 13 },
                new DeviceModel{ DeviceName = "温差(℃)",Value = 36 },
                new DeviceModel{ DeviceName = "振动(mm/s)",Value = 4.1 },
                new DeviceModel{ DeviceName = "转速(r/min)",Value = 2600 },
                new DeviceModel{ DeviceName = "气压(kpa)",Value = 0.5 },
            ];
        #endregion

        #region 初始化雷达数据 
        RaderList =
        [
            new RaderModel { ItemName = "排烟风机", Value = 90 },
            new RaderModel { ItemName = "客梯", Value = 30.00 },
            new RaderModel { ItemName = "供水机", Value = 34.89 },
            new RaderModel { ItemName = "喷淋水泵", Value = 69.59 },
            new RaderModel { ItemName = "稳压设备", Value = 20 },
        ];

        #endregion

        #region 初始化缺岗员工列表
        StuffOutWorkList = [
                new StuffOutWorkModel{ StuffName = "张晓婷", Position = "技术员", OutWorkCount = 123 },
                new StuffOutWorkModel{ StuffName = "李晓", Position = "操作员", OutWorkCount = 23 },
                new StuffOutWorkModel{ StuffName = "王克俭", Position = "技术员", OutWorkCount = 134 },
                new StuffOutWorkModel{ StuffName = "陈家栋", Position = "统计员", OutWorkCount = 143 },
                new StuffOutWorkModel{ StuffName = "杨过", Position = "技术员", OutWorkCount = 12 }
            ];
        #endregion

        #region 初始化车间列表
        WorkShopList = [
                new WorkShopModel { WorkShopName = "贴片车间", WorkingCount = 32, WaitCount = 8, WrongCount = 4, StopCount = 0 },
                new WorkShopModel { WorkShopName = "封装车间", WorkingCount = 20, WaitCount = 8, WrongCount = 4, StopCount = 0 },
                new WorkShopModel { WorkShopName = "焊接车间", WorkingCount = 68, WaitCount = 8, WrongCount = 4, StopCount = 0 },
                new WorkShopModel { WorkShopName = "贴片车间", WorkingCount = 68, WaitCount = 8, WrongCount = 4, StopCount = 0 }
            ];
        #endregion

        #region 初始化机台列表
        MachineList = [];
        Random random = new ();
        for (int i = 0; i < 20; i++)
        {
            int plan = random.Next(100, 1000);//计划量 随机数
            int finished = random.Next(0, plan);//已完成量
            MachineList.Add(new MachineModel
            {
                MachineName = "焊接机-" + (i + 1),
                FinishedCount = finished,
                PlanCount = plan,
                Status = "作业中",
                OrderNo = "H202212345678"
            });
        }
        #endregion
    }

    #region 监控用户控件
    /// <summary>
    /// 监控用户控件
    /// </summary>
    private UserControl _monitorUC;
    
    /// <summary>
    /// 监控用户控件
    /// </summary>
    public UserControl MonitorUC
    {
        get
        {
            //if (_MonitorUC == null)
            //    _MonitorUC = new MonitorUC();
            //_monitorUC ??= new MonitorUC();

            return _monitorUC;
        }
        set
        {
            _monitorUC = value;
            //if (PropertyChanged != null)
            //    PropertyChanged(this,new PropertyChangedEventArgs(nameof(MonitorUC)));
            OnPropertyChanged();
        }
    }
    #endregion

    #region 时间 小时:分钟
    /// <summary>
    /// 时间 小时:分钟
    /// </summary>
    public static string TimeStr
    {
        get
        {
            return DateTime.Now.ToString("HH:mm");
        }
    }

    /// <summary>
    /// 日期 年-月-日
    /// </summary>
    public static string DateStr
    {
        get
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    #endregion

    #region 星期
    /// <summary>
    /// 星期
    /// </summary>
    public static string WeekStr
    {
        get
        {
            int index = (int)DateTime.Now.DayOfWeek;
            string[] week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
            return week[index];
        }
    }
    #endregion

    #region 机台总数
    /// <summary>
    /// 机台总数
    /// </summary>
    private string _machineCount = "0298";

    /// <summary>
    /// 机台总数
    /// </summary>
    public string MachineCount
    {
        get
        {
            return _machineCount;
        }
        set
        {
            _machineCount = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 生产计数
    /// <summary>
    /// 生产计数
    /// </summary>
    private string _productCount = "0125";

    /// <summary>
    /// 生产计数
    /// </summary>
    public string ProductCount
    {
        get
        {
            return _productCount;
        }
        set
        {
            _productCount = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 不良计数
    /// <summary>
    /// 不良计数
    /// </summary>
    private string _badCount = (298-125).ToString();

    /// <summary>
    /// 不良计数
    /// </summary>
    public string BadCount
    {
        get
        {
            return _badCount;
        }
        set
        {
            _badCount = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 环境类
    /// <summary>
    /// 环境属性列表
    /// </summary>
    private ObservableCollection<EnvironmentModel> _environmentList;

    public ObservableCollection<EnvironmentModel> EnvironmentList
    {
        get { return _environmentList; }
        set 
        {
            _environmentList = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region 环境类Dto
    private ObservableCollection<EnvironmentModelDto> _environmentListDto;

    public ObservableCollection<EnvironmentModelDto> EnvironmentListDto
    {
        get { return _environmentListDto; }
        set
        {
            _environmentListDto = value;
            OnPropertyChanged();
        }
    } 
    #endregion

    #region 报警属性
    private List<AlarmModel> _alarmList;

    public List<AlarmModel> AlarmList
    {
        get { return _alarmList; }
        set 
        {
            _alarmList = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 设备列表
    private List<DeviceModel> _deviceList;

    public List<DeviceModel> DeviceList
    {
        get { return _deviceList; }
        set {
            _deviceList = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 雷达数据属性
    private List<RaderModel> _raderList;

    public List<RaderModel> RaderList
    {
        get { return _raderList; }
        set 
        {
            _raderList = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region 缺岗员工列表
    private List<StuffOutWorkModel> _stuffOutWorkList;

    public List<StuffOutWorkModel> StuffOutWorkList
    {
        get { return _stuffOutWorkList; }
        set 
        {
            _stuffOutWorkList = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 车间属性列表
    private List<WorkShopModel> _workShopList;

    public List<WorkShopModel> WorkShopList
    {
        get { return _workShopList; }
        set 
        { 
            _workShopList = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 机器属性列表
    private List<MachineModel> _machineList;

    public List<MachineModel> MachineList
    {
        get { return _machineList; }
        set
        {
            _machineList = value;
            OnPropertyChanged();
        }
    } 
    #endregion

}
