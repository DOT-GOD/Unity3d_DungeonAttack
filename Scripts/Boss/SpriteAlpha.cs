using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAlpha : MonoBehaviour
{
    [SerializeField]
    public GameObject shooter = null;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    public float alpha = 0.2f;

    [SerializeField]
    [Range(0.0f, 5.0f)]
    public float delay = 0.0f;

    [SerializeField]
    [Range(5.0f, 100.0f)]
    public float areaSize = 10.0f;

    [SerializeField]
    public bool isInner = false;

    [SerializeField]
    [Range(0.0f, 20.0f)]
    public float _damage = 5.0f;

    private float areaScale = 0.0f;
    private float currentTime = 0.0f;
    private HealthPoint _target = null;

    bool damaged = false;
    bool particlePlayed = false;

    void Start()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        Color color = spr.color;
        color.a = alpha;
        spr.color = color;

    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // delay 시간 동안 areaSize까지 공격범위 표시 원 스케일 증가
        if (isInner && areaScale < areaSize)
        {
            areaScale += (areaSize / delay) * Time.deltaTime;
            this.transform.localScale = new Vector3(areaScale, areaScale, 1);
        }

        if(delay < currentTime && particlePlayed == false && shooter != null)
        {
            Debug.Log("파티클재생");
            // 파티클 재생
            ShootFire();
            particlePlayed = true;
        }

        // 스킬발동후 n초 후 영역 소멸
        if (delay + 0.8f < currentTime)
            Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        // delay시간 이내거나
        // 대상이 Player가 아니거나
        // 이미 데미지를 입었으면 return
        if (currentTime < delay) return;
        if (other.tag != "Player") return;
        if (damaged) return;
        Debug.Log("보스1바닥트리거발동");

        // 데미지 계산
        _target = other.transform.gameObject.GetComponent<HealthPoint>();
        _target.Damage(_damage);
        damaged = true;


    }

    // 새로운 사수 오브젝트 생성
    public void ShootFire()
    {
        GameObject tempShooter;
        tempShooter = GameObject.Instantiate(shooter, this.transform.position, Quaternion.identity);
        tempShooter.transform.rotation = this.transform.rotation;
    }
}
