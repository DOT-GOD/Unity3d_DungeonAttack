using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject _object;
    public HealthPoint _hp;
    public Image _hpBar;

    public float _maxHp = 0;
    public float _curHp = 0;

    void Start()
    {
        _hp = _object.GetComponent<HealthPoint>();
        _maxHp = _hp._value;

    }

    void Update()
    {
        //현재 hp값 받아오기
        _curHp = _hp._value;

        //hp비율에 따라 체력바 길이 조정
        _hpBar.rectTransform.localScale = new Vector3((float)_curHp / (float)_maxHp, 1f, 1f);
    }
}
