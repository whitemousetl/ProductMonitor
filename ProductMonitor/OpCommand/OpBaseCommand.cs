﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductMonitor.OpCommand
{
    public class OpBaseCommand(Action action) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action _action = action;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
}
