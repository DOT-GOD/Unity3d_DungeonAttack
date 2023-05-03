using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SaveData
{
    public int playerHp;
    public int CheckPoint;

    //데이터 저장 양식
    public SaveData(int hp, int check)
    {
        playerHp = hp;
        CheckPoint = check;
    }
}


public class JsonManager : MonoBehaviour
{
    public GameObject _player = null;

    //public GameObject _playerPrefab;
    public GameObject[] _playerSpawnPoint;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Save(int num)
    //저장 경로 : C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]
    {
        //Debug.Log("저장하기");

        _player = GameObject.FindWithTag("Player");

        int tempHp = 50;
        int checkPoint = num;
        SaveData saveData = new SaveData(tempHp, checkPoint);

        // 세이브데이터를 문자로 전환
        JsonData saveGameJson = JsonMapper.ToJson(saveData);

        //dataPath asset폴더에 저장(읽기전용)
        /*
        File.WriteAllText(Application.dataPath
            + "/Save/gameData.json"
            , saveGameJson.ToString());
        persistentDataPath appdata에 저장(안드로이드)(읽기쓰기 둘 다 가능)
        File.WriteAllText(Application.persistentDataPath
            + "/Save/gameData.json"
            , saveGameJson.ToString());
        */

        //Appdata폴더에 저장(읽기쓰기 둘 다 가능)
        //폴더생성
        if (!Directory.Exists(Application.persistentDataPath + "/Save"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Save");

        File.WriteAllText(Application.persistentDataPath
            + "/Save/gameData.json"
            , saveGameJson.ToString());

        Debug.Log("Game Saved");
    }

    public void Load()
    {
        //Debug.Log("불러오기");

        //dataPath asset폴더에 저장
        //string JsonString = File.ReadAllText(Application.dataPath
        //                                        + "/Save/gameData.json");

        //persistentDataPath appdata에 저장(안드로이드)
        string JsonString = File.ReadAllText(Application.persistentDataPath
            + "/Save/gameData.json");

        Debug.Log(JsonString);

        // 문자를 세이브데이터로 전환
        JsonData gameData = JsonMapper.ToObject(JsonString);

        // json파일의 string을 int로 변환해서 저장

        string tempString1 = gameData["playerHp"].ToString();
        string tempString2 = gameData["CheckPoint"].ToString();

        int tempHp = int.Parse(tempString1);
        int checkPoint = int.Parse(tempString2);
        SaveData saveData = new SaveData(tempHp, checkPoint);

        // 플레이어 오브젝트를 다시 생성하는 방식
        //GameObject tempPlayer;
        //tempPlayer = GameObject.Instantiate(_playerPrefab, _playerSpawnPoint[checkPoint].transform.position, Quaternion.identity);
        //tempPlayer.transform.rotation = _playerSpawnPoint[checkPoint].transform.rotation;

        // 비활성화된 플레이어 오브젝트 활성화하는 방식
        if (_player == null)
            _player = GameObject.FindWithTag("Player");
        _player.gameObject.SetActive(false);

        HealthPoint _hp;
        _hp = _player.GetComponent<HealthPoint>();
        _hp.PlayerSpawn(tempHp);

        _player.transform.SetPositionAndRotation(
            _playerSpawnPoint[checkPoint].transform.position,
            Quaternion.Euler(_playerSpawnPoint[checkPoint].transform.rotation.eulerAngles));
        //_player.transform.position = _playerSpawnPoint[checkPoint].transform.position;
        //_player.transform.eulerAngles
        //    = new Vector3(_playerSpawnPoint[checkPoint].transform.eulerAngles.x,
        //    _playerSpawnPoint[checkPoint].transform.eulerAngles.y,
        //    _playerSpawnPoint[checkPoint].transform.eulerAngles.z);

        _player.gameObject.SetActive(true);


    }

    //세이브지점을 0번으로
    public void ResetGame()
    {
        Save(0);
    }

}
