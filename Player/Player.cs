using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public VariableJoystick varibaleJoystick = null;        //지정필요 : 조이스틱 프리팹       

    [SerializeField]
    [Range(1.0f, 20.0f)]
    public float _moveSpeed = 10.0f;                        //지정필요 : 플레이어 이동속도

    [SerializeField]
    public bool _canDash = true;                            //대쉬 사용가능 여부 판별

    [SerializeField]
    [Range(1.0f, 10.0f)]
    public float _dashDelay = 3.0f;                         //지정필요 : 대쉬쿨타임

    CharacterController _cc = null;                         //가지고있는 컴포넌트 자동할당

    float _gravity = -20.0f;                                //지정필요 : 낙하시 중력
    float _yVelocity = 0;                                   //중력구현용 변수

    public float _jumpPower = 10.0f;                        //지정필요 : 점프력

    bool _isJumping = false;                                //점프중인지 판별

    private float _currentTime = 0.0f;                      //시간경과 체크용

    private bool _isDashing = false;                        //대쉬중인지 판별
    private float _dashSpeed = 5.0f;                        //지정필요 : 대쉬 속도
    private float _lastDashTime = 0.0f;                     //마지막으로 대쉬한 시각
    private float _dashTime = 0.2f;                         //지정필요 : 대쉬지속시간

    public GameObject _footstep;                            //지정필요 : 발소리 프리팹

    public bool _usingJoystick = true;                      //지정필요 : 조이스틱 사용여부(비활성화시 모바일에서도 키입력 이동)


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

        //조이스틱 사용여부 판별
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

        //발소리(이동중일때만 활성화)
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

        //대쉬중일때
        if(_isDashing)
        {
            //_dashTime동안 이동속도가 _dashSpeed로 변경되는 식으로 구현
            if (_dashTime > _currentTime - _lastDashTime)
            {
                _cc.Move(dir * _dashSpeed / _dashTime * Time.deltaTime);
                //this.transform.Translate(Vector3.forward * Time.deltaTime * _dashSpeed / _dashTime);
            }
            if (_dashTime < _currentTime - _lastDashTime)
            {
                _isDashing = false;
            }
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
