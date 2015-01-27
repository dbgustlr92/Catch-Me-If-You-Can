using UnityEngine;
using System.Collections;

public class Player_R : MonoBehaviour {

    public float _hp = 100;
    public GameObject _DamageEff;
    public UIFilledSprite _GuageBarWidget;
    public float power;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        
        if (other.name.Equals("Fork"))      //무기타입별로 체력 다르게 
        {
            _hp -=(float)0.02*other.GetComponentInParent<MoveFood>().power ;     //부모의 값을 받아야 하므로 부모로 이동해서 값을 받아옵니다. 
            
            _GuageBarWidget.fillAmount = _hp * 0.01f;
            var _Eff1 = Instantiate(_DamageEff, transform.localPosition, Quaternion.identity) as GameObject;
            _Eff1.transform.parent = transform;
            _Eff1.transform.localPosition = Vector3.zero;
            _Eff1.transform.localScale = new Vector3(1, 1, 1);
            
            Destroy(other.gameObject);
            
        }
    }
}
