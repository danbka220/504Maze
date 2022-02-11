using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LangChanger : MonoBehaviour
{
    [SerializeField] private string _ru1;
    private string _en1, _en2;
    [SerializeField] private bool _translate2Words;
    [SerializeField] private string _ru2;
    private TextMeshProUGUI _text;
    private string _string;

    private void Awake()
    {
        TryGetComponent(out _text);
        _string = _text.text;
        string[] strings = _string.Split(':');
        _en1 = strings[0];
        if(strings.Length > 1)
            _en2 = strings[1];
        ChangeLang(FindObjectOfType<LangHandler>().GetLang());
    }

    private void OnEnable()
    {
        LangHandler.Changed += ChangeLang;
    }

    private void OnDisable()
    {
        LangHandler.Changed -= ChangeLang;
    }

    private void ChangeLang(LangHandler.Lang lang)
    {
        _string = _text.text;
        string[] strings = _string.Split(':');

        if(lang == LangHandler.Lang.RU)
        {
            strings[0] = _ru1 + ':';
            if(_translate2Words)
                strings[1] = _ru2;
        }
        else
        {
            strings[0] = _en1 + ':';
            if(_translate2Words)
                strings[1] = _en2;
        }

        _text.text = string.Concat(strings);
    }
}
