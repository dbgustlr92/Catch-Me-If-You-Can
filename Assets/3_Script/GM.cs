using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {
    public GameObject _enemySet;
    public GameObject _nearBgObj;
   

	// Use this for initialization
	void Start () {
       
        
	}

    public bool _SpawnChk = true;
	// Update is called once per frame
	void Update () {
        if (_nearBgObj.transform.localPosition.x <-1200.0f && _SpawnChk)
        {
            var Set1 = Instantiate(_enemySet, Vector3.zero, Quaternion.identity) as GameObject;
            Set1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Set1.transform.localPosition = new Vector3(-10,0,0);
            
            _SpawnChk = false;
        }
        if (_nearBgObj.transform.localPosition.x > -1300.0f && !_SpawnChk)
        {
            _SpawnChk = true;
        }
        
	}
}
