using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_MovingDirection
{
    X,
    Y,
    Z,
}
public class TrapWall : MonoBehaviour
{
    public GameObject trapTrigger;

    [SerializeField]
    [Range(0.1f, 20.0f)]
    public float trapDelay = 2.0f;                          //지정필요 : 움직이는 벽 이동시간

    [SerializeField]
    [Range(-50.0f, 50.0f)]
    public float movingDistance = 4.0f;                     //지정필요 : 이동거리

    public ENUM_MovingDirection _direction;                 //지정필요 : 진행방향

    private float currentTime = 0.0f;                       //경과시간 체크용


    void Start()
    {
        // 함정 트리거(ex.핏자국)에 충돌스크립트 추가
        trapTrigger.AddComponent<TrapTrigger>();
    }

    void Update()
    {
        if (trapTrigger.GetComponent<TrapTrigger>().isActivated == true)
        {
            currentTime += Time.deltaTime;

            // trapDelay 시간만큼 벽 이동후 정지
            if (currentTime > trapDelay) return;
            
            float tempDistance = (movingDistance * Time.deltaTime) / trapDelay;

            //벽 이동방향체크
            switch (_direction)
            {
                case ENUM_MovingDirection.X:
                    this.transform.position = new Vector3(this.transform.position.x + tempDistance, this.transform.position.y, this.transform.position.z);
                    break;
                case ENUM_MovingDirection.Y:
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + tempDistance, this.transform.position.z);
                    break;
                case ENUM_MovingDirection.Z:
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + tempDistance);
                    break;
                default:
                    break;
            }
        }
    }
}
