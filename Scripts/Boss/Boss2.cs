using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public GameObject _player;
    public GameObject _wall;
    public GameObject _shooter;
    public GameObject _position;

    private EnemyController _controller = null;
    private HealthPoint _health;
    private float currentTime = 0;

    void Start()
    {
        _health = this.GetComponent<HealthPoint>();
        _controller = this.GetComponent<EnemyController>();
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (_health._value == 0)
        {
            Dead();
        }

        // n초마다 머리 지점에서 플레이어에게 화염구 발사
        if(currentTime > 5.0f)
        {
            if (_controller.IsinRange())
            {
                FireShoot();
            }

            currentTime = 0;
        }
    }

    void Dead()
    {
        _wall.gameObject.SetActive(false);
    }

    void FireShoot()
    {
        GameObject tempShooter;
        tempShooter = GameObject.Instantiate(_shooter, _position.transform.position, Quaternion.identity);
        tempShooter.transform.LookAt(_player.transform);
    }
}
