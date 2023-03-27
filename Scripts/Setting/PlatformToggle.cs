using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggle : MonoBehaviour
{
    public GameObject AndroidUIObject;

    void Start()
    {
#if UNITY_ANDROID
        AndroidUIObject.SetActive(true);
#elif UNITY_EDITOR || UNITY_STANDALONE
        AndroidUIObject.SetActive(false);
#endif
    }

    void Update()
    {
    }
}
