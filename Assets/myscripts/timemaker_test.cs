using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timemaker_test : MonoBehaviour
{
    GameObject sphere;
    GameObject cube;
    TimeBody script_s;
    TimeBody script_c;

    // Use this for initialization
    void Start ()
    {
        sphere = GameObject.Find("sphere");
        script_s = sphere.GetComponent<TimeBody>();
        cube = GameObject.Find("cube");
        script_c = cube.GetComponent<TimeBody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        int preview_s = script_s.cnt2;
        int preview_c = script_c.cnt2;
        int preview;
        if (preview_s >= preview_c)
        {
            preview = preview_s;
        } else
        {
            preview = preview_c;
        }
        //Transform myTransform = this.transform;
        //Vector3 pos = myTransform.position;
        //pos.x += preview * 0.002f;
        this.transform.localPosition = new Vector3(preview * 0.002f, 2.23f, 2.03f);

    }
}
