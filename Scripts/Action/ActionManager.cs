using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    IEnd _curr = null;

    Animator _animator = null;

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
