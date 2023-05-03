using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShooter : MonoBehaviour
{
    public GameObject shooter;

    [SerializeField]
    [Range(0.1f,10.0f)]
    public float fireDelay = 3.0f;                 //지정필요 : 공격 딜레이

    private float remainDelay = 0.0f;              //남은딜레이

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
        //생성딜레이가 0이하일 때
        if(remainDelay <= 0)
        {
            GameObject tempShooter;
            tempShooter = GameObject.Instantiate(shooter, this.transform.position, Quaternion.identity);
            tempShooter.transform.rotation = this.transform.rotation;
            remainDelay += fireDelay;
        }
    }
}
