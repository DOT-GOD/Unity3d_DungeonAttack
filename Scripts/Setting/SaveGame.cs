using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public GameObject _player;          //Player태그 자동할당

    [SerializeField]
    [Range(0,3)]
    public int _checkPointNum;          //지정필요 : 체크포인트 위치 구분용 번호

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // 트리거에 플레이어 입장시 세이브파일 저장
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        //JsonManager _json = new JsonManager();
        JsonManager _json = this.gameObject.AddComponent<JsonManager>();
        _json.Save(_checkPointNum);
    }


}
