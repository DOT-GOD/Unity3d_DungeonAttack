using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowShoot : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 100.0f)]
    public float _speed = 20.0f;
    public float _damage = 3.0f;
    private HealthPoint _target = null;

    private float currentTime = 0.0f;

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
