using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RUNSetting : Setting
{
    [SerializeField] private GameObject _loadingWindow;
    [SerializeField] private GameObject[] _dots;
    [SerializeField] private float _delay = .2f;
    public override void Act()
    {
        _loadingWindow.SetActive(true);
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        foreach(GameObject dot in _dots)
        {
            dot.SetActive(false);
        }
        yield return new WaitForEndOfFrame();

        for(int i = 0; i < _dots.Length / 3; i++)
        {
            _dots[i].SetActive(true);
            yield return new WaitForSeconds(_delay);
        }
        
        yield return new WaitForSeconds(.5f);

        for(int i = _dots.Length / 3; i < _dots.Length; i++)
        {
            _dots[i].SetActive(true);
            yield return new WaitForSeconds(_delay);
        }
        yield return new WaitForSeconds(.5f);
        DifficultySetting.Diff diff = FindObjectOfType<DifficultySetting>().Difficult;
        SceneManager.LoadSceneAsync(diff.ToString());
    }
}
