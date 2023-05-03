using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    public float _value = 50;                               // 지정필요 : 체력 수치

    public GameObject _model = null;                        // 지정필요 : 모델오브젝트

    Animator _animator = null;                              // 가지고 있는 컴포넌트 자동할당
    ActionManager _actionManager = null;                    // 가지고 있는 컴포넌트 자동할당

    bool _isDead = false;

    public bool IsDead { get { return _isDead; } }

    SkinnedMeshRenderer[] _renderers = null;                // 자식이 가지고 있는 컴포넌트 자동할당

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

        // 피격시 깜박거리는 효과
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

    // 마테리얼 색 복구(피격시 깜박거리는 효과용)
    private void ResetColor()
    {
        foreach (SkinnedMeshRenderer render in _renderers)
        {
            render.material.color = Color.white;
        }
    }

    // 논 플레이어 사망시 사망 애니메이션 후 객체 파괴
    private void Dead()
    {
        if (_isDead == true) return;

        _isDead = true;

        if (_animator != null)
            _animator.SetTrigger("Dead");

        if (_actionManager != null)
            _actionManager.StopAction();

        if (this.tag == "Player")
            PlayerDead();
        else
            Invoke("DestroySelf",3);
    }

    // 플레이어 사망시 객체 파괴 대신 일시 비활성화
    private void PlayerDead()
    {
        this.gameObject.SetActive(false);
        //_model.SetActive(false);
        Debug.Log("플레이어 사망");
    }

    // 플레이어 스폰시 체력 및 사망상태 초기화 후 객체 활성화
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
