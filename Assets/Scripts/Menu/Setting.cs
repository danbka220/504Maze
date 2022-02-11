using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class Setting : MonoBehaviour
{
    [SerializeField] protected SettingsData _settings;
    public abstract void Act();
    protected TextMeshProUGUI _text;
    private void Awake()
    {
        TryGetComponent(out _text);
    }
}
