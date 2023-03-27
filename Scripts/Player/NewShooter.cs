using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShooter : MonoBehaviour
{
    public GameObject shooter;

    [SerializeField]
    [Range(0.1f,10.0f)]
    public float fireDelay = 3.0f;

    private float remainDelay = 0.0f;
    private int ShooterNum = 0;

    void Update()
    {
#if UNITY_ANDROID

#elif UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootFire();
        }
#endif
        if(remainDelay > 0)
            remainDelay -= Time.deltaTime;
    }

    // 플레이어 시점방향으로 투사체가 휘는 상황을 방지하기 위해 새로운 사수 오브젝트 생성
    public void ShootFire()
    {
        if(remainDelay > 0)
        {
        }
        else
        {
            GameObject tempShooter;
            tempShooter = GameObject.Instantiate(shooter, this.transform.position, Quaternion.identity);
            tempShooter.transform.rotation = this.transform.rotation;
            remainDelay += fireDelay;
        }
    }
}
