using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LangHandler : MonoBehaviour
{
    public enum Lang 
    {
        RU,
        EN
    }
    public Lang lang;

    public static Action<Lang> Changed;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Lang"))
        {
            lang = (Lang)Enum.Parse(typeof(Lang), PlayerPrefs.GetString("Lang"));
        }
        else
        {
            if(Application.systemLanguage == SystemLanguage.Russian)
                lang = Lang.RU;
            else
                lang = Lang.EN;
            PlayerPrefs.SetString("Lang", lang.ToString());
        }
        Changed?.Invoke(lang);
    }

    public Lang GetLang()
    {
        return lang;
    }

    public void ChangeLang()
    {
        if(lang == Lang.RU)
        {
            lang = Lang.EN;
        }
        else
        {
            lang = Lang.RU;
        }
        PlayerPrefs.SetString("Lang", lang.ToString());
        Changed?.Invoke(lang);
    }

    private void OnValidate()
    {
        Changed?.Invoke(lang);
    }
}
