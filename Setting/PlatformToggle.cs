using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggle : MonoBehaviour
{
    public GameObject AndroidUIObject;           //지정필요 : 안드로이드용 UI

    void Start()
    {
        //안드로이드 사용시에만 UI활성화
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
