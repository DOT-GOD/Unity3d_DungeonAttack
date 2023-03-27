using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform _target = null;
    
    void Start()
    {
        GameObject _temp = GameObject.FindGameObjectWithTag("Player");
        _target = _temp.transform;
    }

    void Update()
    {
        this.transform.position = _target.position;
    }
}
