using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class AD : MonoBehaviour
{
    [DllImport("__Internal")]
    
    private static extern int ShowAd();
    private void Start()
    {
        ShowAd();
    }
}
