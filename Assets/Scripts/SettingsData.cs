using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SettingsData", fileName = "new SettingsData", order = 51)]
public class SettingsData : ScriptableObject
{
    public LangHandler.Lang lang = LangHandler.Lang.RU;
    public DifficultySetting.Diff difficulty = DifficultySetting.Diff.EASY;
    public TimerSetting.TimerState timer = TimerSetting.TimerState.OFF;
    public TimeSetting.TimeState time = TimeSetting.TimeState._1m;
    public SoundSetting.SoundState sound = SoundSetting.SoundState.ON;
}
