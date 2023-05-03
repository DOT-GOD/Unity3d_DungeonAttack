using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour, IEnd
{
    [SerializeField]
    float _maxspeed = 5.0f;

    [SerializeField]
    NavMeshAgent _agent;                           // 가지고 있는 컴포넌트 자동할당

    Ray _ray;                                      

    Animator _animator;                            // 가지고 있는 컴포넌트 자동할당
    ActionManager _actionManager = null;           // 가지고 있는 컴포넌트 자동할당
    HealthPoint _healthPoint = null;               // 가지고 있는 컴포넌트 자동할당

    private void Awake()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        _animator = this.GetComponent<Animator>();
        _actionManager = this.GetComponent<ActionManager>();
        _healthPoint = this.GetComponent<HealthPoint>();
    }

    
    private void Update()
    {
        // 사망시 내비게이션 에이전트 비활성화
        if (_agent != null)
        {
            _agent.enabled = !_healthPoint.IsDead;
        }
    }

    private void FixedUpdate()
    {
        UpdateAnimator();
    }
    private void UpdateAnimator()
    {
        // 에이전트 위치로 객체 이동
        Vector3 velocity = _agent.velocity;
        Vector3 local = this.transform.InverseTransformDirection(velocity);

        _animator.SetFloat("MoveSpeed", local.z);
    }

    // 이동 시작
    public void Begin(Vector3 dest, float speedFraction)
    {
        _actionManager.StartAction(this);

        To(dest, speedFraction);
    }

    // 목표물 및 속도 지정
    public void To(Vector3 dest, float speedFraction)
    {
        if (_agent != null && _agent.enabled)
        {
            _agent.destination = dest;
            _agent.speed = _maxspeed * Mathf.Clamp01(speedFraction);
            _agent.isStopped = false;
        }
    }

    // 정지
    public void End()
    {
        if (_agent != null && _agent.enabled)
        {
            _agent.isStopped = true;
            //_agent.destination = _agent.transform.position;
        }
    }
}
