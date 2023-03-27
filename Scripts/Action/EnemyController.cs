using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 10.0f)]
    float _searchRange = 5.0f;

    [SerializeField]
    GameObject _player = null;

    Attack _attack = null;
    HealthPoint _healthPoint = null;
    Movement _movement = null;
    ActionManager _actionManager = null;

    [SerializeField]
    WayPoint _wayPoint = null;
    int _currWaypointIndex = 0;

    [SerializeField]
    float _waypointDistance = 1.0f;

    Vector3 _initPosition;

    [SerializeField]
    float _waitTime = 5.0f;
    float _foundPlayerLastTime = Mathf.Infinity;

    [SerializeField]
    float _dwellTime = 2.0f;
    float _arrivedAtWaypoint = Mathf.Infinity;

    [SerializeField, Range(0, 1)]
    float _patrolSpeedFraction = 0.2f;


    private void Awake()
    {
        _attack = this.GetComponent<Attack>();
        _healthPoint = this.GetComponent<HealthPoint>();
        _movement = this.GetComponent<Movement>();
        _actionManager = this.GetComponent<ActionManager>();
        //_wayPoint = this.GetComponent<WayPoint>();
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _initPosition = this.transform.position;
    }
    private void Update()
    {
        // rotation고정
        {
            //this.transform.eulerAngles = new Vector3(0.0f, transform.rotation.y, 0.0f);
        }

        //확인
        //{
        //    bool temp = _player != true;
        //    Debug.Log(this.gameObject.name + "/" + temp.ToString());
        //}

        if (_healthPoint.IsDead) return;

        if(IsinRange())
            Debug.Log(this.gameObject.name + "공격범위진입");

        //if (_attack.CanAttack(_player))
        //    Debug.Log(this.gameObject.name + "공격가능상태");

        if (IsinRange() && _attack.CanAttack(_player))
        {
            Debug.Log(this.gameObject.name + "공격 가능");
            //_attack.Begin(_player);
            Attack();
        }
        else if (_foundPlayerLastTime < _waitTime)
        {
            _actionManager.StopAction();
            //Debug.Log("Wait" + _foundPlayerLastTime);
        }
        else
        {
            //_attack.End();
            //_movement.Begin(_initPosition, _patrolSpeedFraction);

            Patrol();
        }

        _foundPlayerLastTime += Time.deltaTime;

        _arrivedAtWaypoint += Time.deltaTime;
    }

    private void Patrol()
    {
        //웨이포인트가 없을 경우 첫지점으로 이동
        Vector3 next = _initPosition;
        if (_wayPoint != null)
        {
            if (IsNearWaypoint() == true)
            {
                _arrivedAtWaypoint = 0.0f;

                _currWaypointIndex = _wayPoint.GetNextIndex(_currWaypointIndex);

                // 정찰 속도 랜덤
                //_patrolSpeedFraction = Random.Range(0.2f, 1.0f);

                // 정찰 속도 일정
                _patrolSpeedFraction = 0.3f;

            }

            next = GetCurrentWaypoint();
        }
        if (_arrivedAtWaypoint > _dwellTime)
        {
            _movement.Begin(next, _patrolSpeedFraction);
        }
    }

    private bool IsNearWaypoint()
    {
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 waypoint = new Vector2(GetCurrentWaypoint().x, GetCurrentWaypoint().z);

        return Vector2.Distance(point, waypoint) < _waypointDistance;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return _wayPoint.GetWaypoint(_currWaypointIndex);
    }

    public bool IsinRange()
    {
        Vector2 targetPoint = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);

        //_distance = Vector2.Distance(targetPoint, point);
        return Vector2.Distance(targetPoint, point) < _searchRange;
    }

    private void Attack()
    {
        _foundPlayerLastTime = 0.0f;
        _attack.Begin(_player);
    }

    private void Return()
    {
        _movement.Begin(_initPosition, _patrolSpeedFraction);
    }

    // gui 거리표시
    //float _distance = 0.0f;
    //private void OnGUI()
    //{
    //    GUI.Box(new Rect(0, 0, 200, 20), _distance.ToString());
    //}

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(this.transform.position, new Vector3(3, 3, 3));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _searchRange);
    }
}
