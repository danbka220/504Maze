using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextBlink : MonoBehaviour
{
    [SerializeField, Min(0.01f)] private float _blinkDelay = .2f;
    private TextMeshProUGUI _text;

    private void Start()
    {
        TryGetComponent(out _text);
        StartCoroutine(Blink());
    }
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while(true)
        {
            yield return new WaitForSeconds(_blinkDelay);
            _text.color = new Color(_text.color.r,_text.color.g,_text.color.b,1);
            yield return new WaitForSeconds(_blinkDelay);
            _text.color = new Color(_text.color.r,_text.color.g,_text.color.b,.3f);
        }
    }
}
