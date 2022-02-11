using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangSetting : Setting
{
    private string[] _strings;
    private LangHandler _lang;

    private void Start()
    {
        _lang = FindObjectOfType<LangHandler>();
    }

    public override void Act()
    {
        _lang.ChangeLang();
    }
}
