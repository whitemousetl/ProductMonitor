using CommunityToolkit.Mvvm.ComponentModel;
using Mapster;
using ProductMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Dto;
public class EnvironmentModelDto : ObservableObject
{
    private readonly EnvironmentModel _environmentModel;

    public EnvironmentModelDto(EnvironmentModel environmentModel)
    {
        _environmentModel = environmentModel;
        _environmentModel.Adapt(this);
    }

    private string _envItemName = string.Empty;

    public string EnvItemName
    {
        get { return _envItemName; }
        set 
        { 
            _envItemName = value; 
            OnPropertyChanged();
        }
    }

    private int _envItemValue;

    public int EnvItemValue
    {
        get { return _envItemValue; }
        set 
        {
            _envItemValue = value;
            OnPropertyChanged();
        }
    }


    public void ApplyChanges() => this.Adapt(this._environmentModel);

    public void DiscardChanges() => _environmentModel.Adapt(this);
}
