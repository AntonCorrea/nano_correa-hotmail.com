using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleToCamera : MonoBehaviour
{
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.eulerAngles-Camera.main.transform.eulerAngles);
        angle = Mathf.Abs(transform.eulerAngles.y - Camera.main.transform.eulerAngles.y);
    }
}
