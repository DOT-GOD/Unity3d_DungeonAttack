using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowShoot : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 100.0f)]
    public float _speed = 20.0f;                   //지정필요 : 화살 속도
    public float _damage = 3.0f;                   //지정필요 : 화살 피해량
    private HealthPoint _target = null;            //대상 컴포넌트 자동할당

    private float currentTime = 0.0f;              //시간경과 체크용

    void Start()
    {
        
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        this.transform.Translate(Vector3.forward * Time.deltaTime * _speed);

        if (currentTime > 2.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("함정화살명중");

        if (other.tag != "Player") return;
        Debug.Log("함정화살명중2");

        _target = other.transform.gameObject.GetComponent<HealthPoint>();
        _target.Damage(_damage);
        Destroy(this.gameObject);

    }
}
