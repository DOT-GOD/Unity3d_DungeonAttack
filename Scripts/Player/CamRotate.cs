using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;              //지정필요 : 조이스틱 프리팹

    public float _rotSpeed = 500.0f;                     //지정필요 : 카메라회전속도

    private float _mx = 0;
    private float _my = 0;

    void Start()
    {

    }

    void Update()
    {
        //if (GameManager.gm._gState != GameManager.GameState.Run) return;

        // 안드로이드는 조이스틱, PC에서는 마우스로 카메라 회전
#if UNITY_ANDROID
        _mx += dynamicJoystick.Horizontal * _rotSpeed * Time.deltaTime;
        _my += dynamicJoystick.Vertical * _rotSpeed * Time.deltaTime;
#elif UNITY_EDITOR || UNITY_STANDALONE
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");
        _mx += mouse_X * _rotSpeed * Time.deltaTime;
        _my += mouse_Y * _rotSpeed * Time.deltaTime;
#endif

        _my = Mathf.Clamp(_my, -90.0f, 90.0f);

        this.transform.eulerAngles = new Vector3(-_my, _mx, 0);
    }
}
