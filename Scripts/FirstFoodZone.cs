using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFoodZone : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private int _positionNum;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Food")
        {
            _spawner.MoveQueue(other.transform, other.GetComponent<Food>().TablePosition, _positionNum);
            other.GetComponent<Food>().isOnTable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            other.GetComponent<Mover>().CanDrag = true;
        }
    }
}
