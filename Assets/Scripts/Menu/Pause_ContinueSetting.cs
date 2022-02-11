using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pause_ContinueSetting : Setting
{
    public UnityEvent _event;
    public override void Act()
    {
        _event?.Invoke();
    }
}
