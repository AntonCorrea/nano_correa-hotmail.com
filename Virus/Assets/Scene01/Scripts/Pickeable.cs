using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickeable : MonoBehaviour
{
    //public bool isPickable;
    // Start is called before the first frame update
    public bool readyToPick = false;
    public GameObject caller;
    public string description;
    public string recipeFor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToPick)
        {
            if (Vector3.Distance(transform.position, caller.transform.position) < 2f)
            {
                caller.GetComponent<Inventory>().Add(transform.name);
                //print(transform.name);
                caller.GetComponent<PlayerController>().PrintMsg("Has recogido " + transform.name.Remove(0, 5) + ".");
                Destroy(gameObject);
            }
        } 
    }

    /*public bool isPickeable(GameObject caller)
    {
 
        if (Vector3.Distance(transform.position, caller.transform.position) < 2f)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }*/
}
