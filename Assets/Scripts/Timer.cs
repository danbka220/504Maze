using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    private DateTime _time;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _winText;
    private Coroutine _routine;

    private void Start()
    {
        _time = new DateTime(0);
        _routine = StartCoroutine(Sec());
    }

    private void OnEnable()
    {
        PauseMenu.OnPause += ChangeState;
        Player.Winned += Save;
    }

    private void OnDisable()
    {
        PauseMenu.OnPause -= ChangeState;
        Player.Winned -= Save;
    }

    private void Save()
    {
        StopCoroutine(_routine);
        string[] str = _winText.text.Split(':');
        str[1] = ":" + _time.ToString("mm.ss");
        _winText.text = string.Concat(str);
        if (PlayerPrefs.HasKey(PlayerPrefs.GetString("Diff") + "res"))
        {
            string[] strt = PlayerPrefs.GetString(PlayerPrefs.GetString("Diff") + "res").Split('.');
            string min = strt[0].TrimStart('0') == string.Empty ? "0" : strt[0].TrimStart('0');
            string sec = strt[1].TrimStart('0') == string.Empty ? "0" : strt[1].TrimStart('0');
            int secInMem = int.Parse(min) * 60 + int.Parse(sec);
            
            double seconds = TimeSpan.FromTicks(_time.Ticks).TotalSeconds - secInMem;

            if (seconds < 0)
                PlayerPrefs.SetString(PlayerPrefs.GetString("Diff") + "res", _time.ToString("mm.ss"));
        }
        else
        {
            PlayerPrefs.SetString(PlayerPrefs.GetString("Diff") + "res", _time.ToString("mm.ss"));
        }

    }

    private void ChangeState(bool state)
    {
        if(!state)
        {
            _routine = StartCoroutine(Sec());
        }
        else
        {
            StopCoroutine(_routine);
        }
    }

    private IEnumerator Sec()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            _time = _time.AddSeconds(1);
            _text.text = _time.ToString("mm.ss");
        }
    }
}
