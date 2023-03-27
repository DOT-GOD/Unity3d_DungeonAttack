using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject _player = null;
    public GameObject _destination;
    [SerializeField]
    public bool _tpButton = false;


    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //수동 텔레포트
        if (_tpButton)
        {
            _tpButton = false;
            _player.SetActive(false);
            _player.transform.SetPositionAndRotation(
                _destination.transform.position,
                _destination.transform.rotation);
            _player.SetActive(true);
            Debug.Log("텔레포트");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        _player.SetActive(false);
        _player.transform.SetPositionAndRotation(
            _destination.transform.position,
            _destination.transform.rotation);
        _player.SetActive(true);


        Debug.Log("텔레포트");
    }
}
