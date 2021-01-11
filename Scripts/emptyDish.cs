using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyDish : MonoBehaviour
{
    public bool isInGame;
    public Vector3 PoolPosition;
    public string Type;


    private void Start()
    {
        isInGame = false;
        PoolPosition = transform.position;
    }

    private void BackToPool()
    {
        isInGame = false;
        transform.position = PoolPosition;
    }
}
