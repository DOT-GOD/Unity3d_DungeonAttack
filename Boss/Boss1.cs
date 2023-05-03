using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public GameObject[] _openBlock;                      // 지정필요 : 보스방 조명
    public GameObject _spotLight;                        // 지정필요 : 보스 처치시 출구 스포트라이트

    private HealthPoint _health;                         // 가지고 있는 컴포넌트 자동할당

    void Start()
    {
        _health = this.GetComponent<HealthPoint>();
    }

    void Update()
    {
        if(_health._value == 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        // 죽으면 기존 조명 비활성화 후 스포트라이트 활성화
        for(int i = 0; i < _openBlock.Length; i++)
        {
            _openBlock[i].gameObject.SetActive(false);
        }
        _spotLight.gameObject.SetActive(true);
    }
}
