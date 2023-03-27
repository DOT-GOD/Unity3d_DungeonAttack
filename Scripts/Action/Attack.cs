using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IEnd
{

    TrailRenderer _trail = null;

    [SerializeField]
    [Range(1.0f, 30.0f)]
    float _attackRange = 1.0f;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    float _attackDelay = 2.0f;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    float _attackDamage = 5.0f;

    float _sinceLastAttack = 0.0f;

    HealthPoint _target = null;
    Movement _movement = null;
    ActionManager _actionManager = null;
    Animator _animator = null;
    HealthPoint _healthPoint = null;


    private void Awake()
    {
        _movement = this.GetComponent<Movement>();
        _actionManager = this.GetComponent<ActionManager>();
        _animator = this.GetComponent<Animator>();
        _healthPoint = this.GetComponent<HealthPoint>();
    }

    void Start()
    {
        _sinceLastAttack = _attackDelay;
    }


    void Update()
    {

        _sinceLastAttack += Time.deltaTime;

        if (_target == null) return;

        if (_target.IsDead == true)
        {
            _animator.ResetTrigger("Attack");
            return;
        }

        if (_healthPoint.IsDead) return;

        if (IsinRange() == false)
        {
            _movement.To(_target.transform.position, 1.0f);
        }
        else
        {
            PlayAnimation();

            _movement.End();
        }
    }

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

        if (_sinceLastAttack < _attackDelay) return;

        PlayTrigger();

        _sinceLastAttack = 0.0f;
    }

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
