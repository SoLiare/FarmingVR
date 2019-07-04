﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveGiver : MonoBehaviour
{
    public Objective objective;
    public string[] requiredObject;

    // Start is called before the first frame update
    void Start()
    {
        /*objective.description = "collect 10 gold products";
        objective.type = ObjectiveType.Collecting;

        requiredObject = new string[] {"Gold"};

        objective.Init(10, requiredObject);*/

		objective.description = "Sell 10 gold fruits in the first minute of the game";
        objective.type = ObjectiveType.TimeLimit;

        requiredObject = new string[] {"Gold"};

        objective.Init(10, requiredObject, 60);

		/*objective.description = "Test Restriction Type";
        objective.type = ObjectiveType.Restriction;

        requiredObject = new string[] {"OH"};

        objective.Init(0, requiredObject, 0);*/
    }

	void Update()
	{
		if (objective.goal.completed)
		{
			GameObject Basket = GameObject.Find("basketVoid");
			BasketBehavior basketVariable = Basket.GetComponent<BasketBehavior>();

			basketVariable.score += objective.rewardScore;
		}
	}
}