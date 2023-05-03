using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public GameObject _player;                                   // Player 태그 자동할당
    public GameObject _wall;                                     // 지정필요 : 성으로 가는 길 막는 장애물
    public GameObject _shooter;                                  // 지정필요 : 화염구 슈터 프리팹
    public GameObject _position;                                 // 지정필요 : 위치
                                                                    
    private EnemyController _controller = null;                  // 가지고 있는 컴포넌트 자동할당
    private HealthPoint _health;                                 // 가지고 있는 컴포넌트 자동할당

    private float currentTime = 0;                               // 화염구 딜레이 체크용

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

    // 슈터를 생성하는 방식으로 화염구 발사 (투사체 방향 전환 방지)
    void FireShoot()
    {
        GameObject tempShooter;
        tempShooter = GameObject.Instantiate(_shooter, _position.transform.position, Quaternion.identity);
        tempShooter.transform.LookAt(_player.transform);
    }
}
