﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlantBehavior : MonoBehaviour
{
    public int valuePlant;

    //WATERING PART
    public bool isReady = false;
    public float currentWater = 0;
    public float maxWater = 50;

    //GROWING PART
    public GameObject nextObject; // Next state of plant
    public bool isTimerRun = false;
    public float targetTime;

    //FERTILIZE PART
    public bool isFertilized = false;

    private bool isPicked = false;
    private bool isWeed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            if (transform.position.y > 0.6f)
            {
                isReady = false;
                isTimerRun = true;
                if (gameObject.tag == "Sapling")
                {
                    gameObject.tag = "Plant";
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.005f, transform.position.z);
            }
        }

        //GROWING PART
        if (isTimerRun & !isWeed)
        {
            timer(targetTime);
        }

        //BE STOLEN
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
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
        if (gameObject.transform.parent != null)
        {
            Instantiate(nextObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity,transform.parent);
        }
        else
        {
            Instantiate(nextObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), nextObject.transform.rotation);
        }
        PlantBehavior nextVariable = nextObject.GetComponent<PlantBehavior>();

        Destroy(gameObject);

    }

    private void OnTriggerStay(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.tag == "Planted")
        {
            GroundBehavior ground = otherObject.GetComponent<GroundBehavior>();
            if (ground.isWatered)
            {
                isReady = true;
            }

            if (ground.isFertile && !isFertilized)
            {
                valuePlant += valuePlant * 10 / 100;
                isFertilized = true;
            }

            //Check weed
            if (ground.isWeed)
            {
                isWeed = true;
            }
            else
            {
                isWeed = false;
            }

        }
        else if (otherObject.tag == "SpawnPoint")
        {
            PolyCarpicRespawn spawnPoint = otherObject.GetComponent<PolyCarpicRespawn>();
            if (spawnPoint.isFertile && !isFertilized)
            {
                valuePlant += valuePlant * 10 / 100;
                isFertilized = true;
            }
        }

        else if (otherObject.tag == "Mole")
        {
            Transform molePos = otherObject.GetComponent<Transform>();
            transform.position = new Vector3(transform.position.x, molePos.transform.position.y + 0.28f, transform.position.z);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject.tag == "Planted" || otherObject.tag == "SpawnPoint")
        {
            Collider collider = GetComponent<Collider>();
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            isTimerRun = false;
            rigidbody.useGravity = true;
            collider.isTrigger = false;
            transform.parent = null;

            if (otherObject.tag == "Planted" && !isPicked)
            {
                GroundBehavior ground = otherObject.GetComponent<GroundBehavior>();
                ground.isReset = true;
            }
            else if (otherObject.tag == "SpawnPoint" && !isPicked)
            {
                PolyCarpicRespawn spawnPoint = otherObject.GetComponent<PolyCarpicRespawn>();
                spawnPoint.isFertile = false;
            }
            isPicked = true;
        }
    }
}