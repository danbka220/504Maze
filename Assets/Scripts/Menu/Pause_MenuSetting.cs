using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pause_MenuSetting : Setting
{
    public override void Act()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
