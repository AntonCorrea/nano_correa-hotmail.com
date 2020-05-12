using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngleSprite : MonoBehaviour
{
    public float angleToCamera,angleToDestination,angleToSprite;
    //public GameObject controller;
    public NavMeshAgent navAgent;
    public Vector3 destination;
    public float velocity;
    //public GameObject spritePivot;
    //private SpriteRenderer spriteRenderer;
   // public Sprite[] sprite;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        destination = navAgent.destination;
        velocity = navAgent.velocity.sqrMagnitude;
        animator.SetFloat("velocity", velocity);

        angleToCamera = GetComponent<LookAt>().angleToCamera;
       
        if (Vector3.Distance(transform.position, destination) > 2f)
        {
            angleToDestination = Mathf.Rad2Deg * Mathf.Atan2(destination.z - transform.position.z,
                                                        destination.x - transform.position.x);
        }
        
        angleToSprite = (angleToCamera - angleToDestination);

        //entre 337.5 y 22.5 es el sprite 2
        //asi que sumo y resto para que de 0 a 45 sea el sprite 0
        /*angle += 22.5f + 45 + 45;
        angle += (angle > 360) ? -360 : 0;
        //asigno el sprite
        spriteRenderer.sprite = sprite[(int)Mathf.Floor(angle / 45)];*/

        if (angleToSprite < 0)
        {
            angleToSprite = 360 + angleToSprite;
        }
        if (angleToSprite > 297.5 && angleToSprite < 337.5)
        {
            animator.SetFloat("direction", 0);            
        }
        if (angleToSprite > 337.5 || angleToSprite < 22.5)
        {
            animator.SetFloat("direction", 1);            
        }
        if (angleToSprite > 22.5 && angleToSprite < 67.5)
        {
            animator.SetFloat("direction", 2);            
        }
        if (angleToSprite > 67.5 && angleToSprite < 112.5)
        {
            animator.SetFloat("direction", 3);            
        }
        if (angleToSprite > 112.5 && angleToSprite < 157.5)
        {            
            animator.SetFloat("direction", 4);
        }
        if (angleToSprite > 157.5 && angleToSprite < 202.5)
        {            
            animator.SetFloat("direction", 5);
        }
        if (angleToSprite > 202.5 && angleToSprite < 247.5)
        {          
            animator.SetFloat("direction", 6);
        }
        if (angleToSprite > 247.5 && angleToSprite < 297.5)
        {           
            animator.SetFloat("direction", 7);
        }


    }
}
