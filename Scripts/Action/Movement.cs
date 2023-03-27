using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour, IEnd
{
    [SerializeField]
    float _maxspeed = 5.0f;

    [SerializeField]
    NavMeshAgent _agent;

    Ray _ray;

    Animator _animator;

    ActionManager _actionManager = null;
    HealthPoint _healthPoint = null;

    private void Awake()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        _animator = this.GetComponent<Animator>();
        _actionManager = this.GetComponent<ActionManager>();
        _healthPoint = this.GetComponent<HealthPoint>();
    }

    
    private void Update()
    {
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
        Vector3 velocity = _agent.velocity;
        Vector3 local = this.transform.InverseTransformDirection(velocity);

        _animator.SetFloat("MoveSpeed", local.z);
    }

    public void Begin(Vector3 dest, float speedFraction)
    {
        _actionManager.StartAction(this);

        To(dest, speedFraction);
    }

    public void To(Vector3 dest, float speedFraction)
    {
        if (_agent != null && _agent.enabled)
        {
            _agent.destination = dest;
            _agent.speed = _maxspeed * Mathf.Clamp01(speedFraction);
            _agent.isStopped = false;
        }
    }

    public void End()
    {
        if (_agent != null && _agent.enabled)
        {
            _agent.isStopped = true;
            //_agent.destination = _agent.transform.position;
        }
    }
}
