using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    float y = 0.0f;
    float gravity = 0.0f;     // 중력느낌용
    int direction = 0;       // 0:정지상태, 1:점프중, 2:다운중
    // 설정값
    const float jump_speed = 0.05f;  // 점프속도
    const float jump_accell = 0.0025f; // 점프가속
    const float y_base = -0.11f;      // 캐릭터가 서있는 기준점
    bool jump2 = true;

    public int _hp = 2000;
    public GameObject _DamageEff;
    public UIFilledSprite _GuageBarWidget;  //체력 게이지
    public UIFilledSprite _PowerGauge;   //파워게이지
    public GameObject Garbage_Set;
    public GameObject Stone_Set;
    public GameObject Fork_Set;
   
    public Transform _parent;
    public GameObject Player;
    public int _power = 0;
    public bool Powerswitch = true;

    public float AttackPower;

    public int score;
    public UILabel ScorePanel;

    private bool isable_fork = false;
    private bool isable_garbagecan = false;
    private bool isable_stone = false;
    int ForkCount = 0;
    int GarbageCanCount = 0;
    int StoneCount = 0;

    public GameObject PowerGauge;
    bool Itemable_Fork = false;
    bool Itemable_GarbageCan = false;
    bool Itemable_Stone = false;

    
    void Start()
    {
        PowerGauge.SetActive(false);
        //y = Player.transform.localPosition.y;
        y = y_base;
        
        
    }
    
    void Update()
    {
        UISprite[] child = GameObject.Find("1_BGObj").GetComponentsInChildren<UISprite>();
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
        if (_hp == 0)
        {
            Application.LoadLevel("3");
        }

        if (Input.GetKeyDown(KeyCode.Q) && isable_fork && ForkCount > 0)
        {
            PowerGauge.SetActive(true);
            child[4].GetComponent<UISprite>().spriteName = "btn_fork_pushed";
            

        }
        if (Input.GetKeyUp(KeyCode.Q) && isable_fork && ForkCount >0)          //q눌럿을때 포크포크
        {
            isable_fork = false;
            ForkCount--;
            var attack_fork = Instantiate(Fork_Set, Vector3.zero, Quaternion.identity) as GameObject;
            attack_fork.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            attack_fork.transform.localPosition = new Vector3(-1.5f, Player.transform.position.y, 0);
            attack_fork.transform.parent = _parent;
            attack_fork.gameObject.GetComponent<MoveFood>().power = _power;
            PowerGauge.SetActive(false);
            child[4].GetComponent<UISprite>().spriteName = "btn_fork";

            if (ForkCount > 0)
                isable_fork = true;
            
        }

        if (Input.GetKeyDown(KeyCode.E) && isable_garbagecan && GarbageCanCount > 0)
        {
            PowerGauge.SetActive(true);
            child[6].GetComponent<UISprite>().spriteName = "btn_GarbageCan_pushed";

        }
        if (Input.GetKeyUp(KeyCode.E) && isable_garbagecan && GarbageCanCount >0)        //w 눌렀을때 무기발사 (쓔레기통)
        {
            //버튼 눌려잇는거 추가
            GameObject.Find("Power Gauge").SetActive(true);
            isable_garbagecan = false;
            GarbageCanCount--;
            var attack_GarbageCan = Instantiate(Garbage_Set, Vector3.zero, Quaternion.identity) as GameObject;
            attack_GarbageCan.transform.localScale = new Vector3(1.5f, 2f, 2f);
            attack_GarbageCan.transform.localPosition = new Vector3(-2f, Player.transform.position.y, 0);
            attack_GarbageCan.transform.parent = _parent;
            attack_GarbageCan.gameObject.GetComponent<MoveFood>().power = _power * 5;
            PowerGauge.SetActive(false);
            child[6].GetComponent<UISprite>().spriteName = "btn_GarbageCan";
            if (GarbageCanCount > 0)
                isable_garbagecan = true;
            
        }

        if (Input.GetKeyDown(KeyCode.W) && isable_stone && StoneCount > 0 && Itemable_Stone)
        {
            PowerGauge.SetActive(true);
            child[5].GetComponent<UISprite>().spriteName = "btn_stone_pushed";
        }
        if (Input.GetKeyUp(KeyCode.W) && isable_stone && StoneCount >0 && Itemable_Stone)
        {
            //버튼 눌려있는거 추가
            
            isable_stone = false;
            StoneCount--;
            var attack_Stone = Instantiate(Stone_Set, Vector3.zero, Quaternion.identity) as GameObject;
            attack_Stone.transform.localPosition = new Vector3(-2f, Player.transform.position.y, 0);
            attack_Stone.transform.parent = _parent;
            attack_Stone.gameObject.GetComponent<MoveFood>().power = _power * 5;
            PowerGauge.SetActive(false);
            child[5].GetComponent<UISprite>().spriteName = "btn_stone";
            if (StoneCount > 0)
                isable_stone = true;
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 버튼 눌려있는거 추가
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
            child[3].GetComponent<UISprite>().spriteName = "점프버튼_눌림";
            DoJump();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            child[3].GetComponent<UISprite>().spriteName = "점프버튼";
            
        }
        // y값을 gameObject에 적용하세요.
        Vector3 pos = gameObject.transform.position;
        pos.y = y;
        gameObject.transform.position = pos;

        _hp--;
        _GuageBarWidget.fillAmount = _hp * 0.0005f;

        ScorePanel.color = Color.black;
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
        UISprite[] child = GameObject.Find("1_BGObj").GetComponentsInChildren<UISprite>();
        
        if(other.gameObject.name.Equals("비빔밥")){        //비빔밥 먹기
            Destroy(other.gameObject);  
            score = score + 100;
        }
        else if (other.gameObject.name.Equals("Item_Fork"))      //포크 먹기
        {
            child[4].GetComponent<UISprite>().spriteName = "btn_fork";
            Itemable_Fork = true;
            Destroy(other.gameObject);
            if (isable_fork == false)       //포크 던기기 가능하게 변수 바꿈
                isable_fork = true;     
            ForkCount++;                    //먹은 포크 개수 증가
        }

        else if (other.gameObject.name.Equals("Item_GarbageCan"))
        {
            child[6].GetComponent<UISprite>().spriteName = "btn_GarbageCan";
            Itemable_GarbageCan = true;
            Destroy(other.gameObject);
            if (isable_garbagecan == false)
                isable_garbagecan = true;
            GarbageCanCount++;
        }

        else if (other.gameObject.name.Equals("Item_Stone"))
        {
            child[5].GetComponent<UISprite>().spriteName = "btn_stone";
            Itemable_Stone = true;
            Destroy(other.gameObject);
            if (isable_stone == false)
                isable_stone = true;
            StoneCount++;
        }
    }
}

