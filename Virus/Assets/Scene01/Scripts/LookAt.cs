using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    //public GameObject target;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle = GetComponent<AngleSprite>().angle;
        //transform.position += new Vector3(0,angle - 360,0);
        transform.rotation = Quaternion.Euler(42.72f,angle -360,0f);
    }
}
