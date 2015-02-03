using UnityEngine;
using System.Collections;

public class quest_color : MonoBehaviour {

	// Use this for initialization
	void Start () {
        renderer.material.color = new Color(93.0f, 93.0f, 93.0f);
        renderer.material.shader = Shader.Find("UI/Default");
	}
	
	// Update is called once per frame
	void Update () {

        
        
	}
}
