using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Setting[] _settings;
    [SerializeField] private GameObject _dot;
    private int _currentSettingId = 1;
    private bool _paused;
    public static Action<bool> OnPause;
    private bool _pressed = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(!Input.anyKey)
        {
            _pressed = false;
        }

        float input = Input.GetAxisRaw("Horizontal");
        if(input != 0 && !_pressed && _paused)
        {
            if(input > 0 && _currentSettingId < _settings.Length - 1)
            {
                _currentSettingId++;
                _dot.transform.position = _settings[_currentSettingId].transform.position + new Vector3(2.5f,0);
                _pressed = true;
            }
            else if(input < 0 && _currentSettingId > 0)
            {
                _currentSettingId--;
                _dot.transform.position = _settings[_currentSettingId].transform.position + new Vector3(2.5f,0);
                _pressed = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return) && _paused)
        {
            _settings[_currentSettingId].Act();
        }

        if(!_pauseMenu.activeInHierarchy && !_paused)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeState(true);
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ChangeState(false);
            }
        }
    }

    public void ChangeState(bool state)
    {
        _pauseMenu.SetActive(state);
        _paused = state;
        OnPause?.Invoke(_paused);
        _dot.transform.position = _settings[_currentSettingId].transform.position + new Vector3(2.5f,0);
    }
}
