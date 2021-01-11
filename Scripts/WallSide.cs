using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSide : MonoBehaviour
{
    [SerializeField] private GameObject _fullWall;
    [SerializeField] private GameObject _halfWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            _fullWall.SetActive(false);
            _halfWall.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            _fullWall.SetActive(true);
            _halfWall.SetActive(false);
        }
    }
}
