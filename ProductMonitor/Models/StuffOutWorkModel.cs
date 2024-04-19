using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    /// <summary>
    /// 缺岗数据模型
    /// </summary>
    public class StuffOutWorkModel
    {
        /// <summary>
        /// 缺岗姓名
        /// </summary>
        public string StuffName {  get; set; } = string.Empty;

        /// <summary>
        /// 缺岗职位
        /// </summary>
        public string Position { get; set;} = string.Empty;

        /// <summary>
        /// 缺岗次数
        /// </summary>
        public int OutWorkCount {  get; set;}

        /// <summary>
        /// 界面展示宽度
        /// </summary>
        public int ShowWidth { get { return OutWorkCount * 70 / 100; } }
    }
}
