using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public GameObject[] _bgmList;
    public int _bgmNum = 0;
    private int _curNum = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(_curNum != _bgmNum)
        {
            for(int i = 0; i < _bgmList.Length; i++)
            {
                _bgmList[i].SetActive(false);
            }

            _bgmList[_bgmNum].SetActive(true);
        }
        _curNum = _bgmNum;
    }
}
