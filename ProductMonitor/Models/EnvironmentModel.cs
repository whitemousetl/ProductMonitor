using ProductMonitor.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    public class EnvironmentModel
    {

        public string EnvItemName { get; set; } = string.Empty;

        public int EnvItemValue { get; set; }
    }
}
