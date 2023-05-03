using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTrigger : MonoBehaviour
{
    public GameObject _bgmManager;                //지정필요 : BGM매니저
    public int _bgmNumSetting;                    //지정필요 : 재생할 BGM번호

    //플레이어 트리거 작동시 BGM변경
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        _bgmManager.GetComponent<BGMManager>()._bgmNum = _bgmNumSetting;
    }
}
