using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    public float _value = 50;

    public GameObject _model = null;

    Animator _animator = null;
    ActionManager _actionManager = null;

    bool _isDead = false;

    public bool IsDead { get { return _isDead; } }

    SkinnedMeshRenderer[] _renderers = null;

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _actionManager = this.GetComponent<ActionManager>();

        _renderers = this.GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void Damage(float damage)
    {
        // 플레이어와 공격 대상이 서로 바라보게 함
        //this.transform.LookAt(trans);

        // 입력된 수 중 가장 큰 수 반환
        _value = Mathf.Max(_value - damage, 0);
        Debug.Log("잔여체력 : " + _value);

        foreach (SkinnedMeshRenderer render in _renderers)
        {
            render.material.color = new Color(8, 0, 0);

            Invoke("ResetColor", 0.2f);
        }

        if (_value <= 0.0f)
        {
            Dead();
        }
    }

    private void ResetColor()
    {
        foreach (SkinnedMeshRenderer render in _renderers)
        {
            render.material.color = Color.white;
        }
    }
    private void Dead()
    {
        if (_isDead == true) return;

        _isDead = true;

        if (_animator != null)      _animator.SetTrigger("Dead");

        if (_actionManager != null) _actionManager.StopAction();

        if (this.tag == "Player")
            PlayerDead();
        else
            Invoke("DestroySelf",3);
    }

    private void PlayerDead()
    {
        this.gameObject.SetActive(false);
        //_model.SetActive(false);
        Debug.Log("플레이어 사망");
    }

    public void PlayerSpawn(int num)
    {
        _value = num;
        _isDead = false;

        this.gameObject.SetActive(true);
        //_model.SetActive(true);
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
