using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Field : MonoBehaviour
{
    public Boss1Pattern _pattern;

    void Start()
    {
        _pattern = this.GetComponentInParent<Boss1Pattern>();
    }

    void Update()
    {
    }

    // 플레이어가 보스 지역에 있으면 패턴 시작
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;
        _pattern.isStart = true;
    }

    // 플레이어가 보스 지역에서 나가면 패턴 종료
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        _pattern.isStart = false;
    }
}
