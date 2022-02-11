using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeSetting : Setting
{
    public enum TimeState 
    {
        _1m,
        _2m,
        _3m
    }

    private TimeState _state;
    private string[] _strings;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Time"))
        {
            _state = (TimeState)Enum.Parse(typeof(TimeState), PlayerPrefs.GetString("Time"));
        }
        else
        {
            PlayerPrefs.SetString("Time", _state.ToString());
        }

        _strings = _text.text.Split(':');
        _strings[1] = ":" + _state.ToString();
        _text.text = string.Concat(_strings);
    }

    public override void Act()
    {
        _strings = _text.text.Split(':');
        
        if(_state == TimeState._1m)
        {
            _state = TimeState._2m;
        }
        else if(_state == TimeState._2m)
        {
            _state = TimeState._3m;
        }
        else if(_state == TimeState._3m)
        {
            _state = TimeState._1m;
        }

        PlayerPrefs.SetString("Time", _state.ToString());
        _strings[1] = ":" + _state.ToString();
        _text.text = string.Concat(_strings);
    }
}
