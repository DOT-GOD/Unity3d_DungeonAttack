using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTrigger : MonoBehaviour
{
    public GameObject _bgmManager;
    public int _bgmNumSetting;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        _bgmManager.GetComponent<BGMManager>()._bgmNum = _bgmNumSetting;
    }
}
