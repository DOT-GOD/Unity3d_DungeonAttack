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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;
        _pattern.isStart = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        _pattern.isStart = false;
    }
}
