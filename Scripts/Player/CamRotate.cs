using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;

    public float _rotSpeed = 500.0f;

    float _mx = 0;
    float _my = 0;

    void Start()
    {

    }

    void Update()
    {
        //if (GameManager.gm._gState != GameManager.GameState.Run) return;

        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

#if UNITY_ANDROID
        _mx += dynamicJoystick.Horizontal * _rotSpeed * Time.deltaTime;
        _my += dynamicJoystick.Vertical * _rotSpeed * Time.deltaTime;
#elif UNITY_EDITOR || UNITY_STANDALONE
        _mx += mouse_X * _rotSpeed * Time.deltaTime;
        _my += mouse_Y * _rotSpeed * Time.deltaTime;
#endif

        _my = Mathf.Clamp(_my, -90.0f, 90.0f);

        this.transform.eulerAngles = new Vector3(-_my, _mx, 0);
    }
}
