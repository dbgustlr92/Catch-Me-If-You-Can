using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {
    public UILabel score2;
    public int scoretest = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        score2.text = scoretest.ToString();
	}
}
