using UnityEngine;
using System.Collections;

public class Touch_Stage : MonoBehaviour {

    public GameObject sword;
    public GameObject earth;
    public GameObject SelectPanel;
    public GameObject Enter;
    public GameObject Back;
    public Camera MainCamera;
    public Camera SubCamera;
    public bool StartSwitch = true;


    private bool _downed = false;            //터치 상태 
    private Vector3 _lastPos = Vector3.zero; //터치 포인트 

    public float SensitivityX = 0.05f;          //X 감도 
    public float SensitivityY = 0.05f;          //Y 감도 
    public bool ReverseX = false;        //X 반전 
    public bool ReverseY = false;        //Y 감도 


    void Start()
    {
        MainCamera.enabled = true;
        SubCamera.enabled = false;
        SelectPanel.SetActive(false);
    }


    void Update()
    {

        if (!_downed && Input.GetMouseButtonDown(0) && StartSwitch)
        {
            _downed = true;
            _lastPos = Input.mousePosition;
        }
        else if (_downed && Input.GetMouseButtonUp(0) && StartSwitch)
        {
            _downed = false;
        }
        else if (_downed && StartSwitch)
        {
            Vector3 curPos = Input.mousePosition;
            float distX = curPos.x - _lastPos.x;
            float distY = curPos.y - _lastPos.y;
            _lastPos = curPos;

            if (Mathf.Abs(distX) >= 0.01f)
                RotateX(distX);

            //if (Mathf.Abs(distY) >= 0.01f)            //Y축 회전
                //RotateY(distY);
        }


        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        Ray ray2 = SubCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObj;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, -1, 0);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.Z))//z키 눌렷을때 스테이지 1 검은색으로
        {
            GameObject.Find("stage1_1").renderer.material.color = new Color(0, 0, 0);
            GameObject.Find("stage1_2").renderer.material.color = new Color(0, 0, 0);
        }
      
        if (Input.GetKeyUp(KeyCode.Z))//z키 뗏을때 들어가기 창
        {
            GameObject.Find("stage1_1").renderer.material.color = new Color(255, 255, 255);
            GameObject.Find("stage1_2").renderer.material.color = new Color(255, 255, 255);
            transform.rotation = Quaternion.identity;
            transform.Rotate(-1.018213f, 367.138f, 33.53464f);
            MainCamera.enabled = false;
            SubCamera.enabled = true;
            SelectPanel.SetActive(true);
            StartSwitch = false;
        }
        if (Input.GetKeyDown(KeyCode.X) && SubCamera.enabled == true)
        {
            GameObject.Find("Btn_Enter").gameObject.GetComponent<UISprite>().spriteName = "Enter눌림";
        }
        if (Input.GetKeyUp(KeyCode.X) && SubCamera.enabled == true)
        {
            Application.LoadLevel("inGame");
        }

        if (Input.GetKeyDown(KeyCode.C) && SubCamera.enabled == true)
        {
            GameObject.Find("Btn_Back").gameObject.GetComponent<UISprite>().spriteName = "Back눌림";
        }
        if (Input.GetKeyUp(KeyCode.C) && SubCamera.enabled == true)
        {
            GameObject.Find("Btn_Back").gameObject.GetComponent<UISprite>().spriteName = "Back버튼";
            StartSwitch = true;
            MainCamera.enabled = true;
            SubCamera.enabled = false;
            SelectPanel.SetActive(false);
        }


        if (Physics.Raycast(ray, out hitObj, Mathf.Infinity))
        {

            Debug.Log(hitObj.transform.gameObject.name);
            if (hitObj.transform.gameObject.name.Equals("stage1_1") || hitObj.transform.gameObject.name.Equals("stage1_2"))          //  깃발을 터치 했을시에
            {

                /*핸드폰 용
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {

                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {

                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)           //터치를 뗐을때 활성화
                {
                    transform.rotation = Quaternion.identity;
                    transform.Rotate(-1.018213f, 367.138f, 33.53464f);
                    MainCamera.enabled = false;
                    SubCamera.enabled = true;
                    SelectPanel.SetActive(true);
                    StartSwitch = false;
                }*/
                
                //컴퓨터용
              
                    transform.rotation = Quaternion.identity;
                    transform.Rotate(-1.018213f, 367.138f, 33.53464f);
                    MainCamera.enabled = false;
                    SubCamera.enabled = true;
                    SelectPanel.SetActive(true);
                    StartSwitch = false;
            }


            
            if (Physics.Raycast(ray2, out hitObj, Mathf.Infinity) && SubCamera.enabled == true)
            {
                /*핸드폰용
               if (hitObj.transform.gameObject.Equals(Enter))
               {
                   if (Input.GetTouch(0).phase == TouchPhase.Began)
                   {

                   }
                   else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                   {

                   }
                   else if (Input.GetTouch(0).phase == TouchPhase.Ended)           //터치를 뗐을때 활성화
                   {
                       Application.LoadLevel("inGame");
                   }
               }
               else if (hitObj.transform.gameObject.Equals(Back))
               {
                   if (Input.GetTouch(0).phase == TouchPhase.Began)
                   {

                   }
                   else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                   {

                   }
                   else if (Input.GetTouch(0).phase == TouchPhase.Ended)           //터치를 뗐을때 활성화
                   {
                       StartSwitch = true;
                       MainCamera.enabled = true;
                       SubCamera.enabled = false;
                       SelectPanel.SetActive(false);
                   }
               }*/
                
                Debug.Log(hitObj.transform.gameObject.name);
                if (hitObj.transform.gameObject.Equals(Enter))
                {
                    Application.LoadLevel("inGame");
                }
                if(hitObj.transform.gameObject.Equals(Back))
                {
                    StartSwitch = true;
                    MainCamera.enabled = true;
                    SubCamera.enabled = false;
                    SelectPanel.SetActive(false);
                }
            }

        }
        
    }
    //} 



    private void RotateX(float distX)
    {
        if (ReverseX)
            transform.Rotate(0.0f, distX * SensitivityX, 0.0f);
        else
            transform.Rotate(0.0f, -distX * SensitivityX, 0.0f);
    }

    private void RotateY(float distY)
    {
        if (ReverseY)
            transform.Rotate(distY * SensitivityY, 0.0f, 0.0f);
        else
            transform.Rotate(-distY * SensitivityY, 0.0f, 0.0f);
    }


}