using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    [SerializeField]
    public bool isToMonster = true;                 // 지정필요 : 대상이 플레이어인지 몬스터인지 여부

    HealthPoint _target = null;                     // 대상 컴포넌트 자동할당

    [SerializeField]
    [Range(1.0f,20.0f)]
    float _damage = 5.0f;                           // 지정필요 : 화염구 공격 피해량


    // Collision에 isTrigger활성화 되야 작동함
    private void OnTriggerEnter(Collider other)
    {
        // 공격대상과 태그가 일치하는지 판별
        if (isToMonster)
            if (other.tag != "Monster") return;
        else
            if (other.tag != "Player") return;

        _target = other.transform.parent.gameObject.GetComponent<HealthPoint>();
        _target.Damage(_damage);

    }
}
