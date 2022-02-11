using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DifficultySetting : Setting
{
    public enum Diff
    {
        EASY,
        NORMAL,
        HARD
    }

    [SerializeField] private TextMeshProUGUI _resultText;

    private const string _easyRu = "Легко";
    private const string _normalRu = "Средне";
    private const string _hardRu = "Сложно";

    public Diff Difficult => _difficulty;

    private string[] _strings;
    private Diff _difficulty;
    private LangHandler _lang;

    private void Start()
    {
        _lang = FindObjectOfType<LangHandler>();

        if(!PlayerPrefs.HasKey("Diff"))
        {
            _difficulty = _settings.difficulty;
            PlayerPrefs.SetString("Diff", _difficulty.ToString());
        }
        else
        {
            _difficulty = (Diff)Enum.Parse(typeof(Diff), PlayerPrefs.GetString("Diff"));
        }
        ChangeLang(_lang.GetLang());

        string[] res = _resultText.text.Split(':');
        if (PlayerPrefs.HasKey(_difficulty.ToString() + "res"))
        {
            res[1] = ":" + PlayerPrefs.GetString(_difficulty.ToString() + "res");
        }
        else
        {
            res[1] = ":none";
        }
        _resultText.text = string.Concat(res);
    }

    private void OnEnable()
    {
        LangHandler.Changed += ChangeLang;
    }

    private void OnDisable()
    {
        LangHandler.Changed -= ChangeLang;
    }

    public override void Act()
    {
        if(_difficulty == Diff.EASY)
        {
            _difficulty = Diff.NORMAL;
        }
        else if(_difficulty == Diff.NORMAL)
        {
            _difficulty = Diff.HARD;
        }
        else if(_difficulty == Diff.HARD)
        {
            _difficulty = Diff.EASY;
        }
        PlayerPrefs.SetString("Diff", _difficulty.ToString());
        ChangeLang(_lang.GetLang());

        string[] res = _resultText.text.Split(':');
        if (PlayerPrefs.HasKey(_difficulty.ToString() + "res"))
        {
            res[1] = ":" + PlayerPrefs.GetString(_difficulty.ToString() + "res");
        }
        else
        {
            res[1] = ":none";
        }
        _resultText.text = string.Concat(res);
    }

    private void ChangeLang(LangHandler.Lang lang)
    {
        _strings = _text.text.Split(':');

        if(lang == LangHandler.Lang.RU)
        {
            _strings[0] = "СЛОЖНОСТЬ:";
            _strings[1] = _difficulty == Diff.EASY ? _easyRu : _difficulty == Diff.NORMAL ? _normalRu : _hardRu;
        }
        else
        {
            _strings[0] = "DIFFICULTY:";
            _strings[1] = _difficulty.ToString();
        }

        _text.text = string.Concat(_strings);

        string[] res = _resultText.text.Split(':');
        if (PlayerPrefs.HasKey(_difficulty.ToString() + "res"))
        {
            res[1] = ":" + PlayerPrefs.GetString(_difficulty.ToString() + "res");
        }
        else
        {
            res[1] = ":none";
        }
        _resultText.text = string.Concat(res);
    }
}
