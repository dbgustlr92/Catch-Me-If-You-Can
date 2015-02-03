using UnityEngine;
using System.Collections;

public class Earth_Rotate : MonoBehaviour {

    private bool _downed = false;            //터치 상태 
    private Vector3 _lastPos = Vector3.zero; //터치 포인트 

    public float SensitivityX = 0.05f;          //X 감도 
    public float SensitivityY = 0.05f;          //Y 감도 
    public bool ReverseX = false;        //X 반전 
    public bool ReverseY = false;        //Y 감도 

    void Update()
    {
        if (!_downed && Input.GetMouseButtonDown(0))
        {
            _downed = true;
            _lastPos = Input.mousePosition;
        }
        else if (_downed && Input.GetMouseButtonUp(0))
        {
            _downed = false;
        }
        else if (_downed)
        {
            Vector3 curPos = Input.mousePosition;
            float distX = curPos.x - _lastPos.x;
            float distY = curPos.y - _lastPos.y;
            _lastPos = curPos;

            if (Mathf.Abs(distX) >= 0.01f)
                RotateX(distX);

           // if (Mathf.Abs(distY) >= 0.01f)
                //RotateY(distY);
        }
    }

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
            transform.Rotate(-distY * SensitivityY, 0.0f, 0.0f);
        else
            transform.Rotate(distY * SensitivityY, 0.0f, 0.0f);
    }
}
