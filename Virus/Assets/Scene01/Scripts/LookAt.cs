using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    //public GameObject target;
    public float angle= 42.72f;
    public float angleToCamera;
    //public float smoothCoef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //angleToCamera = GetComponent<AngleSprite>().angleToCamera;
        angleToCamera = Mathf.Rad2Deg * Mathf.Atan2(Camera.main.transform.position.z - transform.position.z,
                                    Camera.main.transform.position.x - transform.position.x);
        transform.rotation = Quaternion.Euler(angle,-90 - angleToCamera ,transform.rotation.eulerAngles.z);


    }

    private void FixedUpdate()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(42.72f, angle - 360, 0f),Time.deltaTime * smoothCoef);
    }
}
