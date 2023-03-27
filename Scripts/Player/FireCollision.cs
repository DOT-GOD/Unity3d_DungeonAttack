using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    [SerializeField]
    public bool isToMonster = true;

    HealthPoint _target = null;

    [SerializeField]
    [Range(1.0f,20.0f)]
    float _damage = 5.0f;


    // Collision에 isTrigger활성화 되야 작동함
    private void OnTriggerEnter(Collider other)
    {
        if (isToMonster)
        {
            if (other.tag != "Monster") return;
        }
        else
        {
            if (other.tag != "Player") return;
        }
        //_target.Damage(_damage);


        _target = other.transform.parent.gameObject.GetComponent<HealthPoint>();
        _target.Damage(_damage);

    }
}
