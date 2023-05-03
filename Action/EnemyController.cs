using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    [Range(1.0f, 10.0f)]
    float _searchRange = 5.0f;

    [SerializeField]
    GameObject _player = null;                          // Player 태그 자동할당

    Attack _attack = null;                              // 가지고 있는 컴포넌트 자동할당
    HealthPoint _healthPoint = null;                    // 가지고 있는 컴포넌트 자동할당
    Movement _movement = null;                          // 가지고 있는 컴포넌트 자동할당
    ActionManager _actionManager = null;                // 가지고 있는 컴포넌트 자동할당

    [SerializeField]
    WayPoint _wayPoint = null;                          // 지정필요 : 웨이포인트 오브젝트
    int _currWaypointIndex = 0;

    [SerializeField]
    float _waypointDistance = 1.0f;                     // 지정필요 : 웨이포인트와의 거리(값보다 가까우면 도착으로 간주)

    Vector3 _initPosition;                              // 현재 위치 자동할당

    [SerializeField]
    float _waitTime = 5.0f;                             // 지정필요 : 추적시간(미탐지 시간이 지정값보다 길어지면 정찰실행)
    float _foundPlayerLastTime = Mathf.Infinity;        // 마지막으로 플레이어가 탐지된 시각 

    [SerializeField]
    float _dwellTime = 2.0f;                            // 지정필요 : 웨이포인트 도착 후 대기시간
    float _arrivedAtWaypoint = Mathf.Infinity;

    [SerializeField, Range(0, 1)]
    float _patrolSpeedFraction = 0.3f;                  // 지정필요 : 정찰속도(0~1f)


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

        // 플레이어 사망시 이하 실행중지
        if (_healthPoint.IsDead) return;

        if(IsinRange())
            Debug.Log(this.gameObject.name + "공격범위진입");

        //if (_attack.CanAttack(_player))
        //    Debug.Log(this.gameObject.name + "공격가능상태");

        // 공격 가능시 공격
        if (IsinRange() && _attack.CanAttack(_player))
        {
            Debug.Log(this.gameObject.name + "공격 가능");
            Attack();
        }
        // 공격 불가능하면 대기시간 동안 정지
        else if (_foundPlayerLastTime < _waitTime)
        {
            _actionManager.StopAction();
            //Debug.Log("Wait" + _foundPlayerLastTime);
        }
        // 공격 불가능하면 대기시간 이후 정찰
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
            // 출발시간 초기화 및 다음 웨이포인트 할당
            if (IsNearWaypoint() == true)
            {
                _arrivedAtWaypoint = 0.0f;

                _currWaypointIndex = _wayPoint.GetNextIndex(_currWaypointIndex);

                // 정찰 속도 랜덤
                //_patrolSpeedFraction = Random.Range(0.2f, 1.0f);

                // 정찰 속도 일정(변수 선언시 설정됨)
                //_patrolSpeedFraction = 0.3f;

            }

            next = GetCurrentWaypoint();
        }
        // 도착 후 대기
        if (_arrivedAtWaypoint > _dwellTime)
        {
            //다음 지점으로 정찰 실행
            _movement.Begin(next, _patrolSpeedFraction);
        }
    }

    // 객체와 웨이포인트 간의 거리가 _waypointDistance보다 좁으면 true값 반환
    private bool IsNearWaypoint()
    {
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 waypoint = new Vector2(GetCurrentWaypoint().x, GetCurrentWaypoint().z);

        return Vector2.Distance(point, waypoint) < _waypointDistance;
    }

    // 현재 웨이포인트 위치값 반환
    private Vector3 GetCurrentWaypoint()
    {
        return _wayPoint.GetWaypoint(_currWaypointIndex);
    }

    // 탐지 범위 내에 타겟(플레이어)가 있다면 true값 반환
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

    // 최초 위치로 복귀
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

    // 선택되면 구형으로 탐지범위 기즈모 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _searchRange);
    }
}
