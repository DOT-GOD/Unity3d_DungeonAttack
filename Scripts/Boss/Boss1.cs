using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public GameObject[] _openBlock;
    public GameObject _spotLight;

    private HealthPoint _health;

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
        for(int i = 0; i < _openBlock.Length; i++)
        {
            _openBlock[i].gameObject.SetActive(false);
        }
        _spotLight.gameObject.SetActive(true);
    }
}
