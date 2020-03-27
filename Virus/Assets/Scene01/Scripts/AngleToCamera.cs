using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AngleToCamera : MonoBehaviour
{
    public float angle;
    private AICharacterControlTest parent;
    public float dir_angle;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<AICharacterControlTest>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.eulerAngles-Camera.main.transform.eulerAngles);
        angle = Mathf.Abs(transform.eulerAngles.y - Camera.main.transform.eulerAngles.y);
 
        Vector2 director = new Vector2(parent.director.x, parent.director.z);
        director.Normalize();
        dir_angle = Mathf.Asin(director.x);

        dir_angle = dir_angle * 180 / Mathf.PI;
        if (director.y < 0)
        {
            dir_angle = (dir_angle - 180) * (-1);
        }
        angle -= dir_angle;
        if (angle < 0)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
    }
}
