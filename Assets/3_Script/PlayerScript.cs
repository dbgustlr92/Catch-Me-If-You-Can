using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    float y = 0.0f;
    float gravity = 0.0f;     // 중력느낌용
    int direction = 0;       // 0:정지상태, 1:점프중, 2:다운중
    // 설정값
    const float jump_speed = 0.05f;  // 점프속도
    const float jump_accell = 0.0025f; // 점프가속
    const float y_base = -0.25f;      // 캐릭터가 서있는 기준점
    bool jump2 = true;

    public int _hp = 2000;
    public GameObject _DamageEff;
    public UIFilledSprite _GuageBarWidget;  //체력 게이지
    public UIFilledSprite _PowerGauge;   //파워게이지
    public GameObject Stone_Set;
    public GameObject Fork_Set;
   
    public Transform _parent;
    public GameObject Player;
    public int _power = 0;
    public bool Powerswitch = true;

    public float AttackPower;

    public int score;
    public UILabel ScorePanel;


    void Start()
    {
        y = y_base;
        
        
    }
    
    void Update()
    {
        /////////파워게이지 자동으로 오르락 내리락 부분
        if (_power > 0 && Powerswitch)
        {
            _PowerGauge.fillAmount = _power * 0.01f;
            _power = _power - 1;
        }
        if (_power == 0)
        {
            Powerswitch = false;
        }
        if (_power < 100 && !Powerswitch)
        {
            _PowerGauge.fillAmount = _power * 0.01f;
            _power = _power + 1;
        }
        if (_power == 100)
        {
            Powerswitch = true;
        }
        //////////////////////////////////////

        if (Input.GetKeyUp(KeyCode.Q))          //q눌럿을때 포크포크
        {
            var attack_fork = Instantiate(Fork_Set, Vector3.zero, Quaternion.identity) as GameObject;
            attack_fork.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            attack_fork.transform.localPosition = new Vector3(-1.5f, Player.transform.position.y, 0);
            attack_fork.transform.parent = _parent;
            attack_fork.gameObject.GetComponent<MoveFood>().power = _power;
        }
        if (Input.GetKeyUp(KeyCode.W))        //w 눌렀을때 무기발사 (쓔레기통)
        {
            var attack_Stone = Instantiate(Stone_Set, Vector3.zero, Quaternion.identity) as GameObject;
            attack_Stone.transform.localScale = new Vector3(1.5f, 2f, 2f);
            attack_Stone.transform.localPosition = new Vector3(-2f, Player.transform.position.y, 0);
            attack_Stone.transform.parent = _parent;
            attack_Stone.gameObject.GetComponent<MoveFood>().power = _power * 5;
            
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.localScale = new Vector3(1f, 0.5f, 1f);
            ((BoxCollider)this.collider).size = new Vector3(100, 50, 200);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            ((BoxCollider)this.collider).size = new Vector3(100, 150, 200);
        }

        JumpProcess();
        if (Input.GetKeyDown(KeyCode.Space)/*Input.touchCount>0*/)
        {
            DoJump();
        }
        // y값을 gameObject에 적용하세요.
        Vector3 pos = gameObject.transform.position;
        pos.y = y;
        gameObject.transform.position = pos;

        _hp--;
        _GuageBarWidget.fillAmount = _hp * 0.0005f;

        /*var _Eff1 = Instantiate(_DamageEff, transform.localPosition, Quaternion.identity) as GameObject;
        _Eff1.transform.parent = transform;
        _Eff1.transform.localPosition = Vector3.zero;
        _Eff1.transform.localScale = new Vector3(1, 1, 1);*/

        ScorePanel.text = score.ToString();

  


       



    }
    void DoJump() // 점프키 누를때 1회만 호출
    {
        direction = 1;
        gravity = jump_speed;
    }
    void JumpProcess()
    {
        switch (direction)
        {
            case 0: // 2단 점프시 처리
            {
                if (y > y_base)
                {
                    if (y >= jump_accell)
                    {
                        //y -= jump_accell;
                        y -= gravity;
                    }
                    else
                    {
                        y = y_base;
                    }
                }
                break;
            }
            case 1: // up
            {
                y += gravity;
                if (gravity <= 0.0f)
                {
                    direction = 2;
                }
                else
                {
                    gravity -= jump_accell;
                }
                break;
            }
 
            case 2: // down
            {
                y -= gravity;
                if (y > y_base)
                {
                    gravity += jump_accell;
                }
                else
                {
                    direction = 0;
                    y = y_base;
                }
                break;
            }
        }
 
    }

    void OnTriggerEnter(Collider other) {

        
        if(other.gameObject.name.Equals("비빔밥")){
            Debug.Log(other.gameObject.name);
            Destroy(other.gameObject);  // 객체제거
            score = score + 100;
        }
        //animation.Play("1_damage");
        
    }
}

