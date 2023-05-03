using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform _target = null;                // 플레이어 위치 자동할당
    
    void Start()
    {
        GameObject _temp = GameObject.FindGameObjectWithTag("Player");
        _target = _temp.transform;
    }

    void Update()
    {
        //카메라를 플레이어 위치로 이동
        this.transform.position = _target.position;
    }
}
