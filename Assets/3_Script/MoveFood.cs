using UnityEngine;
using System.Collections;

public class MoveFood : MonoBehaviour {
    public float _speed;
    public float power=2;
	// Use this for initialization
	void Start () {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
      
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
	}
}
