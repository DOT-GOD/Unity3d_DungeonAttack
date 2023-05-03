using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IEnd
{

    TrailRenderer _trail = null;

    [SerializeField]
    [Range(1.0f, 30.0f)]
    float _attackRange = 1.0f;                      // 지정필요 : 자동공격 가능 범위

    [SerializeField]
    [Range(1.0f, 20.0f)]
    float _attackDelay = 2.0f;                      // 지정필요 : 자동공격 지연시간

    [SerializeField]
    [Range(1.0f, 20.0f)]
    float _attackDamage = 5.0f;
     
    float _sinceLastAttack = 0.0f;                  // 지정필요 : 자동공격 피해량

    HealthPoint _target = null;                     // 자동할당
    Movement _movement = null;                      // 자동할당(가지고 있는 컴포넌트)
    ActionManager _actionManager = null;            // 자동할당(가지고 있는 컴포넌트)
    Animator _animator = null;                      // 자동할당(가지고 있는 컴포넌트)
    HealthPoint _healthPoint = null;                // 자동할당(가지고 있는 컴포넌트)


    private void Awake()
    {
        _movement = this.GetComponent<Movement>();
        _actionManager = this.GetComponent<ActionManager>();
        _animator = this.GetComponent<Animator>();
        _healthPoint = this.GetComponent<HealthPoint>();
    }

    void Start()
    {
        // 처음에는 공격 딜레이가 걸리지 않게 설정
        _sinceLastAttack = _attackDelay;
    }


    void Update()
    {

        _sinceLastAttack += Time.deltaTime;

        // 타겟이 없을 경우 이하 실행중지
        if (_target == null) return;
        
        // 타겟이 사망시 어택 애니메이션 중지(트리거 파라미터 리셋)
        if (_target.IsDead == true)
        {
            _animator.ResetTrigger("Attack");
            return;
        }

        // 객체 사망시 이하 실행 중지
        if (_healthPoint.IsDead) return;


        // 공격 범위 밖이면 이동
        if (IsinRange() == false)
        {
            _movement.To(_target.transform.position, 1.0f);
        }
        // 공격 범위 안이면 이동 정지 및 공격
        else
        {
            PlayAnimation();

            _movement.End();
        }
    }

    // 타겟이 체력이 존재하면서 사망하지 않았을 때 true값 반환
    public bool CanAttack(GameObject target)
    {
        if (target == null)
        {
            Debug.Log("공격대상 존재하지않음");
            return false;
        }
        HealthPoint hp = target.GetComponent<HealthPoint>();

        return hp != null && hp.IsDead == false;
    }

    public void Begin(GameObject target)
    {

        _actionManager.StartAction(this);
        _target = target.GetComponent<HealthPoint>();

    }

    public void End()
    {
        EndTrigger();

        _target = null;

    }

    private void EndTrigger()
    {
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("StopAttack");
    }

    // 객체와 타겟간 거리가 공격 범위 이내라면 true값 반환
    private bool IsinRange()
    {
        Vector2 targetPoint = new Vector2(_target.transform.position.x, _target.transform.position.z);
        Vector2 point = new Vector2(this.transform.position.x, this.transform.position.z);

        return Vector2.Distance(targetPoint, point) < _attackRange;
    }

    private void Hit()
    {
        Debug.Log("공격이벤트");
        if (_target == null) return;

        Debug.Log("타겟있음");
        if (IsinRange() == false) return;

        Debug.Log("타겟데미지");
        _target.Damage(_attackDamage);

        //Vector3 dirToTarget = this.transform.position - _target.transform.position;
        //Vector3 look = Vector3.Slerp(_target.transform.forward, dirToTarget.normalized, Time.deltaTime * 1.0f);
        //_target.transform.rotation = Quaternion.LookRotation(look, Vector3.up);

    }

    private void PlayAnimation()
    {
        //Transform temp = null;
        //temp.Translate(_target.transform.position.x, this.transform.position.y, _target.transform.position.z);


        //this.transform.LookAt(temp.transform);


        // 플레이어가 공격대상을 바라보게 함
        if (_sinceLastAttack < 1)
            this.transform.LookAt(_target.transform);

        // 공격 딜레이가 끝나기 전이면 이하 실행중지
        if (_sinceLastAttack < _attackDelay) return;

        PlayTrigger();

        _sinceLastAttack = 0.0f;
    }

    // 공격 트리거 활성화
    private void PlayTrigger()
    {
        _animator.ResetTrigger("StopAttack");
        _animator.SetTrigger("Attack");
    }

    private void AttackStart()
    {
        if (_trail == null) return;
        _trail.enabled = true;
    }

    private void AttackEnd()
    {
        if (_trail == null) return;
        _trail.enabled = false;
    }
}
