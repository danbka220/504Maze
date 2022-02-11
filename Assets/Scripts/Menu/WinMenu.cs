using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject _winMenuGo;

    private void OnEnable()
    {
        Player.Winned += Open;
    }

    private void OnDisable()
    {
        Player.Winned -= Open;
    }

    private void Update()
    {
        if(_winMenuGo.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }

    private void Open()
    {
        _winMenuGo.SetActive(true);
    }
}
