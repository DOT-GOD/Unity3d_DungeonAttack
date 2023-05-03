using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public GameObject[] _bgmList;                //지정필요 : BGM 사운드
    public int _bgmNum = 0;                      //재생 요청된 BGM
    private int _curNum = 0;                     //현재 재생중인 BGM

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
