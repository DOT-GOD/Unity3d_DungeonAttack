using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    // 현재 액션 상태 저장
    IEnd _curr = null;

    Animator _animator = null;  // 객체에 있는 애니메이터 자동 할당

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void StartAction(IEnd action)
    {
        if (_curr == action) return;

        if (_curr != null)
        {
            _curr.End();
        }

        _curr = action;
    }

    public void StopAction()
    {
        if (_curr != null)
        {
            _curr.End();
        }
        _curr = null;
    }
}
