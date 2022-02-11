using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerSetting : Setting
{
    public enum TimerState
    {
        OFF,
        ON
    }

    private string[] _strings;
    private TimerState _state;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Timer"))
        {
            _state = (TimerState)Enum.Parse(typeof(TimerState), PlayerPrefs.GetString("Timer"));
            _strings = _text.text.Split(':');
            _strings[1] = ":" + _state.ToString();
            _text.text = string.Concat(_strings);
        }
        else
        {
            PlayerPrefs.SetString("Timer", _state.ToString());
        }
    }

    public override void Act()
    {
        _strings = _text.text.Split(':');

        if(_state == TimerState.OFF)
        {
            _state = TimerState.ON;
        }
        else
        {
            _state = TimerState.OFF;
        }

        PlayerPrefs.SetString("Timer", _state.ToString());
        _strings[1] = ":" + _state.ToString();
        _text.text = string.Concat(_strings);
    }
}
