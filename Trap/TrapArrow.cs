using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapArrow : MonoBehaviour
{
    public GameObject arrow;                      //지정필요 : 화살프리팹
    public GameObject trapTrigger;                //지정필요 : 함정발동 트리거
    public GameObject stopTrigger;                //지정필요 : 함정정지 트리거
    public GameObject[] trapShooter;              //지정필요 : 화살발사구

    [SerializeField]
    [Range(0.1f, 5.0f)]
    public float trapDelay = 2.0f;                //지정필요 : 화살발사 딜레이

    private float currentTime = 0.0f;             //시간경과체크용

    void Start()
    {
        // 함정 트리거(ex.핏자국)에 충돌스크립트 추가
        trapTrigger.AddComponent<TrapTrigger>();
        stopTrigger.AddComponent<TrapTrigger>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if(trapTrigger.GetComponent<TrapTrigger>().isActivated == true)
        {
            if (currentTime > trapDelay)
            {
                for (int i = 0; i < trapShooter.Length; i++)
                {
                    ShootArrow(trapShooter[i]);
                }

                currentTime = 0;
            }
        }

        // 함정 정지 트리거
        if (stopTrigger.GetComponent<TrapTrigger>().isActivated == true)
        {
            trapTrigger.GetComponent<TrapTrigger>().isActivated = false;
            stopTrigger.GetComponent<TrapTrigger>().isActivated = false;
        }
    }

    void ShootArrow(GameObject shooter)
    {
        Debug.Log("함정화살발사");
        GameObject tempArrow;
        tempArrow = GameObject.Instantiate(arrow, shooter.transform.position, Quaternion.identity);
        tempArrow.transform.rotation = shooter.transform.rotation;
    }
}
