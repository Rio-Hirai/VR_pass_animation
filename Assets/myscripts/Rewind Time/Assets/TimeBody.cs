using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    bool isRewinding = true;
    public bool outputflag = true;

    public float recordTime = 10f;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    public int cnt = 1;
    public int cnt2 = 1;
    public int cnt3 = 1;
    public int cnt4 = 1;
    public int cnt5 = 1;
    public float outputx = 0;
    public float outputy = 0;
    public float outputz = 0;
    public int gf = 0;

    // Use this for initialization

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gf);
        if (Input.GetKeyDown("joystick button 15"))
        {
            //Debug.Log("OK15");
            cnt2 = cnt4;
            StartRewind();
        }
        if (Input.GetKeyDown("joystick button 5"))
        {
            //Debug.Log("stop");
            StopRewind();
        }
        if (Input.GetKeyDown("joystick button 17"))
        {
            Debug.Log("OK15");
            cnt5 = cnt2;
            cnt2 = 0;
            StartRewind();
            cnt2 = cnt5;
            //StartRecord();
            //isRewinding = false;
            //rb.isKinematic = false;
            //outputflag = false;
        }

    }

    void FixedUpdate()
    {
        //Debug.Log("rewinding = " + isRewinding);
        if (isRewinding)
        {
            //Debug.Log("rewind");
            Rewind();
        }
        else
        {
            //Debug.Log("record");
            Record();
        }
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[cnt2];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            if (cnt3 > cnt2)
            {
                cnt2++;
            }
            else
            {
                //cnt2 = 1;
            }

        }
        else
        {
            //StopRewind();
            //Debug.Log("else"+ pointsInTime.Count);
            //cnt2 = 1;
        }

        if (cnt2 == cnt3)
        {
            cnt2 = 0;
        }

        cnt = cnt2;
        cnt4 = cnt2;

        //Debug.Log("cnt2(=position) = " +cnt2);
        //Debug.Log("cnt3(=position) = " +cnt3);

    }

    void Preview()
    {

        PointInTime pointInTime = pointsInTime[cnt2];
        transform.position = pointInTime.position;
        transform.rotation = pointInTime.rotation;
        if (cnt2 > 0)
        {
            cnt2--;
        }
        else
        {
            //cnt2 = 0;
        }

        cnt = cnt2;

    }

    void Record()
    {
        if (gf == 1)
        {
            if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
            {
                pointsInTime.RemoveAt(pointsInTime.Count - 1);
                //Debug.Log("remove");
            }
            pointsInTime.Insert(cnt, new PointInTime(transform.position, transform.rotation));
            cnt++;
            cnt3 = cnt;
            outputx = transform.position.x * 1000;
            outputy = transform.position.y * 1000;
            outputz = transform.position.z * 1000;

            //Debug.Log("cnt(=length) = " + cnt);
            //Debug.Log("cnt3(=length) = " + cnt3);
        }
    }

    void StartRecord()
    {
        Debug.Log("record");
        /*
        cnt2 = 0;
        PointInTime pointInTime = pointsInTime[cnt2];
        transform.position = pointInTime.position;
        transform.rotation = pointInTime.rotation;
        if (cnt2 > 0)
        {
            cnt2++;
        }
        */

        if (pointsInTime.Count > 0)
        {
            cnt2 = 0;
            PointInTime pointInTime = pointsInTime[cnt2];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            if (cnt3 > cnt2)
            {
                cnt2++;
            }
            else
            {
                //cnt2 = 1;
            }

        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
        outputflag = true;
        //Debug.Log("true");
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
        outputflag = false;
        //Debug.Log("false");
    }
}
