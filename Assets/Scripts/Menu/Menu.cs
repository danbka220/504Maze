using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private Setting[] _settings;
    [SerializeField] private RectTransform _dot;
    private Setting _currentSetting;
    private Vector3 _settingPos;
    private int _settingId;
    private bool _buttonPressed = false;
    private bool _inited = false;

    public void Init()
    {
        _currentSetting = _settings[0];
        _settingId = 0;
        _settingPos = new Vector3(_dot.localPosition.x, _currentSetting.transform.localPosition.y);
        _dot.localPosition = _settingPos;
        _inited = true;
    }

    private void Update()
    {
        if(!_inited) return;
        
        if(!_buttonPressed)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                _currentSetting.Act();
                _buttonPressed = true;
            }

            float input = Input.GetAxisRaw("Vertical");
            if(input != 0)
            {
               ChangeSetting(input);
            }
        }

        if(!Input.anyKey)
        {
            _buttonPressed = false;
        }
    }

    private void ChangeSetting(float input)
    {
        _buttonPressed = true;
        if(input < 0)
        {
            if(_settingId < _settings.Length - 1)
            {
                _settingId++;
            }
        }
        else
        {
            if(_settingId > 0)
            {
                _settingId--;
            }
        }

        _currentSetting = _settings[_settingId];

        _settingPos = new Vector3(_dot.localPosition.x, _currentSetting.transform.localPosition.y);
        _dot.localPosition = _settingPos;
    }
}
