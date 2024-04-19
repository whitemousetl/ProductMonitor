using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models;
/// <summary>
/// 设备数据模型
/// </summary>
public class DeviceModel
{
    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; } = string.Empty;

    /// <summary>
    /// 设备值
    /// </summary>
    public double Value {  get; set; }
}
