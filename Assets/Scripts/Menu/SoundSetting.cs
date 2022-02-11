using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SoundSetting : Setting
{
    public enum SoundState
    {
        ON,
        OFF
    }

    private SoundState _state;
    private string[] _strings;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Sound"))
        {
            _state = (SoundState)Enum.Parse(typeof(SoundState), PlayerPrefs.GetString("Sound"));
            _strings = _text.text.Split(':');
            _strings[1] = ":" + _state.ToString();
            _text.text = string.Concat(_strings);
        }
        else
        {
            PlayerPrefs.SetString("Sound", _state.ToString());
        }
    }

    public override void Act()
    {
        _strings = _text.text.Split(':');

        if(_state == SoundState.OFF)
        {
            _state = SoundState.ON;
        }
        else
        {
            _state = SoundState.OFF;
        }
        PlayerPrefs.SetString("Sound", _state.ToString());
        _strings[1] = ":" + _state.ToString();
        _text.text = string.Concat(_strings);
    }
}
