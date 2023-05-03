using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDestroy : MonoBehaviour
{
    //보스 공격범위 원 제거용
    void Update()
    {
        if (this.transform.childCount == 0)
            Destroy(this.gameObject);
    }
}
