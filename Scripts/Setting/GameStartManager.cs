using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    private GameObject _jsonManager;

    public GameObject _player;

    public GameObject _restartMenu;

    public GameObject _bull;
    public GameObject _vampire;
    public GameObject _skeleton;

    public GameObject _spawnPointBoss1;
    public GameObject _spawnPointMiddleBoss;
    public GameObject[] _spawnPointGroup;

    private GameObject[] _monsterGroup = null;
    private GameObject[] _monsterSpawned = null;


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

    void MonsterRespawn()
    {
        //_monsterGroup = null;
        if (_monsterGroup == null)
            _monsterGroup = GameObject.FindGameObjectsWithTag("SpawnPoint");

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

        //보스가 살아있을 때만 작동
        //if (_bull != null)
        //{
        //    if (_bull.GetComponent<HealthPoint>()._value > 0)
        //    {
        //        _bull.GetComponent<HealthPoint>()._value = 100;
        //        _bull.GetComponent<Boss1Pattern>().isStart = false;
        //    }
        //}

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

        //중간보스가 살아있을 때만 작동
        //if (_vampire != null)
        //{
        //    if (_vampire.GetComponent<HealthPoint>()._value > 0)
        //    {
        //        _vampire.GetComponent<HealthPoint>()._value = 50;
        //        _vampire.transform.position = _spawnPointMiddleBoss.transform.position;
        //    }
        //}

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
