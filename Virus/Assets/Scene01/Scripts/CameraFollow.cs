﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    //public float smoothCoef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (target)
        {
            //target.transform.position = transform.position;
            transform.position = target.transform.position;
            if (Input.GetKey("q"))
            {
                transform.Rotate(0, 1f, 0);
            }
            if (Input.GetKey("e"))
            {
                transform.Rotate(0, -1f, 0);
            }
            if (Input.mouseScrollDelta.y != 0f)
            {
                Camera.main.transform.localPosition += new Vector3(0, -Input.mouseScrollDelta.y, Input.mouseScrollDelta.y);
                //Camera.main.orthographicSize+= Input.mouseScrollDelta.y;
            }
        }

    }

}
