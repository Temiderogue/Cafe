using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public Vector3 HumanLocation;
    public string Type;
    public SittingHuman Client;
    public bool InProcess = false;
    public Transform Food;
    public string FoodName;
    public emptyDish Dish;
    public bool isOnWork;

    public Action (Vector3 humanLocation, string type, SittingHuman client)
    {
        HumanLocation = humanLocation;
        Type = type;
        Client = client;
    }

    public Action(Transform food,SittingHuman client,string type) 
    {
        Type = type;
        Food = food;
        Client = client;
    }

    public Action(emptyDish dish, string type, SittingHuman client)
    {
        Dish = dish;
        Type = type;
        Client = client;
    }
}
