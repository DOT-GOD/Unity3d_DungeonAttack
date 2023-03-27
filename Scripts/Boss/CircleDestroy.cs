using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDestroy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (this.transform.childCount == 0)
            Destroy(this.gameObject);
    }
}
