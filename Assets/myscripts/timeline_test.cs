using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeline_test : MonoBehaviour {
    
    GameObject sphere;
    TimeBody script;

    // Use this for initialization
    void Start () {
        sphere = GameObject.Find("sphere");
        script = sphere.GetComponent<TimeBody>();
    }
	
	// Update is called once per frame
	void Update () {
        int length = script.cnt3;
        this.transform.localScale = new Vector3(length*0.002f, 0.1f, 0.1f);
        this.transform.localPosition = new Vector3(length*0.001f, 2.30f, 2.07f);
    }
}
