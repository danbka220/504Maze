using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextTyper : MonoBehaviour
{
    [SerializeField] private float _speed = .2f;
    [SerializeField] private bool _primary = false;
    [SerializeField] private TextTyper _nextText;
    public UnityEvent _done;
    private TextMeshProUGUI _text;

    private void Start()
    {
        TryGetComponent(out _text);
        _text.maxVisibleCharacters = 0;
        if(_primary)
            StartType();
    }

    public void StartType()
    {
        StartCoroutine(Typing());
    }

    private IEnumerator Typing() 
    {
        yield return new WaitForSeconds(_speed);
        int count = _text.textInfo.characterCount;
        _text.maxVisibleCharacters = 0;

        while(_text.maxVisibleCharacters < count)
        {
            _text.maxVisibleCharacters++;
            yield return new WaitForSeconds(_speed);
        }
        _text.maxVisibleCharacters = 999;

        if(_nextText)
            _nextText.StartType();

        _done?.Invoke();
    }
}
