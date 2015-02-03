using UnityEngine;
using System.Collections;

public class Star_Rotate : MonoBehaviour {
    public int Rotate_Speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * Rotate_Speed);

    }
}