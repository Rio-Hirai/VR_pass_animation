using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeline_test2 : MonoBehaviour
{
    GameObject cube;
    TimeBody script;

    // Use this for initialization
    void Start ()
    {
        cube = GameObject.Find("cube");
        script = cube.GetComponent<TimeBody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        int length = script.cnt3;
        this.transform.localScale = new Vector3(length * 0.002f, 0.1f, 0.1f);
        this.transform.localPosition = new Vector3(length * 0.001f, 2.00f, 2.07f);

    }
}
