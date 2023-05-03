using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    private GameObject _jsonManager;                         //jsonManager자동할당

    public GameObject _player;                               //Player태그로 자동할당

    public GameObject _restartMenu;                          //지정필요 : 재시작버튼

    public GameObject _bull;                                 //지정필요 : 보스 프리팹
    public GameObject _vampire;                              //지정필요 : 중간보스 프리팹
    public GameObject _skeleton;                             //지정필요 : 해골 프리팹

    public GameObject _spawnPointBoss1;                      //지정필요 : 보스 스폰포인트
    public GameObject _spawnPointMiddleBoss;                 //지정필요 : 중간보스 스폰포인트
    public GameObject[] _spawnPointGroup;                    //몬스터 SpawnPoint 태그 자동할당

    //private GameObject[] _monsterGroup = null;               
    private GameObject[] _monsterSpawned = null;             //스폰된 몬스터


    private JsonManager tempJson;
    private HealthPoint _playerHp;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _jsonManager = GameObject.FindGameObjectWithTag("JsonManager");
        _spawnPointGroup = GameObject.FindGameObjectsWithTag("SpawnPoint");

        //JsonManager tempJson;
        tempJson = _jsonManager.GetComponent<JsonManager>();
        //tempJson.Load();
        //MonsterRespawn();
        RestartGame();
    }

    void Update()
    {
        _playerHp = _player.GetComponent<HealthPoint>();
        if (_playerHp._value <= 0)
        {
#if UNITY_ANDROID
            _restartMenu.SetActive(true);
#elif UNITY_EDITOR || UNITY_STANDALONE
            RestartGame();
#endif
        }
    }

    private void MonsterRespawn()
    {
        ////_monsterGroup = null;
        //if (_monsterGroup == null)
        //    _monsterGroup = GameObject.FindGameObjectsWithTag("SpawnPoint");

        SkeletonRespawn();
        BossRespawn();
        MiddleBossRespawn();
    }

    private void SkeletonRespawn()
    {
        //이미 소환된 몬스터 제거
        _monsterSpawned = GameObject.FindGameObjectsWithTag("Monster");

        for (int i = 0; i < _monsterSpawned.Length; i++)
        {
            Destroy(_monsterSpawned[i]);
        }

        //몬스터 스폰
        for (int i = 0; i < _spawnPointGroup.Length; i++)
        {
            GameObject tempMonster;
            tempMonster = GameObject.Instantiate(_skeleton, _spawnPointGroup[i].transform.position, Quaternion.identity);
            tempMonster.transform.rotation = _spawnPointGroup[i].transform.rotation;
        }
    }
    private void BossRespawn()
    {
        //보스 스폰
        GameObject tempBoss1;
        tempBoss1 = GameObject.Instantiate(_bull, _spawnPointBoss1.transform.position, Quaternion.identity);
        tempBoss1.SetActive(false);
        tempBoss1.transform.eulerAngles
            = new Vector3(_spawnPointBoss1.transform.eulerAngles.x,
            _spawnPointBoss1.transform.eulerAngles.y,
            _spawnPointBoss1.transform.eulerAngles.z);
        _bull.GetComponent<HealthPoint>()._value = 100;
        _bull.GetComponent<Boss1Pattern>().isStart = false;
        tempBoss1.SetActive(true);
    }
    private void MiddleBossRespawn()
    {

        //중간보스 스폰
        GameObject tempMiddleBoss;
        tempMiddleBoss = GameObject.Instantiate(_vampire, _spawnPointMiddleBoss.transform.position, Quaternion.identity);
        tempMiddleBoss.SetActive(false);
        tempMiddleBoss.transform.eulerAngles
            = new Vector3(_spawnPointMiddleBoss.transform.eulerAngles.x,
            _spawnPointMiddleBoss.transform.eulerAngles.y,
            _spawnPointMiddleBoss.transform.eulerAngles.z);
        tempMiddleBoss.SetActive(true);
    }

    public void RestartGame()
    {
        tempJson.Load();
        MonsterRespawn();

        _restartMenu.SetActive(false);
    }
}
