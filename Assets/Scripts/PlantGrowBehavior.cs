﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlantGrowBehavior : MonoBehaviour
{
    public GameObject nextObject; // Next state of plant
    public float targetTime;
    public bool isTimerRun = true;
    public int valuePlant;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        //rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRun)
        {
            timer(targetTime);    
        }

        Throwable throwable = GetComponent<Throwable>();
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

            if (throwable.attached)
            {
                isTimerRun = false;
                rigidbody.useGravity = true;
                //rigidbody.isKinematic = false;
            }


    }

    void timer(float time)
    {

        if (targetTime <= 0.0f)
        {
            isTimerRun = false;
            changeState();
        }
        else
        {
            targetTime -= Time.deltaTime;
        }

    }

    void changeState()
    {
        //Debug.Log("CHANGING...");
 
        targetTime = 3.0f;
        GameObject newObject;
        newObject = (GameObject)EditorUtility.InstantiatePrefab(nextObject);
        newObject.transform.position = transform.position;
        newObject.transform.rotation = transform.rotation;

        Destroy(gameObject);
       

    }

    /*
    //Check harvesting and stop growing up
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "AllGround") // Collider of [Empty in Ground] that is set the tag to "AllGround"
        {
            //Debug.Log("STOP GROWING UP");
            isTimerRun = false;
        }
    }
    */
}