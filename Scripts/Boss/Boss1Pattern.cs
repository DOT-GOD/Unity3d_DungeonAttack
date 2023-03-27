using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_BossState
{
    Role,
    Pattern1,
    Pattern2,
    Pattern3,
    Pattern4,
    End,
}

[System.Serializable]
public class patternGroup
{
    public GameObject[] _patternPosition = null;
}
[System.Serializable]
public class patternNumber
{
    public patternGroup[] _patternGroup = null;
}

public class Boss1Pattern : MonoBehaviour
{
    [SerializeField]
    public ENUM_BossState _state;

    public GameObject _player = null;
    public GameObject[] bossAttackArea;

    [SerializeField]
    public patternNumber[] _patternNumber = null;

    public bool isStart = false;
    public bool isPatternEnd = false;

    private float currentTime = 0;
    private float patternTime = 0;

    private int patternToggle = 0;
    private int patternCount = 0;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (!isStart) return;

        currentTime += Time.deltaTime;
        patternTime += Time.deltaTime;

        switch (_state)
        {
            case ENUM_BossState.Role:
                Debug.Log("보스 패턴 시작");
                int tempInt = Random.Range(1, 4);      // 랜덤패턴
                //int tempInt = 4;
                _state = (ENUM_BossState)tempInt;
                currentTime = 0;
                patternTime = 0;
                patternToggle = 0;
                patternCount = 0;
                break;

            /////////////////////////////////////////////////////////////////////
            case ENUM_BossState.Pattern1:
                Debug.Log("보스 패턴1");
                if (patternTime > 2.0f)
                {
                    areaAttack(1,1,patternToggle);
                    if (patternToggle == 0)
                        patternToggle = 1;
                    else
                        patternToggle = 0;
                    patternTime = 0;
                }
                if (currentTime > 10.0f)
                {
                    _state = ENUM_BossState.End;
                    currentTime = 0;
                }
                break;

            /////////////////////////////////////////////////////////////////////
            case ENUM_BossState.Pattern2:
                Debug.Log("보스 패턴2");
                if (patternTime > 0.5f && patternCount < 8)
                {
                    playerAttack();
                    patternTime = 0;
                    patternCount++;
                }
                if (currentTime > 2.0f && patternToggle == 0)
                {
                    areaAttack(2, 2, 0);
                    areaAttack(1, 2, 1);
                    patternToggle = 1;
                }
                if (currentTime > 6.0f && patternToggle == 1)
                {
                    _state = ENUM_BossState.End;
                    currentTime = 0;
                }
                break;

            /////////////////////////////////////////////////////////////////////
            case ENUM_BossState.Pattern3:
                Debug.Log("보스 패턴3");
                if (patternTime > 1.0f && patternCount < 8)
                {
                    if (patternToggle == 0)
                        areaAttack(1, 3, patternCount);
                    else
                        areaAttack(1, 3, 7 - patternCount);

                    patternTime = 0;
                    patternCount++;
                }
                if (patternCount >= 8)
                {
                    patternCount = 0;
                    patternToggle = 1;
                }
                if (currentTime > 17.0f)
                {
                    _state = ENUM_BossState.End;
                    currentTime = 0;
                }
                break;

            /////////////////////////////////////////////////////////////////////
            case ENUM_BossState.Pattern4:
                Debug.Log("보스 패턴4");
                if (patternTime > 1.0f && patternCount < 10)
                {
                    bool tempBool;
                    if (patternCount % 2 == 0)
                        tempBool = false;
                    else
                        tempBool = true;

                    ShootFire(3, 4, patternToggle, tempBool);
                    if (patternToggle == 0)
                        patternToggle = 1;
                    else
                        patternToggle = 0;

                    patternTime = 0;
                    patternCount++;
                }
                if (currentTime > 11.0f)
                {
                    _state = ENUM_BossState.End;
                    currentTime = 0;
                }
                break;

            /////////////////////////////////////////////////////////////////////
            case ENUM_BossState.End:
                if (currentTime > 8.0f)
                {
                    _state = ENUM_BossState.Role;
                    currentTime = 0;
                }
                break;

            default:
                break;
        }
    }

    private void areaAttack(int areaNum, int num, int group)
    {
        for (int i = 0; i < _patternNumber[num - 1]._patternGroup[group]._patternPosition.Length; i++)
        {
            GameObject tempArea;
            tempArea = GameObject.Instantiate(bossAttackArea[areaNum-1], _patternNumber[num-1]._patternGroup[group]._patternPosition[i].transform.position, Quaternion.identity);
            tempArea.transform.rotation = this.transform.rotation;
        }
    }

    private void playerAttack()
    {
        GameObject tempArea;
        tempArea = GameObject.Instantiate(bossAttackArea[0],
            new Vector3(_player.transform.position.x, _player.transform.position.y - 1, _player.transform.position.z),
            Quaternion.identity);
        //tempArea = GameObject.Instantiate(bossAttackArea[0], _player.transform.position, Quaternion.identity);
        //tempArea.transform.localPosition.Set(tempArea.transform.position.x, 15.02f, tempArea.transform.position.z);   //생성 후 위치변경안됨
        tempArea.transform.rotation = this.transform.rotation;
    }


    public void ShootFire(int areaNum, int num, int group, bool isToPlayer)
    {
        for (int i = 0; i < _patternNumber[num - 1]._patternGroup[group]._patternPosition.Length; i++)
        {
            GameObject tempShooter;
            tempShooter = GameObject.Instantiate(bossAttackArea[areaNum - 1], _patternNumber[num - 1]._patternGroup[group]._patternPosition[i].transform.position, Quaternion.identity);
            if(isToPlayer)
                tempShooter.transform.LookAt(_player.transform);
            else
                tempShooter.transform.rotation = this.transform.rotation;
        }

    }


}
