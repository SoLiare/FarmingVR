﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeBehavior : MonoBehaviour
{
    public bool isReady = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 1)
        {
            isReady = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ground" && isReady)
        {
            GameObject ground = collider.gameObject;
            GroundBehavior groundVariable = ground.GetComponent<GroundBehavior>();
            groundVariable.isDug = true;
            isReady = false;
        }
    }
}
