using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public VariableJoystick varibaleJoystick = null;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    public float _moveSpeed = 10.0f;

    [SerializeField]
    public bool _canDash = true;

    [SerializeField]
    [Range(1.0f, 10.0f)]
    public float _dashDelay = 3.0f;

    CharacterController _cc = null;

    float _gravity = -20.0f;
    float _yVelocity = 0;

    public float _jumpPower = 10.0f;

    bool _isJumping = false;

    private float _currentTime = 0.0f;

    private bool _isDashing = false;
    private float _dashSpeed = 5.0f;
    private float _lastDashTime = 0.0f;
    private float _dashTime = 0.2f;

    public GameObject _footstep;

    public bool _usingJoystick = true;


    void Start()
    {
        _cc = this.GetComponent<CharacterController>();
        
        GameObject _tempStick = GameObject.FindWithTag("moveStick");
        varibaleJoystick = _tempStick.GetComponent<VariableJoystick>();
    }

    void Update()
    {
        _currentTime += Time.deltaTime;



#if UNITY_ANDROID
        //Vector3 dir = new Vector3(varibaleJoystick.Horizontal, 0, varibaleJoystick.Vertical);
        float h;
        float v;

        //조이스틱 사용여부
        if (_usingJoystick)
        {
            h = varibaleJoystick.Horizontal;
            v = varibaleJoystick.Vertical;
        }
        else
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        Vector3 dir = new Vector3(h, 0, v);

        //발소리
        Vector3 foot = new Vector3(0, 0, 0);
        if (dir == foot)
            _footstep.SetActive(false);
        else
            _footstep.SetActive(true);


#elif UNITY_EDITOR || UNITY_STANDALONE
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //Vector3 dir = new Vector3(h, 0, v);

#endif

        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        if (_cc.collisionFlags == CollisionFlags.Below)
        {
            if (_isJumping)
            {
                _isJumping = false;
            }

            _yVelocity = 0;
        }


#if UNITY_ANDROID
        // 점프
        if (Input.GetButtonDown("Jump") && !_isJumping)
        {
            _yVelocity = _jumpPower;
        }
        //(점프 없을시에도 중력구현을 위해 필요)
        _yVelocity += _gravity * Time.deltaTime;
        dir.y = _yVelocity;


        // 대쉬(개발자테스트용 버튼)
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
        }
        */
#elif UNITY_EDITOR || UNITY_STANDALONE
        // 점프
                /*
        if (Input.GetButtonDown("Jump") && !_isJumping)
        {
            _yVelocity = _jumpPower;
        }
        //(점프 없을시에도 중력구현을 위해 필요)
        _yVelocity += _gravity * Time.deltaTime;
        dir.y = _yVelocity;
                */
        // 대쉬
        if(Input.GetKeyDown(KeyCode.E))
        {
            Dash();
        }
#endif

        if (_isDashing == true && _dashTime > _currentTime - _lastDashTime)
        {
            _cc.Move(dir * _dashSpeed / _dashTime * Time.deltaTime);
            //this.transform.Translate(Vector3.forward * Time.deltaTime * _dashSpeed / _dashTime);
        }
        if (_isDashing == true && _dashTime < _currentTime - _lastDashTime)
        {
            _isDashing = false;
        }

        _cc.Move(dir * _moveSpeed * Time.deltaTime);

    }

    public void Dash()
    {
        // 마지막 대쉬 사용했을 때부터 딜레이보다 많은 시간이 지났을 때
        if (_dashDelay < _currentTime - _lastDashTime)
        {
            _isDashing = true;
            _lastDashTime = _currentTime;   //마지막 사용 갱신
        }
    }
}
