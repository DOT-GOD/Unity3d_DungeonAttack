using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick = null;      //조이스틱 자동할당

    public float _rotSpeed = 200.0f;                    //지정필요 : 플레이어회전속도

    float _mx = 0;

    void Start()
    {
        GameObject tempStick = GameObject.FindWithTag("viewStick");
        dynamicJoystick = tempStick.GetComponent<DynamicJoystick>();
    }

    void Update()
    {
#if UNITY_ANDROID
        _mx += dynamicJoystick.Horizontal * _rotSpeed * Time.deltaTime;

#elif UNITY_EDITOR || UNITY_STANDALONE
        float mouse_X = Input.GetAxis("Mouse X");
        _mx += mouse_X * _rotSpeed * Time.deltaTime;
#endif

        this.transform.eulerAngles = new Vector3(0, _mx, 0);
    }
}
